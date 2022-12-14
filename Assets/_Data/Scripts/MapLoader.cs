using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapLoader : MyMonoBehaviour
{
    public static MapLoader instance;

    protected override void Awake()
    {
        base.Awake();
        if (instance != null)
            Debug.LogError("Only 1 LevelLoader instance is allowed to be created");
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void LoadMap(int mapIndex) {
        GameScreen.instance.SetPanel(GameScreen.LOADING_GAME_SCREEN_PANEL);
        StartCoroutine(LoadAsynchronously(mapIndex));
    }

    IEnumerator LoadAsynchronously (int mapIndex) {
        AsyncOperation operation = SceneManager.LoadSceneAsync(mapIndex);
        while (!operation.isDone) {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            GameScreen.instance.loadingScreenPanel.SetSliderValue(progress);

            yield return null;
        }
        CreateMobInLoadedMap(mapIndex);
        LoadTileMap(mapIndex);
        Vector3 characterPos;
        if (GameManager.instance.isNewGame) {
            characterPos = PlayerController.instance.character.transform.position;
            characterPos.y = 0;
            PlayerController.instance.character.currentMapId = 1;
            PlayerController.instance.character.lastMapIndex = 1;
            GameManager.instance.isNewGame = false;
        } else {
            PlayerController.instance.character.currentMapId = mapIndex;
            Transform waypoint;
            if (!PlayerController.instance.character.goHome) {
                waypoint = MapScreen.instance.GetWaypointWithLastId(PlayerController.instance.character.lastMapIndex);
                characterPos = waypoint.GetComponent<WaypointUI>().charPos;
            } else {
                waypoint = MapScreen.instance.GetWaypointWithLastId(1);
                PlayerController.instance.character.lastMapIndex = 1;
                PlayerController.instance.character.goHome = false;
                characterPos = new Vector3(0, 0, 0);
            }
        }
        Pet.instance.transform.position = characterPos;
        PlayerController.instance.character.transform.position = characterPos;
        GameScreen.instance.SetPanel(GameScreen.GAME_PANEL);
    }

    protected virtual void CreateMobInLoadedMap(int mapIndex) {
        if (mapIndex == 1)
            return;
        Map loadedMap = MapManager.GetInstance().GetMapByIndex(mapIndex);
        foreach (MobPositionInMap mobPosition in loadedMap.mobPositions) {
            if (mobPosition.mapIndex == mapIndex) {
                Transform spawnedMob = MobManager.instance.SpawnMobByName(mobPosition.mobName);
                spawnedMob.position = new Vector3(mobPosition.x, mobPosition.y, mobPosition.z);
                Mob mob = spawnedMob.GetComponent<Mob>();
                mob.spawnPosition = spawnedMob.position;
                if (mob.isBoss) {
                    Boss boss = (Boss) mob;
                    GameObject healthBar = Instantiate(Resources.Load("Prefabs/BossHealthBar") as GameObject);
                    healthBar.transform.GetComponent<BossHealthBar>().SetMaxHealth(mob.GetHealthPointHolder());
                    healthBar.transform.GetComponent<BossHealthBar>().owner = spawnedMob;
                    healthBar.transform.SetParent(MapScreen.instance.transform, false);
                    boss.bossHealthBar = healthBar.transform.GetComponent<BossHealthBar>();
                    
                }
                mob.RandomSkill();
                spawnedMob.gameObject.SetActive(true);
                loadedMap.spawnedMobs.Add(spawnedMob);
            }
        }
        GameManager.instance.currentMap = loadedMap;
    }

    protected virtual void LoadTileMap(int mapIndex) {
        LoadWaypoint(mapIndex);
    }

    protected virtual void LoadWaypoint(int mapIndex) {
        MapScreen.instance.ClearWaypoint();
        foreach (Waypoint waypoint in GameData.GetInstance().waypoints) {
            if (waypoint.currentMapId == mapIndex) {
                GameObject waypointObject = Instantiate(Resources.Load("Prefabs/Waypoint") as GameObject);
                waypointObject.transform.SetParent(MapScreen.instance.gameObject.transform, true);
                waypointObject.GetComponent<WaypointUI>().SetData(waypoint);
                MapScreen.instance.AddWaypoint(waypointObject.GetComponent<WaypointUI>());
                waypointObject.transform.localScale = new Vector3(1f, 1f, 1f);
            }
        }
    }
}
