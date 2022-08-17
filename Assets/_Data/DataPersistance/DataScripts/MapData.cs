using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class MapData
{
    public int mapIndex;
    public string mapName;

    public MapData() {

    }

    public MapData(string[] data) {
        mapIndex = int.Parse(data[0]);
        mapName = data[1];
    }
}
