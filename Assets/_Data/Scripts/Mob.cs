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
    protected float skillCooldown = 10;
    private bool spawnCoin;
    public bool isBoss;
    public bool isAttacking;
    [SerializeField] protected float reSpawnTimer = 0f;
    [SerializeField] protected bool isStand = true;
    [SerializeField] protected bool isMoving = false;
    [SerializeField] protected float movingTimer = 0;
    [SerializeField] protected float standTimer = 0;
    [SerializeField] protected float waitingSpawnTime = 4f;
    [SerializeField] protected float waitingSpawnTimer = 0f;

    [SerializeField] Vector3 leftBounds, rightBounds, topBounds, bottomBounds;

    public Transform focusedObject;

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
        UpdateSkillCooldown();

        if (isAttacking)
            return;
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

    public virtual void Death() {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        if (!spawnCoin) {
            DropGoldCoin(100, 5000);
        }
        spawnCoin = true;
    }

    public override void ReSpawn()
    {
        transform.position = spawnPosition;
        healthPoint = healthPointHolder;
        isAlive = true;
        isVulnerable = false;
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        spawnCoin = false;
    }

    public void Vulnerability() {
        if (healthPoint < healthPointHolder) {
            isVulnerable = true;
            currentState = HURT_STATE;
            UpdateAnimation();
        }
        if (isVulnerable) {
            Attack();
        }
    }

    public override void Attack()
    {
        if (focusedObject == null)
            return;
        if (!selectedSkill.isCooldown) {
            selectedSkill.cooldownTimer = skillCooldown;
            selectedSkill.isCooldown = true;
            
            FaceLookAtEnemy();
            isAttacking = true;
            currentState = ATTACK_STATE;
            UpdateAnimation();
            Transform spawnedSkill = SkillManager.instance.SpawnSkillById(skills[0].id);
            spawnedSkill.GetComponent<Weapon>().direction = this.faceSide;
            spawnedSkill.GetComponent<Weapon>().owner = gameObject;
            spawnedSkill.GetComponent<Weapon>().target = focusedObject.gameObject;
            spawnedSkill.position = transform.position;
            spawnedSkill.gameObject.SetActive(true);
        }
    }

    public void AttackFinish() {
        isAttacking = false;
    }

    public float cooldownTimer;
    public void UpdateSkillCooldown() {
        if (selectedSkill.isCooldown) {
            selectedSkill.cooldownTimer -= Time.deltaTime;
            cooldownTimer = selectedSkill.cooldownTimer;
            if (selectedSkill.cooldownTimer <= 0)
                selectedSkill.isCooldown = false;
        }
    }

    public override void FaceLookAtEnemy()
    {
        if (focusedObject != null) {
            float dir = focusedObject.transform.position.x - transform.position.x;
            if (dir > 0)
                faceSide = 1;
            if (dir < 0)
                faceSide = -1;
            TurnFace();
        }
    }

    public void RandomSkill() {
        int[] arrIndexSkill = {8};
        int skillId = arrIndexSkill[Random.Range(0, arrIndexSkill.Length)];
        skills.Add(GameData.instance.GetSkillById(skillId).Clone());
        selectedSkill = skills[0];
    }

    public override void SearchFocus() {
        if (makeHurtObject != null) {
            focusedObject = makeHurtObject.transform;
            return;
        }
        Vector3 playerPosition = PlayerController.instance.character.transform.position;
        if (IsPlayerVisible(playerPosition))
            focusedObject = PlayerController.instance.character.transform;
    }

    public override void UpdateFocus()
    {
        if (focusedObject != null) {
            if (!IsPlayerVisible(focusedObject.position))
                focusedObject = null;
        }
    }

    public bool IsPlayerVisible(Vector3 playerPosition) {
        Vector3 mobPos = transform.position;
        if (playerPosition.x >= (mobPos.x - CameraFollow.instance.width / 2) && playerPosition.x <= (mobPos.x + CameraFollow.instance.width / 2) &&
        playerPosition.y >= (mobPos.y - CameraFollow.instance.height / 2) && playerPosition.y <= (mobPos.y + CameraFollow.instance.height / 2))
            return true;
        return false;
    }

    public void DropGoldCoin(int minCoin, int maxCoin) {
        int goldValue = Random.Range(minCoin, maxCoin + 1);
        if (goldValue < (maxCoin * 1.0 / 3)) {
            GameObject coin = Instantiate(Resources.Load("Prefabs/GoldCoin") as GameObject);
            coin.GetComponent<GoldCoin>().goldValue = goldValue;
            coin.transform.position = transform.position;
        } else if (goldValue >= (maxCoin * 1.0 / 3) && goldValue < (maxCoin * 2.0 / 3)) {
            Vector3 pos = transform.position;
            pos.x -= 1;
            for (int i = 1; i <= 2; i++) {
                GameObject coin = Instantiate(Resources.Load("Prefabs/GoldCoin") as GameObject);
                coin.GetComponent<GoldCoin>().goldValue = goldValue / 2;
                coin.transform.position = pos;
                pos.x += 1;
            }
        } else if (goldValue > (maxCoin * 2.0 / 3)) {
             Vector3 pos = transform.position;
                pos.x -= 1;
                for (int i = 1; i <= 3; i++) {
                    GameObject coin = Instantiate(Resources.Load("Prefabs/GoldCoin") as GameObject);
                    coin.GetComponent<GoldCoin>().goldValue = goldValue / 3;
                    coin.transform.position = pos;
                    pos.x += 1;
                }
        }
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
        isBoss = mobData.isBoss;
        isAlive = true;
    }
}
