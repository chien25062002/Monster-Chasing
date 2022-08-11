using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill
{
    public const int KIEMKHI_TYPE = 1;
    public const int AMKHI_TYPE = 2;

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
    public string imageName;

    public Skill() {
 
    }

    public Skill Clone() {
        Skill cloneSkill = new Skill();
        cloneSkill.id = id;;
        cloneSkill.skillTemplateId = skillTemplateId;
        cloneSkill.skillName = skillName;
        cloneSkill.manaUse = manaUse;
        cloneSkill.powerRequire = powerRequire;
        cloneSkill.level = level;
        cloneSkill.cooldown = cooldown;
        cloneSkill.damage = damage;
        cloneSkill.maxFight = maxFight;
        cloneSkill.price = price;
        cloneSkill.effectName = effectName;
        cloneSkill.referenceObjectName = referenceObjectName;
        cloneSkill.imageName = imageName;
        return cloneSkill;
    }

    public Skill(string[] data) {
        id = int.Parse(data[0]);
        skillTemplateId = int.Parse(data[1]);
        skillName = data[2];
        manaUse = mSystem.ParseFloat(data[3]);
        powerRequire = int.Parse(data[4]);
        level = int.Parse(data[5]);
        cooldown = mSystem.ParseFloat(data[6]);
        damage = int.Parse(data[7]);
        maxFight = int.Parse(data[8]);
        price = int.Parse(data[9]);
        effectName = data[10];
        referenceObjectName = data[11];
        imageName = data[12].Remove(data[12].Length - 1); // remove the illegal character at the last
    }

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
    }
}
