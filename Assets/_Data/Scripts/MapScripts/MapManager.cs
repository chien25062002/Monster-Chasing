using System.Collections;
using System.Collections.Generic;

public class MapManager
{
    public static MapManager instance;

    public static MapManager GetInstance() {
        if (instance == null)
            instance = new MapManager();
        return instance;
    }

    public List<Map> maps = new List<Map>();

    public Map GetMapByName(string mapName) {
        foreach (Map map in maps) {
            if (map.mapName == mapName)
                return map;
        }
        return null;
    }

    public Map GetMapByIndex(int mapIndex) {
        foreach (Map map in maps) {
            if (map.mapIndex == mapIndex)
                return map;
        }
        return null;
    }
}
