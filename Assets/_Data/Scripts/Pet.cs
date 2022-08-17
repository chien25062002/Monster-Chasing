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
    public bool hasDrawDialog;

    public const int ATTACK_STATUS = 1;
    public const int PROTECT_STATUS = 2;
    public const int FOLLOW_STATUS = 3;
    public const int GOHOME_STATUS = 4;
    protected int petStatus;

    protected override void Awake()
    {
        if (instance != null)
            Debug.Log("Only 1 instance is allowed to be created");
        instance = this;
        DontDestroyOnLoad(gameObject);
        isMoving = false;
        isMovingToRight = true;
        hasDrawDialog = false;
        petStatus = ATTACK_STATUS;
    }

    protected override void Update() {
        base.Update();
        if (petStatus == ATTACK_STATUS) {
            Attack();
        }
        UpdateSkillCooldown();
        AutoIncreasePotential();
    }

    public override bool CanAttack()
    {
        if (base.CanAttack()) {
            return true;
        } else {
            if (!hasDrawDialog) {
                hasDrawDialog = true;
                string message = "Sư phụ ơi con hết mana rồi.";
                GamePanel.instance.DrawMessageDialog(message, MessageDialog.COLOR_BLACK, transform);
            }
            return false;
        }
    }

    public override void UsePotion()
    {
        this.healthPoint = healthPointHolder;
        this.manaPoint = manaPointHolder;
        if (!hasDrawDialog) {
                hasDrawDialog = true;
                string message = "Cảm ơn sư phụ.";
                GamePanel.instance.DrawMessageDialog(message, MessageDialog.COLOR_BLACK, transform);
        }
    }

    public override void Attack()
    {
        if (mobFocus == null)
            return;
        if (!CanAttack())
            return;
        if (!selectedSkill.isCooldown) {
            selectedSkill.cooldownTimer = selectedSkill.cooldown;
            selectedSkill.isCooldown = true;
            
            UseSkill();
            FaceLookAtEnemy();
            Transform spawnedSkill = SkillManager.instance.SpawnSkillById(skills[0].id);
            spawnedSkill.GetComponent<Weapon>().direction = this.faceSide;
            spawnedSkill.GetComponent<Weapon>().owner = gameObject;
            spawnedSkill.GetComponent<Weapon>().target = mobFocus.gameObject;
            spawnedSkill.position = transform.position;
            spawnedSkill.gameObject.SetActive(true);
        }
    }

    public void UpdateSkillCooldown() {
        if (selectedSkill.isCooldown) {
            selectedSkill.cooldownTimer -= Time.deltaTime;
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

    public virtual void SetStatus(int status) {
        string petMessage = "";
        switch (status) {
            case FOLLOW_STATUS:
                GoHome(false);
                petStatus = FOLLOW_STATUS;
                petMessage = "Ok con đi theo sư phụ.";
                break;
            case ATTACK_STATUS:
                GoHome(false);
                petStatus = ATTACK_STATUS;
                petMessage = "Ok con sẽ tấn công phụ.";
                break;
            case GOHOME_STATUS:
                petStatus = "Ok sư phụ con sẽ về nhà.";
                GamePanel.instance.DrawMessageDialog(petMessage, MessageDialog.COLOR_BLACK, transform);
                GoHome(true);
                return;
                break;
        }
        GamePanel.instance.DrawMessageDialog(petMessage, MessageDialog.COLOR_BLACK, transform);
    }

    private void GoHome(bool goHome) {
        if (goHome)
            transform.GetComponent<SpriteRenderer>().enabled = false;
        else
            transform.GetComponent<SpriteRenderer>().enabled = true;
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

    protected virtual void DecreasePotential(int potential) {
        this.potential -= potential;
    }

    public override void ReceivePotential(int receivedPotential)
    {
        if (receivedPotential <= 0)
            return;
        this.power += (int) (receivedPotential * Percent(150));
        this.potential += (int) (receivedPotential * Percent(150));
    }

    protected virtual void AutoIncreasePotential() {
        int damgePotential = damage * 10;
        int hpPotential = (int) (healthPointHolder * Percent(100));
        int mpPotential = (int) (manaPointHolder * Percent(100));
        int critPotential = GetCrit() * 1000;

        if (potential >= hpPotential && potential - hpPotential >= 0) {
            healthPointHolder += 20;
            DecreasePotential(hpPotential);
            return;
        }
        if (potential >= mpPotential && potential - manaPointHolder >= 0) {
            manaPointHolder += 20;
            DecreasePotential(mpPotential);
            return;
        }
        if (potential >= damgePotential && potential - damgePotential >= 0) {
            damage++;
            DecreasePotential(damgePotential);
            return;
        }
        if (potential >= critPotential && potential - critPotential >= 0) {
            crit += 1;
            DecreasePotential(mpPotential);
            return;
        }
    }

    public override void UpdateData(string[] data)
    {
        base.UpdateData(data);
        selectedSkill = skills[0];
    }
}
