using System;
using UnityEngine;

[System.Serializable]

//Code borrowed and Modified by Dan Pos off of the inventory system series from youtube https://www.youtube.com/playlist?list=PL-hj540P5Q1hLK7NS5fTSNYoNJpPWSL24

public class InventorySlot : ISerializationCallbackReceiver
{
    [NonSerialized] private InventoryData itemData;
    [SerializeField] private int itemID = -1;
    [SerializeField] private int stacksize;
    
    public InventoryData ItemData => itemData;
    public int StackSize => stacksize;

    public InventorySlot(InventoryData source, int amount)
    {
        itemData = source;
        itemID = itemData.ID;
        stacksize = amount;
    }

    public InventorySlot()
    {
        ClearSlot();
    }

    public void ClearSlot()
    {
        itemData = null;
        itemID = -1;
        stacksize = -1;
    }


    public void AssignItem(InventorySlot invSlot)
    {
        if (itemData == invSlot.itemData) AddToStack(invSlot.stacksize);
        else
        {
            itemData = invSlot.itemData;
            itemID = itemData.ID;
            stacksize = 0;
            AddToStack(invSlot.stacksize);
        }
    }
    
    public void UpdateInventorySlot(InventoryData data, int amount)
    {
        itemData = data;
        itemID = itemData.ID;
        stacksize = amount;
    }

    public bool RoomLeftInStack(int amountToAdd, out int amountRemaining)
    {
        amountRemaining = ItemData.maxStackSize - stacksize;
        return RoomLeftInStack(amountToAdd);
    }

    public bool RoomLeftInStack(int amountToAdd)
    {
        if (itemData == null || itemData != null && stacksize + amountToAdd <= itemData.maxStackSize) return true;
        else return false;
    }
    
    public void AddToStack(int amount)
    {
        stacksize += amount;
    }

    public void RemoveFromStack(int amount)
    {
        stacksize -= amount;
    }

    public bool SpiltStack(out InventorySlot spiltStack)
    {
        if (stacksize <= 1)
        {
            spiltStack = null;
            return false;
        }
        
        int halfStack = Mathf.RoundToInt((stacksize / 2));
        RemoveFromStack(halfStack);
        
        spiltStack = new InventorySlot(itemData, halfStack);
        return true;
    }

    public void OnBeforeSerialize()
    {
        
    }

    public void OnAfterDeserialize()
    {
        if (itemID == -1) return;
        {
            var db = Resources.Load<Database>("Database");
            itemData = db.GetItem(itemID);
        }
    }
}

