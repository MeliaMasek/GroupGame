using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

//Code borrowed and Modified by Dan Pos off of the inventory system series from youtube https://www.youtube.com/playlist?list=PL-hj540P5Q1hLK7NS5fTSNYoNJpPWSL24
public class ShopKeeperDisplay : MonoBehaviour
{
    [SerializeField] private ShopSlotsUI shopSlopPrefab;
    [SerializeField] private ShoppingCartItemUI shoppingCartItemPrefab;
    
    [SerializeField] private Button buyTab;
    [SerializeField] private Button sellTab;

    [Header("Shopping Cart")]
    [SerializeField] private Text basketTotalText;
    [SerializeField] private Text playerGoldText;
    [SerializeField] private Text shopGoldText;
    [SerializeField] private Button buyButton;
    [SerializeField] private Text buyButtonText;
    
    [Header("Item Preview Section")]
    [SerializeField] private Image itemPreviewSprite;
    [SerializeField] private Text itemPreviewName;
    [SerializeField] private Text itemPreviewDescription;
    
    [SerializeField] private GameObject itemListContentPanel;
    [SerializeField] private GameObject shoppingCartContentPanel;
    
    private int basekTotal;
    private bool isSelling;
    
    private ShopSystem _shopSystem;
    private PlayerInventoryHolder _playerInventory;

    private Dictionary<InventoryData, int> shoppingCart = new Dictionary<InventoryData, int>();
    private Dictionary<InventoryData, ShoppingCartItemUI> shoppingCartUI = new Dictionary<InventoryData, ShoppingCartItemUI>();

    public void DisplayShopWindow(ShopSystem shopSystem, PlayerInventoryHolder playerInventory)
    {
        _shopSystem = shopSystem;
        _playerInventory = playerInventory;

        RefreshDisplay();
    }

    public void RefreshDisplay()
    {
        if (buyButton != null)
        {
            buyButtonText.text = isSelling ? "Sell Items" : "Buy Items";
            buyButton.onClick.RemoveAllListeners();
            if (isSelling) buyButton.onClick.AddListener(SellItems);
            else buyButton.onClick.AddListener(BuyItems);
        }
        
        ClearSlots();
        ClearItemPreview();
        
        basketTotalText.enabled = false;
        buyButton.gameObject.SetActive(false);
        basekTotal = 0;
        playerGoldText.text = $"Player Gold: {_playerInventory.PrimaryInventorySystem.Gold}";
        shopGoldText.text = $"Shop Gold: {_shopSystem.AvailableGold}";

        if (isSelling) DisplayPlayerInventory();
        else DisplayShopInventory();
    }

    private void BuyItems()
    {
        if (_playerInventory.PrimaryInventorySystem.Gold < basekTotal) return;
        if(!_playerInventory.PrimaryInventorySystem.CheckInvRemaining(shoppingCart)) return;

        foreach (var kvp in shoppingCart)
        {
            _shopSystem.PurchaseItem(kvp.Key, kvp.Value);

            for (int i = 0; i < kvp.Value; i++)
            {
                _playerInventory.PrimaryInventorySystem.AddToInventory(kvp.Key, 1);
            }
        }
        
        _shopSystem.GainGold(basekTotal);
        _playerInventory.PrimaryInventorySystem.SpendGold(basekTotal);
        
        RefreshDisplay();
    }
    private void SellItems()
    {
        if (_shopSystem.AvailableGold < basekTotal) return;

        foreach (var kvp in shoppingCart)
        {
            var price = GetModifiedPrice(kvp.Key, kvp.Value, _shopSystem.SellMarkUp);

            _shopSystem.SellItem(kvp.Key, kvp.Value, price);
            _playerInventory.PrimaryInventorySystem.GainGold(price);
            _playerInventory.PrimaryInventorySystem.RemoveItemFromInv(kvp.Key, kvp.Value);
        }
        RefreshDisplay();
    }
    
    private void ClearSlots()
    {
        shoppingCart = new Dictionary<InventoryData, int>();
        shoppingCartUI = new Dictionary<InventoryData, ShoppingCartItemUI>();
        
        foreach (var item in itemListContentPanel.transform.Cast<Transform>())
        {
            Destroy(item.gameObject);
        }
        foreach (var item in shoppingCartContentPanel.transform.Cast<Transform>())
        {
            Destroy(item.gameObject);
        }
    }

    private void DisplayPlayerInventory()
    {
        foreach (var item in _playerInventory.PrimaryInventorySystem.GetAllItemsHeld())
        {
            var tempSlot = new ShopSlots();
            tempSlot.AssignItem(item.Key, item.Value);
            
            var shopSlot = Instantiate(shopSlopPrefab, itemListContentPanel.transform);
            shopSlot.Init(tempSlot, _shopSystem.SellMarkUp);
        }
    }
    
    private void DisplayShopInventory()
    {
        foreach (var item in _shopSystem.ShopInventory)
        {
            if (item.ItemData == null) continue;
            {
                var shopSlot = Instantiate(shopSlopPrefab, itemListContentPanel.transform);
                shopSlot.Init(item, _shopSystem.BuyMarkUp);
            }
        }
    }
    
    public void AddItemToCart(ShopSlotsUI shopSlotsUI)
    {
        var data = shopSlotsUI.AssignedItemSlot.ItemData;
        
        UpdateItemPreview(shopSlotsUI);

        var price = GetModifiedPrice(data, 1, shopSlotsUI.MarkUp);

        if (shoppingCart.ContainsKey(data))
        {
            shoppingCart[data]++;   
            var newString = $"{data.displayName} {price}G x {shoppingCart[data]}";
            shoppingCartUI[data].SetItemText(newString);
        }
        else
        {
            shoppingCart.Add(data, 1);
            var shoppingCartTextObj = Instantiate(shoppingCartItemPrefab, shoppingCartContentPanel.transform);
            var newString = $"{data.displayName} {price}G x 1";
            shoppingCartTextObj.SetItemText(newString);
            shoppingCartUI.Add(data, shoppingCartTextObj);
        }
        
        basekTotal += price;
        basketTotalText.text = $"Total: {basekTotal}G";

        if (basekTotal > 0 && !basketTotalText.IsActive())
        {
            basketTotalText.enabled = true;
            buyButton.gameObject.SetActive(true);
        }
        
        CheckCartAvailGold();
    }

    public void RemoveItemFromCart(ShopSlotsUI shopSlotsUI)
    {
        var data = shopSlotsUI.AssignedItemSlot.ItemData;
        var price = GetModifiedPrice(data, 1, shopSlotsUI.MarkUp);

        if (shoppingCart.ContainsKey(data))
        {
            shoppingCart[data]--;
            var newString = $"{data.displayName} {price}G x {shoppingCart[data]}";
            shoppingCartUI[data].SetItemText(newString);

            if (shoppingCart[data] <= 0)
            {
                shoppingCart.Remove(data);
                var tempObj = shoppingCartUI[data].gameObject;
                shoppingCartUI.Remove(data);
                Destroy(tempObj);
            }
        }
        basekTotal -= price;
        basketTotalText.text = $"Total: {basekTotal}G";

        if (basekTotal <= 0 && basketTotalText.IsActive())
        {
            basketTotalText.enabled = false;
            buyButton.gameObject.SetActive(false);
            ClearItemPreview();
            return; 
        }
        CheckCartAvailGold();
    }

    private void ClearItemPreview()
    {
        itemPreviewSprite.sprite = null;
        itemPreviewSprite.color = Color.clear;
        itemPreviewName.text = "";
        itemPreviewDescription.text = "";
    }
    
    private void UpdateItemPreview(ShopSlotsUI shopSlotsUI)
    {
        var data = shopSlotsUI.AssignedItemSlot.ItemData;
        
        itemPreviewSprite.sprite = data.icon;
        itemPreviewSprite.color = Color.white;
        itemPreviewName.text = data.displayName;
        itemPreviewDescription.text = data.description;
    }

    public void OnBuyTabPressed()
    {
        isSelling = false;
        RefreshDisplay();
    }
    
    public void OnSellTabPressed()
    {
        isSelling = true;
        RefreshDisplay();
    }
    
    public static int GetModifiedPrice(InventoryData data, int amount, float markUp)
    {
        var baseValue = data.GoldValue * amount;
        
        return Mathf.FloorToInt(baseValue + baseValue * markUp);    
    }
    
    public void CheckCartAvailGold()
    {
        var goldToCheck =  isSelling ? _shopSystem.AvailableGold : _playerInventory.PrimaryInventorySystem.Gold;
        
        basketTotalText.color = basekTotal > goldToCheck ? Color.red : Color.white;

        if (isSelling || _playerInventory.PrimaryInventorySystem.CheckInvRemaining(shoppingCart)) return;
        
        basketTotalText.text = "Not enough space in inventory";
        basketTotalText.color = Color.red;
    }
}


