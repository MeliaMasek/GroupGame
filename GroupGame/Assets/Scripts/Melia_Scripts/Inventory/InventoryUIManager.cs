using UnityEngine;
using UnityEngine.InputSystem;

//Code borrowed and Modified by Dan Pos off of the inventory system series from youtube https://www.youtube.com/playlist?list=PL-hj540P5Q1hLK7NS5fTSNYoNJpPWSL24
public class InventoryUIManager : MonoBehaviour
{
    public DynamicInventoryDisplay inventoryPanel;
    public int inventorySize = 10;
    private void Awake()
    {
        inventoryPanel.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        InventoryHolder.OnDynamicInventoryDisplayRequested += DisplayInventory;
    }
    
    private void OnDisable()
    {
        InventoryHolder.OnDynamicInventoryDisplayRequested -= DisplayInventory;
    }

    private void Update()
    {
        if (inventoryPanel.gameObject.activeInHierarchy && Keyboard.current.escapeKey.wasPressedThisFrame) inventoryPanel.gameObject.SetActive(false);
    }

    void DisplayInventory(InventorySystem invToDisplay)
    {
        inventoryPanel.gameObject.SetActive(true);
        inventoryPanel.RefreshDynamicInventory(invToDisplay);
    }
}
