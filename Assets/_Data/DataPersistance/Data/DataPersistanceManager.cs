using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPersistanceManager
{
    public const string MOB_DATA_FILE = "MobData";
    public const string MOB_POSITION_IN_MAP_FILE = "MobInMap";
    public const string MAP_DATA_FILE = "MapData";

    public static DataPersistanceManager instance;

    private FileDataHandler dataHandler = FileDataHandler.GetInstance();

    public static DataPersistanceManager GetInstance() {
        if (instance == null)
            instance = new DataPersistanceManager();
        return instance;
    }

    public void NewGame() {
        //this.gameData = new MobData();
    }

    public void LoadGame() {
        this.LoadMobDataFromResources();
        this.LoadMapFromResources();
        this.LoadSkillData();
        this.LoadDefaultChracterData();
        this.LoadWaypoints();
    }

    public void LoadWaypoints() {
        GameData gameData = GameData.GetInstance();
        gameData.waypoints = CSVReader.LoadWaypoints();
    }

    protected virtual void LoadDefaultChracterData() {
        PlayerController.instance.character.UpdateData(CSVReader.LoadDefaultCharacterData());
    }

    protected virtual void LoadSkillData() {
        GameData gameData = GameData.GetInstance();
        gameData.skills = CSVReader.LoadSkill();
        gameData.skillTemplates = CSVReader.LoadSkillTemplate();
        foreach (Skill skill in gameData.skills) {
            Transform skillObject = SkillManager.instance.GetSkillReferenceByName(skill.referenceObjectName);
            skillObject.gameObject.GetComponent<ObjectSkill>().SetData(skill);

            foreach (SkillTemplate skillTemplate in gameData.skillTemplates)
                if (skill.skillTemplateId == skillTemplate.id)
                    skillTemplate.skills.Add(skill);
        }
    }

    protected virtual void LoadMobDataFromResources() {
        GameData.GetInstance().mobDatas = CSVReader.LoadMobData();
    }

    protected virtual void LoadMapFromResources() {
        List<MobPositionInMap> listMobPositionInMap = CSVReader.LoadMobPosition();
        List<MapData> listMapData = CSVReader.LoadMapData();

        foreach (MapData mapData in listMapData) {
            Map map = new Map();
            
            map.mapName = mapData.mapName;
            map.mapIndex = mapData.mapIndex;
            foreach (MobPositionInMap mobPositionInMap in listMobPositionInMap) {
                if (map.mapIndex == mobPositionInMap.mapIndex)
                    map.mobPositions.Add(mobPositionInMap);
            }
            GameData.GetInstance().maps.Add(map);
            MapManager.GetInstance().maps.Add(map);
        }
    }
    
    public void SaveGame() {
       
    }
}
