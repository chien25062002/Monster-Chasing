using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageReceiver : MonoBehaviour
{
    public void GetDamage(int damage, Transform makeHurtObject) {
        if (gameObject.CompareTag("Player")) {
            gameObject.GetComponent<Character>().GetDamage(damage, makeHurtObject);
        } else if (gameObject.CompareTag("Pet")) {
            gameObject.GetComponent<Pet>().GetDamage(damage, makeHurtObject);
        } else {
            gameObject.GetComponent<Mob>().GetDamage(damage, makeHurtObject);
        }
    }
}
