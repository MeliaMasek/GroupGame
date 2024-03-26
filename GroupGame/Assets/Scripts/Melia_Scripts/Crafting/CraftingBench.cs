using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//Code borrowed and Modified by Dan Pos off of the inventory system series from youtube https://www.youtube.com/playlist?list=PL-hj540P5Q1hLK7NS5fTSNYoNJpPWSL24

public class CraftingBench : MonoBehaviour, IInteractable
{
    [SerializeField] private CraftingRecipe activeRecipe;
    
    private InventorySystem playerInventory;

    public UnityAction<IInteractable> OnInteractionComplete { get; set; }
    public void Interact(Interactor interactor, out bool interactSuccessful)
    {
        playerInventory = interactor.GetComponent<InventoryHolder>().PrimaryInventorySystem;
        interactSuccessful = true;
    }

    public void EndInteraction()
    {
        
    }
}
