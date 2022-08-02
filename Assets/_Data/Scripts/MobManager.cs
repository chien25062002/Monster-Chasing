using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobManager : MyMonoBehaviour
{
    public static MobManager instance;

    public List<Mob> mobs = new List<Mob>();

    protected override void Awake()
    {
        base.Awake();
        if (instance != null)
            Debug.Log("Only 1 MobManager instance is allowed to be created");
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadAllMobTemplate();
        DisableAllMob();
    }

    protected virtual void LoadAllMobTemplate() {
        if (mobs.Count > 0) return;
        foreach (Transform child in this.transform) {
            mobs.Add(child.GetComponent<Mob>());
        }
    }

    protected virtual void DisableAllMob() {
        foreach (Mob mob in this.mobs) {
            mob.gameObject.SetActive(false);
        }
    }

    public virtual void LoadMobData() {
        foreach (MobData mobData in GameData.GetInstance().mobDatas) {
            foreach (Mob mob in this.mobs)
                if (mobData.name == mob.gameObject.name) {
                    mob.SetData(mobData);
                    break;
                }
        }
    }

    public Transform SpawnMobByName(string name) {
        Transform spawnedMob;
        GameObject spawnedObject;
        foreach (Mob mob in mobs) {
            if (mob.gameObject.name == name) {
                spawnedObject = Instantiate(mob.gameObject);
                spawnedMob = spawnedObject.transform;
                return spawnedMob;
            }
        }
        return null;
    }
}
