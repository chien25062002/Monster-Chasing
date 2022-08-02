using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSender : MonoBehaviour
{
    public void SendDamage(GameObject other) {
        float damagePercent = this.gameObject.GetComponent<ObjectSkill>().damage / 100f;
        int damage = (int) (PlayerController.instance.character.GetDamage() * damagePercent);
        other.GetComponent<DamageReceiver>().GetDamage(damage);
    }
}
