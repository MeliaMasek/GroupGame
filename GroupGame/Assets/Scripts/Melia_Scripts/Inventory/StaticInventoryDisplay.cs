using System.Collections.Generic;
using UnityEngine;

//Code borrowed and Modified by Dan Pos off of the inventory system series from youtube https://www.youtube.com/playlist?list=PL-hj540P5Q1hLK7NS5fTSNYoNJpPWSL24

public class StaticInventoryDisplay : InventoryDisplay
{
    [SerializeField] private InventoryHolder inventoryHolder;
    [SerializeField] private SlotUI[] slotArray;
    protected override void Start()
    {
        base.Start();
        if (inventoryHolder != null)
        {
            inventorySystem = inventoryHolder.InventorySystem;
            inventorySystem.OnInventorySlotChanged += UpdateSlot;
        }
        else Debug.LogWarning($"no inventory assigned to this {this.gameObject}");
        
        AssignSlot(inventorySystem);
    }
    
    public override void AssignSlot(InventorySystem invToDisplay)
    {
        slotDictionary = new Dictionary<SlotUI, InventorySlot>();

        if (slotArray.Length != inventorySystem.InventorySize) //Debug.Log($"inventory slots dont match on {this.gameObject} ");
        
            for (int i = 0; i < inventorySystem.InventorySize; i++)
            {
                slotDictionary.Add(slotArray[i], inventorySystem.InventorySlots[i]);
                slotArray[i].Init(inventorySystem.InventorySlots[i]);
            }
    }
}
