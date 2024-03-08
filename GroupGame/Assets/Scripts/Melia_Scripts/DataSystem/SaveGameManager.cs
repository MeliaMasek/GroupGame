using UnityEngine;

//Code borrowed and Modified by Dan Pos off of the inventory system series from youtube https://www.youtube.com/playlist?list=PL-hj540P5Q1hLK7NS5fTSNYoNJpPWSL24
public class SaveGameManager : MonoBehaviour
{
    public static SaveData data;
    
    private void Awake()
    {
        data = new SaveData();
        SaveLoad.onLoadGame += LoadData;
    }

    public static void DeleteData()
    {
        SaveLoad.DeleteSaveData();
    }
    
    public static void SaveData()
    {
        var saveData = data;
        SaveLoad.Save(saveData);
    }   
    private static void LoadData(SaveData _data)
    {
        data = _data;
    }
    
    public static void TryToLoadData()
    {
        SaveLoad.Load();
        Debug.Log("Loaded Data");
    }
}
