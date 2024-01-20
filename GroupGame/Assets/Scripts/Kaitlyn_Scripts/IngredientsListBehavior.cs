using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientsListBehavior : MonoBehaviour
{
    private List<GameObject> ingredientList = new List<GameObject>();

    private void AddIngredientToList(GameObject obj)
    {
        ingredientList.Add(obj);
    }
}
