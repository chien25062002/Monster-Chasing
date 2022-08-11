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
        DrawDamge(damage.ToString(), other.transform.position.x, other.transform.position.y - 10);
        other.GetComponent<DamageReceiver>().GetDamage(damage, owner.transform);
    }

    protected virtual void DrawDamge(string damage, float x, float y) {
        GameObject flyText = Instantiate(Resources.Load("Prefabs/FlyText") as GameObject);
        flyText.transform.SetParent(GameScreen.instance.gamePanel.transform, true);
        flyText.GetComponent<FlyText>().mFont_Roboto(damage, x, y, FlyText.COLOR_RED);
    }
}
