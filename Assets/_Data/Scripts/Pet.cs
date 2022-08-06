using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pet : Character
{
    public static Pet instance;
    public bool isMovingToRight;
    public Vector3 targetPos;
    public bool standFly;
    public float standFlyTime = 4;
    public float standFlyTimer;

    protected override void Awake()
    {
        if (instance != null)
            Debug.Log("Only 1 instance is allowed to be created");
        instance = this;
        DontDestroyOnLoad(gameObject);
        isMoving = false;
        isMovingToRight = true;
    }

    private void Update() {
        //OnHeadOfCharacter();
    }

    private void FixedUpdate() {
        LookAtOwner();
    }

    private void LateUpdate() {
        MovingAroundCharacterHead();
    }

    public void LookAtOwner() {
        this.faceSide = PlayerController.instance.character.faceSide;
        TurnFace();
    }

    public void MovingAroundCharacterHead() {
        Vector3 charPos = PlayerController.instance.character.transform.position;
        Vector3 petPos = charPos;
        petPos.y += 4;
        if (PlayerController.instance.character.isStanding) {
            if (isMovingToRight) {
                if (!isMoving) {
                    petPos.x += 7;
                    targetPos = petPos;
                    isMoving = true;
                }
                if (transform.position.x >= targetPos.x - 1) {
                    standFlyTimer += Time.deltaTime;
                    if (standFlyTimer >= standFlyTime) {
                        standFlyTimer = 0;
                        isMovingToRight = false;
                        isMoving = false;
                    }
                }
            }
            if (!isMovingToRight) {
                if (!isMoving) {
                    petPos.x -= 7;
                    targetPos = petPos;
                    isMoving = true;
                }
                if (transform.position.x <= targetPos.x + 1) {
                    standFlyTimer += Time.deltaTime;
                    if (standFlyTimer >= standFlyTime) {
                        standFlyTimer = 0;
                        isMovingToRight = true;
                        isMoving = false;
                    }
                }
            }
        }
        else {
            petPos.x -= 3;
            targetPos = petPos;
        }
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime);
    }
}
