using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCollision : MonoBehaviour
{
    public const string GROUND_TAG = "Ground";
    public const string GOLD_TAG = "GoldCoin";
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag(GROUND_TAG)) {
            gameObject.GetComponent<Character>().isGrounded = true;
            gameObject.GetComponent<Character>().isFlying = false;
        }
        if (other.gameObject.CompareTag(GOLD_TAG)) {
            int goldValue = other.gameObject.GetComponent<GoldCoin>().goldValue;
            PlayerController.instance.character.ReceiveGoldCoin(goldValue);
            GamePanel.instance.DrawReceivedGoldCoin(goldValue, transform);
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.CompareTag(GROUND_TAG)) {
            gameObject.GetComponent<Character>().isGrounded = false;
        }
    }
}
