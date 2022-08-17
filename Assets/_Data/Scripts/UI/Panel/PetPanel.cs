using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PetPanel : Panel {

    public static PetPanel instance;

    public const int PET_INFO_INVENTORY = 1;
    public const int PET_STATUS_INVENTORY = 2;

    [SerializeField] protected Button exitMenuButton;
    [SerializeField] protected Text powerText;
    [SerializeField] protected Text healthPointText;
    [SerializeField] protected Text manaPointText;
    [SerializeField] protected Text damageText;
    [SerializeField] protected Text defPointText;
    [SerializeField] protected Text critPointText;
    [SerializeField] protected Text goldCoinText;
    [SerializeField] protected Text diamondText;

    [SerializeField] protected Panel currentInventory;
    [SerializeField] protected Transform currentCharInfo;

    [SerializeField] protected Panel iPetInventory;
    [SerializeField] protected Panel iPetStatusInventory;

    protected override void Awake()
    {
        base.Awake();
        if (instance != null)
            Debug.Log("Only 1 PetPanel instance is allowed to be created");
        instance = this;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        exitMenuButton = this.transform.Find("ExitMenuButton").GetComponent<Button>();
        exitMenuButton.onClick.AddListener(delegate {GameScreen.instance.Hide();});
        
        LoadTextUI();
        this.LoadInventoryPanel();
        currentInventory = iPetStatusInventory;
        currentCharInfo = this.transform.Find("CharacterInfo/PotentialInfo");
    }

    protected virtual void LoadTextUI() {
        Transform characterInfo = this.transform.Find("CharacterInfo/BasicInfo");
        characterInfo.gameObject.SetActive(true);
        powerText = characterInfo.Find("PowerPanel/CharacterPowerText").GetComponent<Text>();
        healthPointText = characterInfo.Find("HPPanel/CharacterHPText").GetComponent<Text>();
        manaPointText = characterInfo.Find("ManaPanel/CharacterMPText").GetComponent<Text>();
        damageText = characterInfo.Find("DamagePanel/CharacterDamageText").GetComponent<Text>();
        critPointText = characterInfo.Find("CriticalPanel/CharacterCritText").GetComponent<Text>();

        Transform characterCoins = this.transform.Find("CharacterCoins");
        goldCoinText = characterCoins.Find("GoldCoin/GoldText").GetComponent<Text>();
        diamondText = characterCoins.Find("Diamond/DiamondText").GetComponent<Text>();
    }

    public virtual void ShowInventory() {
        if (currentInventory != null) {
            currentInventory.Show();
        }
    }

    protected virtual void LoadInventoryPanel() {
        Transform inventoryHolder = transform.Find("InventoryHolder").transform;
        iPetStatusInventory = inventoryHolder.Find("PetStatusInventory").GetComponent<Panel>();
    }

    public virtual void ChangeInventory(int inventoryIndex) {
        if (currentInventory != null)
            currentInventory.Hide();
        switch (inventoryIndex) {
            case PET_INFO_INVENTORY:
                currentInventory = iPetInventory;
                break;
            case PET_STATUS_INVENTORY:
                currentInventory = iPetStatusInventory;
                break;
        }
        ShowInventory();
    }

    private void FixedUpdate() {
        Pet pet = Pet.instance;
        
        powerText.text = pet.GetPower().ToString();
        healthPointText.text = pet.GetHealthPoint().ToString() + "/" + pet.GetHealthPointHolder();
        manaPointText.text = pet.GetManaPoint().ToString() + "/" + pet.GetManaPointHolder();
        damageText.text = pet.GetDamage().ToString();
        critPointText.text = pet.GetCrit().ToString();
    }

    private void Update() {
        Pet pet = Pet.instance;
        
        powerText.text = pet.GetPower().ToString();
        healthPointText.text = pet.GetHealthPoint().ToString() + "/" + pet.GetHealthPointHolder();
        manaPointText.text = pet.GetManaPoint().ToString() + "/" + pet.GetManaPointHolder();
        damageText.text = pet.GetDamage().ToString();
        critPointText.text = pet.GetCrit().ToString();
    }
}
