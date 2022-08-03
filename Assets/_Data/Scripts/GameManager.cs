using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MyMonoBehaviour
{
    public static GameManager instance;

    public Map currentMap = null;
    public bool isNewGame;

    protected override void Awake()
    {
        base.Awake();
        if (instance != null)
            Debug.LogError("Only 1 GameManager instance is allowed to be created.");
        instance = this;
        DontDestroyOnLoad(gameObject);
        isNewGame = true;
    }

    protected override void LoadComponents()
    {
        this.LoadFixedData();
    }
    
    protected virtual void LoadFixedData() {
        DataPersistanceManager.GetInstance().LoadGame();
        MobManager.instance.LoadMobData();
    }

    public void LoadMap(int mapIndex) {

        MapLoader.instance.LoadMap(mapIndex);
    }

}
