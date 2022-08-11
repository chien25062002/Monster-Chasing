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

    protected override void Update() {
        base.Update();
        Attack();
        UpdateSkillCooldown();
    }

    private float skillCooldown = 4;

    public override void Attack()
    {
        if (mobFocus == null)
            return;
        if (!selectedSkill.isCooldown) {
            selectedSkill.cooldownTimer = skillCooldown;
            selectedSkill.isCooldown = true;
            
            FaceLookAtEnemy();
            Transform spawnedSkill = SkillManager.instance.SpawnSkillById(skills[0].id);
            spawnedSkill.GetComponent<Weapon>().direction = this.faceSide;
            spawnedSkill.GetComponent<Weapon>().owner = gameObject;
            spawnedSkill.GetComponent<Weapon>().target = mobFocus.gameObject;
            spawnedSkill.position = transform.position;
            spawnedSkill.gameObject.SetActive(true);
        }
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

    private void FixedUpdate() {
        LookAtOwner();
    }

    private void LateUpdate() {
        MovingAroundCharacterHead();
    }

    protected virtual void AutoAttack() {
        if (mobFocus != null) {

        }
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

    public override void UpdateData(string[] data)
    {
        base.UpdateData(data);
        selectedSkill = skills[0];
    }
}
