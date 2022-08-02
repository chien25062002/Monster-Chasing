using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectMn : MyMonoBehaviour
{
    public static EffectMn instance;

    public List<Transform> effects = new List<Transform>();

    protected override void Awake()
    {
        base.Awake();
        if (instance != null)
            Debug.Log("Only 1 EffectMn instance is allowed to be created");
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    protected override void LoadComponents()
    {
        LoadAllEffect();
        HideAllEffect();
    }

    protected virtual void LoadAllEffect() {
        if (effects.Count > 0) return;
        foreach (Transform child in transform) {
            effects.Add(child);
        }
    }

    public Transform SpawnEffect(string effectName) {
        Transform spawnedEffect;
        foreach (Transform effect in effects) {
            if (effect.name == effectName) {
                spawnedEffect = Instantiate(effect);
                return spawnedEffect;
            }
        }
        return null;
    }

    public void SpawnEffect(string effectName, Vector3 spawnPos) {
        Transform spwawnedEffect = this.SpawnEffect(effectName);
        spwawnedEffect.position = spawnPos;
        spwawnedEffect.gameObject.SetActive(true);
    }

    public Transform GetEffectByName(string name) {
        foreach (Transform effect in effects)
            if (name.Contains(effect.gameObject.name))
                return effect;
        return null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected virtual void HideAllEffect() {
        foreach (Transform effect in effects) {
            effect.gameObject.SetActive(false);
        }
    }
}
