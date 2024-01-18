using System.Collections.Generic;
using UnityEngine;

//Code borrowed and Modified by Dan Pos off of the inventory system series from youtube https://www.youtube.com/playlist?list=PL-hj540P5Q1hLK7NS5fTSNYoNJpPWSL24

public abstract class InventoryDisplay : MonoBehaviour
{
    protected InventorySystem inventorySystem;
    protected Dictionary<SlotUI, InventorySlot> slotDictionary;

    public InventorySystem InventorySystem => inventorySystem;
    public Dictionary<SlotUI, InventorySlot> SlotDictionary => slotDictionary;

    protected virtual void Start()
    {
        
    }

    public abstract void AssignSlot(InventorySystem invToDisplay);

    protected virtual void UpdateSlot(InventorySlot updatedSlot)
    {
        foreach (var slot in SlotDictionary)
        {
            if (slot.Value == updatedSlot) //slot value
            {
                slot.Key.UpdateUISlot(updatedSlot); //slot key (UI value)
            }
        }
    }

    public void SlotClicked(SlotUI clickedSlot)
    {
        Debug.Log("Slot Clicked");
    }
}
