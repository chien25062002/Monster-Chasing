    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MyMonoBehaviour
{
    public static SkillManager instance;

    public List<Transform> skills = new List<Transform>();

    protected override void Awake()
    {
        base.Awake();
        if (instance != null)
            Debug.Log("Only 1 SkillManager instance is allowed to be created");
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    protected override void LoadComponents()
    {
        LoadAllSkill();
        HideAllSkill();
    }

    protected virtual void LoadAllSkill() {
        if (skills.Count > 0) return;
        foreach (Transform child in transform) {
            skills.Add(child);
        }
    }

    public Transform SpawnSkill(string skillName) {
        Transform spawnedSkill;
        GameObject spawnedObject;
        foreach (Transform skill in skills) {
            if (skillName.Contains(skill.name)) {
                spawnedObject = Instantiate(skill.gameObject);
                spawnedSkill = spawnedObject.transform;
                return spawnedSkill;
            }
        }
        return null;
    }

    public Transform SpawnSkillById(int id) {
        Transform spawnedSkill;
        GameObject spawnedObject;
        foreach (Transform skill in skills) {
            if (skill.GetComponent<ObjectSkill>().id == id) {
                spawnedObject = Instantiate(skill.gameObject);
                spawnedObject.transform.localScale = skill.localScale;
                spawnedSkill = spawnedObject.transform;
                return spawnedSkill;
            }
        }
        return null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected virtual void HideAllSkill() {
        foreach (Transform skill in skills) {
            skill.gameObject.SetActive(false);
        }
    }

    public Transform GetSkillReferenceByName(string name) {
        foreach (Transform skill in skills) {
            if (name.Contains(skill.gameObject.name)) {
                return skill;
            }
        }
        return null;
    }
}
