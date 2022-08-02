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
        x = float.Parse(data[3]);
        y = float.Parse(data[4]);
        z = float.Parse(data[5]);
    }
}
