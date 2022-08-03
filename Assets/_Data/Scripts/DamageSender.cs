using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSender : MonoBehaviour
{
    public void SendDamage(GameObject other) {
        float damagePercent = this.gameObject.GetComponent<ObjectSkill>().damage / 100f;
        int damage;
        GameObject owner = gameObject.GetComponent<Weapon>().owner;
        if (owner.CompareTag("Player")) {
            damage = (int) (PlayerController.instance.character.GetDamage() * damagePercent);
        } else {
            damage = (int) (owner.GetComponent<Mob>().GetDamage() * damagePercent);
        }
        other.GetComponent<DamageReceiver>().GetDamage(damage);
    }
}
