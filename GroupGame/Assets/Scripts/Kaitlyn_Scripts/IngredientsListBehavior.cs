using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientsListBehavior : MonoBehaviour
{
    private List<GameObject> ingredientList = new List<GameObject>();

    public void AddObjectToList(GameObject obj)
    {
        ingredientList.Add(obj);
        
		//debug logs for testing purposes
        Debug.Log("Added " + obj.name + " to the list.");
        Debug.Log("item added: " + ingredientList.Count);
    }
}
