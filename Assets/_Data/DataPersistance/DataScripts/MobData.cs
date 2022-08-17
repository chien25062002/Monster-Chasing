using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MobData
{
    public string name;
    public int healthPoint;
    public int manaPoint;
    public int damage;
    public int def;
    public int crit;
    public int speed;
    public bool isBoss;

    public MobData() {

    }

    public MobData(string[] data) {
        name = data[0];
        healthPoint = int.Parse(data[1]);
        manaPoint = int.Parse(data[2]);
        damage = int.Parse(data[3]);
        def = int.Parse(data[4]);
        crit = int.Parse(data[5]);
        speed = int.Parse(data[6]);
        if (int.Parse(data[7]) == 1) {
            isBoss = true;
        } else {
            isBoss = false;
        }
    }

    public override string ToString()
    {
        string data = "";
        data += "name: " + this.name;
        data += "\nHealthpoint: " + this.healthPoint;
        data += "\nManaPoint: " + this.manaPoint;
        data += "\nDamage: " + this.damage;
        data += "\nDef: " + this.def;
        data += "\nCrit: " + this.crit;
        data += "\nSpeed: " + this.speed;
        return data;
    }
}
