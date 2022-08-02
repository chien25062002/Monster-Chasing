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
        Vector3 characterPos = PlayerController.instance.character.transform.position;
        characterPos.y = 0;
        PlayerController.instance.character.transform.position = characterPos;
        GameScreen.instance.SetPanel(GameScreen.GAME_PANEL);
    }

    // tạo quái đây a
    protected virtual void CreateMobInLoadedMap(int mapIndex) {
        int id = 1;
        Map loadedMap = MapManager.GetInstance().GetMapByIndex(mapIndex);
        foreach (MobPositionInMap mobPosition in loadedMap.mobPositions) {
            if (mobPosition.mapIndex == mapIndex) {
                Transform spawnedMob = MobManager.instance.SpawnMobByName(mobPosition.mobName);
                spawnedMob.GetComponent<Mob>().id = id++;
                spawnedMob.position = new Vector3(mobPosition.x, mobPosition.y, mobPosition.z);
                spawnedMob.GetComponent<Mob>().spawnPosition = spawnedMob.position;
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
        foreach (Waypoint waypoint in GameData.GetInstance().waypoints) {
            if (waypoint.currentMapId == mapIndex) {
                GameObject waypointObject = Instantiate(Resources.Load("Prefabs/Waypoint") as GameObject);
                waypointObject.transform.parent = MapScreen.instance.gameObject.transform;
                waypointObject.GetComponent<WaypointUI>().SetData(waypoint);
                MapScreen.instance.AddWaypoint(waypointObject.GetComponent<WaypointUI>());
                waypointObject.transform.localScale = new Vector3(1f, 1f, 1f);
                //waypointObject.SetActive(false);
            }
        }
    }
}
