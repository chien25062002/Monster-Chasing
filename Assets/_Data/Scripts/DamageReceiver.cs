using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageReceiver : MonoBehaviour
{
    public void GetDamage(int damage) {
        if (gameObject.CompareTag("Player")) {
            gameObject.GetComponent<Character>().GetDamage(damage);
        } else {
            gameObject.GetComponent<Mob>().GetDamage(damage);
        }
    }
}
