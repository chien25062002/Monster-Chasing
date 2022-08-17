using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadDataFromResources
{
    private static string dataPathDir = "_FixData/";
    private static string splitCharacter = ";";
    public static LoadDataFromResources instance;

    public static List<T> Load<T>(string dataFileName) {
        List<T> listDataLoaded = new List<T>();
        var dataToLoad = Resources.Load<TextAsset>(dataPathDir + dataFileName);
        string[] loadedDatas = dataToLoad.ToString().Split(";");
        foreach (string data in loadedDatas) {
            listDataLoaded.Add(JsonUtility.FromJson<T>(data));
        }
        return listDataLoaded;
    }
}
