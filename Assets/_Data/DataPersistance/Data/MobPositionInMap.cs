using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MobPositionInMap
{
    public string mapName;
    public int mapIndex;
    public string mobName;
    public float x;
    public float y;
    public float z;

    public MobPositionInMap() {

    }

    public MobPositionInMap(string[] data) {
        mapName = data[0];
        mapIndex = int.Parse(data[1]);
        mobName = data[2];
        x = mSystem.ParseFloat(data[3]);
        y = mSystem.ParseFloat(data[4]);
        z = mSystem.ParseFloat(data[5]);
    }
}
