using UnityEngine;
using UnityEngine.UI;

//Code borrowed and Modified by Dan Pos off of the inventory system series from youtube https://www.youtube.com/playlist?list=PL-hj540P5Q1hLK7NS5fTSNYoNJpPWSL24

public class MouseItemData : MonoBehaviour
{
    public Image itemSprite;
    public Text ItemCount;

    private void Awake()
    {
        itemSprite.color = Color.clear;
        ItemCount.text = "";
    }
}
