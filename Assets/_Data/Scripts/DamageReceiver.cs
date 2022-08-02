using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageReceiver : MonoBehaviour
{
    public void GetDamage(int damage) {
        gameObject.GetComponent<Mob>().GetDamage(damage);
    }
}
