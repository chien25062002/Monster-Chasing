using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CSVReader
{
    public static string dataPathDir = "_FixData/";
    public static char splitLineCharacter = '\n';
    public static char splitColumnCharacter = ',';
    public static string MOB_DATA_FILE = "MobData";
    public static string MAP_DATA_FILE = "MapData";
    public static string MOB_IN_MAP = "MobInMap";
    public static string SKILL_TEMPLATE_FILE = "SkillTemplate";
    public static string SKILL_FILE = "Skill";
    public static string CHARACTER_FILE = "Character";
    public static string WAYPOINT_FILE = "Waypoint";

    public static string[] Read (string fileName) {
        string fullPath = dataPathDir + fileName;
        TextAsset dataToRead = Resources.Load<TextAsset>(fullPath);
        string[] data = dataToRead.text.Split(splitLineCharacter);
        return data;
    }

    public static List<MobData> LoadMobData() {
        List<MobData> mobDatas = new List<MobData>();
        string[] readedData = Read(MOB_DATA_FILE);
        for (int i = 1; i < readedData.Length - 1; i++) {
            string[] row = readedData[i].Split(splitColumnCharacter);
            MobData mobData = new MobData(row);
            mobDatas.Add(mobData);
        }
        return mobDatas;
    }

    public static List<MapData> LoadMapData() {
        List<MapData> mapDatas = new List<MapData>();
        string[] readedData = Read(MAP_DATA_FILE);
        for (int i = 1; i < readedData.Length - 1; i++) {
            string[] row = readedData[i].Split(splitColumnCharacter);
            MapData mapData = new MapData(row);
            mapDatas.Add(mapData);
        }
        return mapDatas;
    }

    public static List<SkillTemplate> LoadSkillTemplate() {
        List<SkillTemplate> loadedDatas = new List<SkillTemplate>();
        string[] readedData = Read(SKILL_TEMPLATE_FILE);
        for (int i = 1; i < readedData.Length - 1; i++) {
            string[] row = readedData[i].Split(splitColumnCharacter);
            SkillTemplate loadedData = new SkillTemplate(row);
            loadedDatas.Add(loadedData);
        }
        return loadedDatas;
    }

    public static List<Skill> LoadSkill() {
        List<Skill> loadedDatas = new List<Skill>();
        string[] readedData = Read(SKILL_FILE);
        for (int i = 1; i < readedData.Length - 1; i++) {
            string[] row = readedData[i].Split(splitColumnCharacter);
            Skill loadedData = new Skill(row);
            loadedDatas.Add(loadedData);
        }
        return loadedDatas;
    }
    public static List<MobPositionInMap> LoadMobPosition() {
        List<MobPositionInMap> loadedDatas = new List<MobPositionInMap>();
        string[] readedData = Read(MOB_IN_MAP);
        for (int i = 1; i < readedData.Length - 1; i++) {
            string[] row = readedData[i].Split(splitColumnCharacter);
            MobPositionInMap loadedData = new MobPositionInMap(row);
            loadedDatas.Add(loadedData);
        }
        return loadedDatas;
    }

    public static string[] LoadDefaultCharacterData() {
        string[] readedData = Read(CHARACTER_FILE);
        return readedData[1].Split(splitColumnCharacter);
    }

    public static List<Waypoint> LoadWaypoints() {
        List<Waypoint> loadedDatas = new List<Waypoint>();
        string[] readedData = Read(WAYPOINT_FILE);
        for (int i = 1; i < readedData.Length - 1; i++) {
            string[] row = readedData[i].Split(splitColumnCharacter);
            Waypoint loadedData = new Waypoint(row);
            loadedDatas.Add(loadedData);
        }
        return loadedDatas;
    }
}
