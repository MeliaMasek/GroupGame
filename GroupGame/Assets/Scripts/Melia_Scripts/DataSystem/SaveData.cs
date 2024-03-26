using System.Collections.Generic;

//Code borrowed and Modified by Dan Pos off of the inventory system series from youtube https://www.youtube.com/playlist?list=PL-hj540P5Q1hLK7NS5fTSNYoNJpPWSL24
public class SaveData
{
    public List<string> collectedItems;
    public SerializableDictionary<string, ItemPickupSaveData> activeItems;
    public SerializableDictionary<string, InventorySaveData> chestDictionary;
    public SerializableDictionary<string, ShopSaveData> shopKeeperDictionary;

    public InventorySaveData playerInventory;
    
    public SaveData()
    {
        collectedItems = new List<string>();
        activeItems = new SerializableDictionary<string, ItemPickupSaveData>();
        chestDictionary = new SerializableDictionary<string, InventorySaveData>();
        playerInventory = new InventorySaveData();
        shopKeeperDictionary = new SerializableDictionary<string, ShopSaveData>();
    }
}
