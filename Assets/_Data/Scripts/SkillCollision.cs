using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCollision : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other) {
        if (gameObject.GetComponent<Weapon>().owner == other.gameObject) return;
        if (gameObject.GetComponent<Weapon>().target != other.gameObject) return;
        Vector3 spawnedEffPos = other.transform.position;
        spawnedEffPos.y += 1.5f;
        gameObject.GetComponent<ObjectSkill>().SpawnEffect(spawnedEffPos);
        EffectMn.instance.SpawnEffect("BloodSplash", other.transform.position + new Vector3(3.5f, 2, 0));
        gameObject.GetComponent<DamageSender>().SendDamage(other.gameObject);
        Destroy(gameObject);
    }
}
