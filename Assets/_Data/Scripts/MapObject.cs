using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapObject : MyMonoBehaviour, IMapObject
{
    public const int STAND_STATE = 0;
    public const int RUN_STATE = 1;
    public const int FLY_STATE = 2;
    public const int DEAD_STATE = 3;
    public const int WALK_STATE = 4;
    public const int HURT_STATE = 5;
    public const int ATTACK_STATE = 9;

    public string objectName;
    [SerializeField] protected int healthPointHolder;
    [SerializeField] protected int manaPointHolder;
    [SerializeField] public int currentState;
    [SerializeField] protected int healthPoint;
    [SerializeField] protected int manaPoint;
    [SerializeField] protected int damage;
    [SerializeField] protected int def;
    [SerializeField] protected int crit;
    [SerializeField] protected int speed = 4;
    public bool isDeath;
    public int currentMapId;
    public int lastMapIndex;
    [SerializeField] protected int power;
    public bool isAlive = true;
    public float faceSide = 1f;
    public bool isStanding;
    [SerializeField] protected float x;
    [SerializeField] protected float y;
    public Vector3 position;
    public Vector3 spawnPosition;
    public bool isGrounded;
    public bool isVulnerable;
    public Transform makeHurtObject;

    public List<Skill> skills = new List<Skill>();
    public Skill selectedSkill;

    [SerializeField] protected float timer = 0;

    [SerializeField] protected SpriteRenderer sr;
    [SerializeField] protected Animator anim;

    protected override void Awake() {
        base.Awake();
        faceSide = 1f;
    }
    protected override void LoadComponents()
    {
        sr = this.GetComponent<SpriteRenderer>();
        anim = this.GetComponent<Animator>();
    }

    public int GetPower() {
        return power;
    }

    public int GetHealthPoint() {
        return healthPoint;
    }

    public int GetHealthPointHolder() {
        return healthPointHolder;
    }

    public void GetDamage(int damage, Transform makeHurtObject) {
        this.makeHurtObject = makeHurtObject;
        this.healthPoint -= damage;
        if (this.healthPoint < 0) {
            this.healthPoint = 0;
            isDeath = true;
        }
    }

    public int GetManaPoint() {
        return manaPoint;
    }

    public int GetManaPointHolder() {
        return manaPointHolder;
    }

    public int GetDamage() {
        return damage;
    }

    public int GetDef() {
        return def;
    }

    public int GetCrit() {
        return crit;
    }

    public float GetX() {
        return x;
    }

    public float GetY() {
        return y;
    }

    public bool IsInvisible() {
        return true;
    }

    public virtual void Move() {
        // for override
    }

    public virtual void Attack() {

    }

    public virtual void FaceLookAtEnemy() {
        
    }

    public virtual void OnDeath() {

    }

    protected virtual bool IsAlive() {
        if (healthPoint > 0) {
            isAlive = true;
            return true;
        }
        isAlive = false;
        return false;
    }

    public virtual void ReSpawn() {

    }

    public virtual void SearchFocus() {

    }

    public virtual void UpdateFocus() {

    }

    public void TurnFace() {
        if (faceSide > 0)
            sr.flipX = false;
        if (faceSide < 0)
            sr.flipX = true;
    }

    protected virtual void UpdateAnimation() {

    }
}
