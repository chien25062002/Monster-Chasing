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
        } else if (owner.CompareTag("Pet")) {
            damage = (int) (Pet.instance.GetDamage() * damagePercent);
        } else {
            damage = (int) (owner.GetComponent<Mob>().GetDamage() * damagePercent);
        }
        GamePanel.instance.DrawDamge(damage.ToString(), other.transform);
        if (owner.CompareTag("Player") || owner.CompareTag("Pet")) {
            int ReceivePotential = caculateReceivedPotential(damage);
            PlayerController.instance.character.ReceivePotential(ReceivePotential);
            if (owner.CompareTag("Pet")) {
                Pet.instance.ReceivePotential(ReceivePotential);
            }
            GamePanel.instance.DrawReceivedPotential(ReceivePotential, owner.transform);
        }
        other.GetComponent<DamageReceiver>().GetDamage(damage, owner.transform);
    }

    private int caculateReceivedPotential(int damage) {
        return damage * 2;
    }
}
