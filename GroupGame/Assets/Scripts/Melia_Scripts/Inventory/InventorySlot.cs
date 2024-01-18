using UnityEngine;

[System.Serializable]

//Code borrowed and Modified by Dan Pos off of the inventory system series from youtube https://www.youtube.com/playlist?list=PL-hj540P5Q1hLK7NS5fTSNYoNJpPWSL24

public class InventorySlot
{
    [SerializeField] private InventoryData itemData;
    [SerializeField] private int stacksize;
    
    public InventoryData ItemData => itemData;
    public int StackSize => stacksize;

    public InventorySlot(InventoryData source, int amount)
    {
        itemData = source;
        stacksize = amount;
    }

    public InventorySlot()
    {
        ClearSlot();
    }

    public void ClearSlot()
    {
        itemData = null;
        stacksize = -1;
    }

    public void UpdateInventorySlot(InventoryData data, int amount)
    {
        itemData = data;
        stacksize = amount;
    }

    public bool RoomLeftInStack(int amountToAdd, out int amountRemaining)
    {
        amountRemaining = ItemData.maxStackSize - stacksize;
        return RoomLeftInStack(amountToAdd);
    }

    public bool RoomLeftInStack(int amountToAdd)
    {
        if (stacksize + amountToAdd <= itemData.maxStackSize) return true;
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
}

