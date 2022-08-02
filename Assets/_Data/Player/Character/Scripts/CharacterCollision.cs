using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCollision : MonoBehaviour
{
    public const string GROUND_TAG = "Ground";
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag(GROUND_TAG)) {
            gameObject.GetComponent<Character>().isGrounded = true;
            gameObject.GetComponent<Character>().isFlying = false;
        }
    }
}
