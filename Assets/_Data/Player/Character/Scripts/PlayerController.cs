using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MyMonoBehaviour
{
    public static PlayerController instance;

    public PlayerMovement playerMovement;
    public Animator anim;
    public Character character;

    protected override void Awake() {
        if (instance != null)
            Debug.Log("Only 1 PlayerController is allowed to be created");
        PlayerController.instance = this;
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    protected override void LoadComponents()
    {
        this.LoadCharacter();
        this.LoadChildComponents();
    }

    protected void LoadCharacter() {
        character = this.transform.Find("Character").GetComponent<Character>();
        anim = character.gameObject.GetComponent<Animator>();
    }

    protected void LoadChildComponents() {
        playerMovement = transform.Find("PlayerMovement").GetComponent<PlayerMovement>();
        //playerCombat = transform.Find("PlayerCombat").GetComponent<PlayerCombat>();
    }
}
