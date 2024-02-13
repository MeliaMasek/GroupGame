using UnityEngine;
using UnityEngine.EventSystems;

//code borrowed and modified from Stackoverflow https://gamedev.stackexchange.com/questions/180033/best-way-make-static-gameobject-clickable-2d#:~:text=1%20Answer&text=If%20you%20are%20trying%20to,is%20the%20way%20to%20go.&text=Use%20the%20LayerMask%20parameter%20of,You%20can%20then%20call%20hit.
public class GameobjectInteractionBehaviour : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject CraftingMenu;

    public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                Debug.Log("Clicked " + name);
                Crafting();
            }
        }
    public void Crafting()
    {
        CraftingMenu.SetActive(true);
        Time.timeScale = 0f;
    }
}
