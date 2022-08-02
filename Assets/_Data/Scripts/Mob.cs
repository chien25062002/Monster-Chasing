using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : MapObject
{
    public int id;
    public const string MOB_STATE = "State";
    public const int FLY_TYPE = 0;
    public const int STAND_TYPE = 1;

    protected int mobType = STAND_TYPE;
    protected float standTime = 3f;
    protected float movingTime = 4f;
    protected float reSpawnTime = 4f;
    [SerializeField] protected float reSpawnTimer = 0f;
    [SerializeField] protected bool isStand = true;
    [SerializeField] protected bool isMoving = false;
    [SerializeField] protected float movingTimer = 0;
    [SerializeField] protected float standTimer = 0;
    [SerializeField] protected float waitingSpawnTime = 4f;
    [SerializeField] protected float waitingSpawnTimer = 0f;

    [SerializeField] Vector3 leftBounds, rightBounds, topBounds, bottomBounds;

    public Character focusedPlayer;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadMovingBounds();
        name = this.gameObject.name;
    }

    protected virtual void LoadMovingBounds() {
        Transform movingBound = this.transform.Find("MovingBounds");
        leftBounds = movingBound.Find("LeftBounds").position;
        rightBounds = movingBound.Find("RightBounds").position;
        topBounds = movingBound.Find("TopBounds").position;
        bottomBounds = movingBound.Find("BottomBounds").position;
    }

    protected override void Reset()
    {
        // Do nothing
    }

    private void Update() {
        if (!IsAlive()) {
            Death();
            reSpawnTimer += Time.deltaTime;
            if (reSpawnTimer >= reSpawnTime) {
                reSpawnTimer = 0;
                ReSpawn();
            }
            return;
        }

        SearchFocus();
        UpdateFocus();
        Vulnerability();

        if (isStand) {
            standTimer += Time.deltaTime;
            Stand();
            if (standTimer >= standTime) {
                standTimer = 0;
                isStand = false;
                int randomFaceSide = Random.Range(0, 2);
                if (randomFaceSide == 1) {
                    faceSide = 1;
                } else if (randomFaceSide == 0) {
                    faceSide = -1;
                }
            }
        }
        if (!isStand) {
            Move();
            movingTimer += Time.deltaTime;
            if (movingTimer >= movingTime) {
                movingTimer = 0;
                isStand = true;
            }
        }
        UpdateAnimation();
    }

    public override void Move()
    {
        if (mobType == STAND_TYPE) {
            if (faceSide > 0) {
                if (transform.position.x < rightBounds.x) {
                    transform.position += new Vector3(faceSide, 0, 0) * speed * Time.deltaTime;
                    currentState = WALK_STATE;
                }
                if (transform.position.x >= rightBounds.x) {
                    faceSide = -1;
                }
            }
            if (faceSide < 0) {
                if (transform.position.x >= leftBounds.x) {
                    transform.position += new Vector3(faceSide, 0, 0) * speed * Time.deltaTime;
                    currentState = WALK_STATE;
                }
                if (transform.position.x <= leftBounds.x) {
                    faceSide = 1;
                }
            }
        }
        TurnFace();
    }

    private void Stand() {
        currentState = STAND_STATE;
    }

    protected override void UpdateAnimation()
    {
        anim.SetInteger(MOB_STATE, currentState);
    }

    public void Death() {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    public override void ReSpawn()
    {
        transform.position = spawnPosition;
        healthPoint = healthPointHolder;
        isAlive = true;
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }

    public void Vulnerability() {
        if (healthPoint < healthPointHolder) {
            isVulnerable = true;
            currentState = HURT_STATE;
            UpdateAnimation();
        }
    }

    public override void SearchFocus() {
        Vector3 playerPosition = PlayerController.instance.character.transform.position;
        if (IsPlayerVisible(playerPosition))
            focusedPlayer = PlayerController.instance.character;
    }

    public override void UpdateFocus()
    {
        Vector3 playerPosition = PlayerController.instance.character.transform.position;
        if (!IsPlayerVisible(playerPosition))
            focusedPlayer = null;
    }

    public bool IsPlayerVisible(Vector3 playerPosition) {
        Vector3 mobPos = transform.position;
        if (playerPosition.x >= (mobPos.x - CameraFollow.instance.width / 2) && mobPos.x <= (mobPos.x + CameraFollow.instance.width / 2) &&
        mobPos.y >= (mobPos.x - CameraFollow.instance.height / 2) && mobPos.y <= (mobPos.x + CameraFollow.instance.height / 2))
            return true;
        return false;
    }

    public void SetData(MobData mobData) {
        objectName = mobData.name;
        healthPointHolder = mobData.healthPoint;
        healthPoint = mobData.healthPoint;
        manaPointHolder = mobData.manaPoint;
        manaPoint = mobData.manaPoint;
        damage = mobData.damage;
        def = mobData.def;
        crit = mobData.crit;
        speed = mobData.speed;
        isAlive = true;
    }
}