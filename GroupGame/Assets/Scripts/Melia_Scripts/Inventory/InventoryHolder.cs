using UnityEngine;
using UnityEngine.Events;

[System.Serializable]

//Code borrowed and Modified by Dan Pos off of the inventory system series from youtube https://www.youtube.com/playlist?list=PL-hj540P5Q1hLK7NS5fTSNYoNJpPWSL24

public class InventoryHolder : MonoBehaviour
{
    [SerializeField] private int inventorySize;
    [SerializeField] protected InventorySystem inventorySystem;
    
    public InventorySystem InventorySystem => inventorySystem;
    
    public static UnityAction<InventorySystem> OnDynamicInventoryDisplayRequested;

    private void Awake()
    {
        inventorySystem = new InventorySystem(inventorySize);
    }
}
