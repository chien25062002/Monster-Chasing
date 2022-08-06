using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveX;
    public float moveY;

    // Update is called once per frame
    void Update()
    {
        Character character = PlayerController.instance.character;
        if (character.IsAttacking)
            return;
        if (moveX != 0 && character.isGrounded) {
            character.CurrentState = Character.RUN_STATE;
            character.isMoving = true;
            character.isStanding = false;
        }
        if (moveX > 0) {
            character.faceSide = 1f;
        } else if (moveX < 0) {
            character.faceSide = -1f;
        } else {
            character.isMoving = false;
            character.CurrentState = Character.STAND_STATE;
            character.isStanding = true;
        }

        if (character.isGrounded) {
            if (moveY >= 0.5) {
                character.GetComponent<Rigidbody2D>().gravityScale = 0f;
                character.isGrounded = false;
            }
            if (moveY <= 0) {
                moveY = 0;
            }
        } else {
            character.CurrentState = Character.FLY_STATE;
            character.isFlying = true;
            if (moveY == 0) {
                character.GetComponent<Rigidbody2D>().gravityScale = 1.5f;
                character.CurrentState = Character.STAND_STATE;
            }
        }
        
        character.moveX = moveX;
        character.moveY = moveY;
        character.Move();
        UpdateAnimation();
    }

    protected virtual void UpdateAnimation() {
        PlayerController.instance.anim.SetInteger(Character.PLAYER_STATE, PlayerController.instance.character.gameObject.GetComponent<Character>().CurrentState);
    }
}
