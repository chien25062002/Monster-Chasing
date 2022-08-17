using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSkill : MonoBehaviour
{
    public static int CANCHIEN_TYPE = 1;
    public static int DANHXA_TYPE = 2;
    public int id;
    public int skillTemplateId;
    public string skillName;
    public float manaUse;
    public int powerRequire;
    public int level;
    public float cooldown;
    public int damage;
    public int maxFight;
    public int price;
    public string effectName;
    public string referenceObjectName;
    public float cooldownTimer;
    public bool isCooldown;
    public Transform effect;
    public string imageName;
    public SkillTemplate skillTemplate;

    public void SetData(Skill skill) {
        this.skillName = skill.skillName;
        this.id = skill.id;
        this.skillTemplateId = skill.skillTemplateId;
        this.manaUse = skill.manaUse;
        this.powerRequire = skill.powerRequire;
        this.level = skill.level;
        this.cooldown = skill.cooldown;
        this.damage = skill.damage;
        this.maxFight = skill.maxFight;
        this.price = skill.price;
        this.effectName = skill.effectName;
        this.referenceObjectName = skill.referenceObjectName;
        this.imageName = skill.imageName;
        skillTemplate = skill.skillTemplate;

        effect = EffectMn.instance.GetEffectByName(effectName);
    }

    public void SpawnEffect() {
        Transform spwawnedEffect = EffectMn.instance.SpawnEffect(effectName);
        spwawnedEffect.position = transform.position;
        spwawnedEffect.gameObject.SetActive(true);
    }

    public void SpawnEffect(Vector3 spawnPos) {
        Transform spwawnedEffect = EffectMn.instance.SpawnEffect(effectName);
        spwawnedEffect.position = spawnPos;
        spwawnedEffect.gameObject.SetActive(true);
    }
}
