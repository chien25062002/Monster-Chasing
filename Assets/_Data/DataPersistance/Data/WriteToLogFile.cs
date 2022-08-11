using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class WriteToLogFile : MonoBehaviour
{
    string fileName = "";
    
    private void OnEnable() {
        Application.logMessageReceived += Log;
    }

    private void OnDisable() {
        Application.logMessageReceived -= Log;
    }

    private void Start() {
        fileName = Application.persistentDataPath + "/LogFile.txt";
    }

    public void Log(string logString ,string stackTrace, LogType type) {
        TextWriter tw = new StreamWriter(fileName, true);
        tw.WriteLine("[" + System.DateTime.Now + "] " + logString + "stack trace: " + stackTrace);
        tw.Close();
    }
}
