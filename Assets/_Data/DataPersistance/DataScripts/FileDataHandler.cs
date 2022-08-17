using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class FileDataHandler
{
    private string dataDirPath = "Assets/_Data/DataPersistance/DataFile/";
    private string splitCharacter = ";";


    public static FileDataHandler instance;

    public static FileDataHandler GetInstance() {
        if (instance == null)
            instance = new FileDataHandler();
        return instance;
    }


    public List<T> Load<T>(string dataFileName) {
        string fullPath = Path.Combine(this.dataDirPath, dataFileName);
        List<T> listDataLoaded = new List<T>();
        if (File.Exists(fullPath)) {
            try {
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open)) {
                    using (StreamReader reader = new StreamReader(stream)) {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                string[] loadedDatas = dataToLoad.Split(splitCharacter);
                
                foreach (string data in loadedDatas) {
                    listDataLoaded.Add(JsonUtility.FromJson<T>(data));
                }
            } catch (Exception e) {
                Debug.LogError("Error occurred when trying to load game data to file: " + fullPath + "\n" + e);
            }
        }
        Debug.Log("List: " + listDataLoaded.Count);
        return listDataLoaded;
    }

    public void Save<T> (T data, string dataFileName) {
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        Debug.Log("Path: " + fullPath);
        try {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            string dataToStore = JsonUtility.ToJson(data, true);

            using (FileStream stream = new FileStream(fullPath, FileMode.Create)) {
                using (StreamWriter writer = new StreamWriter(stream)) {
                    writer.Write(dataToStore);
                }
            }
        } catch (Exception e) {
            Debug.LogError("Error occurred when trying to save game data to file: " + fullPath + "\n" + e);
        }

    }
}
