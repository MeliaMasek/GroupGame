using UnityEngine;
using UnityEngine.InputSystem;

//Code borrowed and Modified by Dan Pos off of the inventory system series from youtube https://www.youtube.com/playlist?list=PL-hj540P5Q1hLK7NS5fTSNYoNJpPWSL24
public class Interactor : MonoBehaviour
{
    public Transform interactionPoint;
    public LayerMask shopLayer;
    public LayerMask craftingLayer;
    public float interactionPointRadius = 1f;
    
    public GameObject craftingPanel;
    public GameObject shopPanel;

    [SerializeField] private Vector3 shopResetPosition;
    [SerializeField] private Vector3 craftingResetPosition;
    public Vector3Data resetPositions;
    public Transform playerTransform;

    private bool isInCraftingOrShop = false;

    public bool isInteracting { get; private set; }

    private void Update()
    {
        Collider[] shopColliders = Physics.OverlapSphere(interactionPoint.position, interactionPointRadius, shopLayer);
        Collider[] craftingColliders = Physics.OverlapSphere(interactionPoint.position, interactionPointRadius, craftingLayer);
        Collider[] colliders = new Collider[shopColliders.Length + craftingColliders.Length];
        shopColliders.CopyTo(colliders, 0);
        craftingColliders.CopyTo(colliders, shopColliders.Length);

        if (Keyboard.current.aKey.wasPressedThisFrame || Input.GetMouseButtonDown(0))
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                var interactable = colliders[i].GetComponent<IInteractable>();
                if (interactable != null) StartInteraction(interactable);
            }
        }
    }

    void StartInteraction(IInteractable interactable)
    {
        interactable.Interact(this, out bool interactSuccessful);
        isInteracting = true;
    }

    void EndInteraction(IInteractable interactable)
    {
        isInteracting = false;
    }
    
    public void ShopBackButtonPressed()
    {
        shopPanel.SetActive(false);
        playerTransform.position = resetPositions.shopResetPosition; // Move the player to the specified position from the ScriptableObject
        isInCraftingOrShop = false;
    }

    public void CraftingBackButtonPressed()
    {
        craftingPanel.SetActive(false);
        playerTransform.position = resetPositions.craftingResetPosition; // Move the player to the specified position from the ScriptableObject
        isInCraftingOrShop = false;
    }
}