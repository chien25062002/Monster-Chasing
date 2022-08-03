using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MapObject, IMapObject
{
    public const string PLAYER_STATE = "State";
    public const int THROW_STATE = 10;
    
    protected bool isAttacking = false;
    public bool isMoving = false;
    public bool isFlying = false;
    public Mob mobFocus;
    public float moveX;
    public float moveY;

    private void Update() {
        SearchFocus();
        UpdateFocus();
    }

    public override void SearchFocus() {
        if (mobFocus == null) {
            float minDistance = float.MaxValue;
            foreach (Transform mob in GameScreen.instance.visibleMobs) {
                if (mob != null && mob.GetComponent<Mob>().isAlive) {
                    float distance = Vector3.Distance(transform.position, mob.transform.position);
                    if (minDistance > distance) {
                        minDistance = distance;
                        mobFocus = mob.GetComponent<Mob>();
                    }
                }
            }
        }
    }

    public override void UpdateFocus() {
        if (mobFocus != null) {
            if (!mobFocus.isAlive) {
                mobFocus = null;
                return;
            }
            if (!GameScreen.instance.IsMobInCamera(mobFocus.gameObject.transform))
                mobFocus = null;
        } else {
            SearchFocus();
        }
    }

    public void ChangeFocus() {
        if (mobFocus != null) {
            int index = GameScreen.instance.visibleMobs.IndexOf(mobFocus.gameObject.transform);
            if ((index + 1) >= GameScreen.instance.visibleMobs.Count) {
                if (GameScreen.instance.visibleMobs[0] != null)
                    mobFocus = GameScreen.instance.visibleMobs[0].GetComponent<Mob>();
            }
            else {
                if (GameScreen.instance.visibleMobs[index + 1] != null) {
                    mobFocus = GameScreen.instance.visibleMobs[index + 1].GetComponent<Mob>();
                }
            }
        }
    }

    public override void Move() {
        speed = 10;
        if (isMoving) {
            if (isGrounded && moveY < 0.5 )
                moveY = 0;
            transform.position += new Vector3(moveX, moveY, 0) * speed * Time.deltaTime;
        }
        x = transform.position.x;
        y = transform.position.y;
        TurnFace();
    }

    public void Falling() {
        transform.position += new Vector3(0, 1, 0) * speed * Time.deltaTime;
    }

    public void Jump() {
        
    }

    public override void FaceLookAtEnemy() {
        if (mobFocus != null) {
            float dir = mobFocus.transform.position.x - transform.position.x;
            if (dir > 0)
                faceSide = 1;
            if (dir < 0)
                faceSide = -1;
            TurnFace();
        }
    }

    public override void Attack() {
        if (selectedSkill == null)
            return;
        if (mobFocus == null)
            return;
        FaceLookAtEnemy();
        if (!selectedSkill.isCooldown) {
            selectedSkill.cooldownTimer = selectedSkill.cooldown;
            selectedSkill.isCooldown = true;
            isAttacking = true;
            switch (selectedSkill.skillTemplateId) {
                case Skill.KIEMKHI_TYPE:
                    currentState = ATTACK_STATE;
                    break;
                case Skill.AMKHI_TYPE:
                    currentState = THROW_STATE;
                    break;
            }
            anim.SetInteger(Character.PLAYER_STATE, currentState);
        }
    }

    public void Attack(string skillName) {
        if (mobFocus == null)
            return;
        FaceLookAtEnemy();
        Skill skill = GetSkillByName(skillName);
        selectedSkill = skill;
        if (!selectedSkill.isCooldown) {
            selectedSkill.cooldownTimer = selectedSkill.cooldown;
            selectedSkill.isCooldown = true;
            isAttacking = true;
            switch (skill.skillTemplateId) {
                case Skill.KIEMKHI_TYPE:
                    currentState = ATTACK_STATE;
                    break;
                case Skill.AMKHI_TYPE:
                    currentState = THROW_STATE;
                    break;
            }
            anim.SetInteger(Character.PLAYER_STATE, currentState);
        }
    }

    public Skill GetSkillByName(string name) {
        foreach (Skill skill in skills)
            if (name.Contains(skill.skillName))
                return skill;
        return null;
    }

    public Skill GetSkillById(int id) {
        foreach (Skill skill in skills)
            if (skill.id == id)
                return skill;
        return null;
    }

    public virtual void AttackFinish() {
        isAttacking = false;
    }

    public virtual void ThrowWeapon() {
        Transform spawnedSkill = SkillManager.instance.SpawnSkill(selectedSkill.referenceObjectName);
        spawnedSkill.GetComponent<Weapon>().direction = this.faceSide;
        spawnedSkill.GetComponent<Weapon>().owner = gameObject;
        spawnedSkill.GetComponent<Weapon>().target = mobFocus.gameObject;
        if (this.faceSide > 0)
            spawnedSkill.transform.position = this.transform.Find("RightThrowingPosition").transform.position;
        if (this.faceSide < 0)
            spawnedSkill.transform.position = this.transform.Find("LeftThrowingPosition").transform.position;
        spawnedSkill.gameObject.SetActive(true);
    }

    public virtual void UseSword() {
        Transform spawnedSkill = SkillManager.instance.SpawnSkill(selectedSkill.referenceObjectName);
        spawnedSkill.GetComponent<Weapon>().owner = gameObject;
        spawnedSkill.GetComponent<Weapon>().direction = this.faceSide;
        spawnedSkill.GetComponent<Weapon>().target = mobFocus.gameObject;
        spawnedSkill.GetComponent<Weapon>().FlipX();
        if (this.faceSide > 0)
            spawnedSkill.transform.position = this.transform.Find("RightSplash").transform.position;
        if (this.faceSide < 0)
            spawnedSkill.transform.position = this.transform.Find("LeftSplash").transform.position;
        spawnedSkill.gameObject.SetActive(true);
    }

    public virtual void FindingFocus() {

    }

    public Vector3 Position {
        get { return new Vector3(x, y, transform.position.z); }
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
    }

     public virtual bool IsAttacking {
        get { return isAttacking; }
    }
    public int Speed {
        get { return speed; }
    }

    public int CurrentState {
        get { return currentState; }
        set { currentState = value; }
    }

    public void UpdateData(string[] data) {
        healthPoint = int.Parse(data[0]);
        manaPoint = int.Parse(data[1]);
        damage = int.Parse(data[2]);
        def = int.Parse(data[3]);
        crit = int.Parse(data[4]);
        string[] sSkillId = data[5].Split(";");
        for (int i = 0; i < sSkillId.Length; i++)
            this.skills.Add(GameData.GetInstance().GetSkillById(int.Parse(sSkillId[i])));
        transform.position = new Vector3(float.Parse(data[6]), float.Parse(data[7]), float.Parse(data[8]));
        power = int.Parse(data[9]);
        currentMapId = int.Parse(data[10]);
    }
}
