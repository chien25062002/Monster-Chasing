using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map
{
    public List<Mob> mobs = new List<Mob>();

    public int mapIndex;
    public string mapName;
    public List<MobPositionInMap> mobPositions = new List<MobPositionInMap>();
    public List<Transform> spawnedMobs = new List<Transform>();
}
