using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MenuPanel : Panel
{
    public static MenuPanel instance;

    public const int MISSION_INVENTORY = 1;
    public const int EQUIPMENT_INVENTORY = 2;
    public const int POTENTIALITY_INVENTORY = 3;
    public const int SKILL_INVENTORY = 4;
    public const int MENU_INVENTORY = 5;

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
    [SerializeField] protected Panel iMissionInventory;
    [SerializeField] protected Panel iEquipmentInventory;
    [SerializeField] protected Panel iPotentialityInventory;
    [SerializeField] protected Panel iSkillInventory;
    [SerializeField] protected Panel iMenuInventory;
    [SerializeField] protected Transform currentCharInfo;
    public Transform increasementInfo;

    protected override void Awake()
    {
        base.Awake();
        if (instance != null)
            Debug.Log("Only 1 MenuPanel instance is allowed to be created");
        instance = this;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        exitMenuButton = this.transform.Find("ExitMenuButton").GetComponent<Button>();
        exitMenuButton.onClick.AddListener(delegate {GameScreen.instance.Hide();});
        
        LoadTextUI();
        this.LoadInventoryPanel();
        currentInventory = null;
        currentCharInfo = this.transform.Find("CharacterInfo/PotentialInfo");
        increasementInfo = transform.Find("IncreasementInfo");
        increasementInfo.gameObject.SetActive(false);
    }

    protected virtual void LoadTextUI() {
        Transform characterInfo = this.transform.Find("CharacterInfo/BasicInfo");
        powerText = characterInfo.Find("PowerPanel/CharacterPowerText").GetComponent<Text>();
        healthPointText = characterInfo.Find("HPPanel/CharacterHPText").GetComponent<Text>();
        manaPointText = characterInfo.Find("ManaPanel/CharacterMPText").GetComponent<Text>();
        damageText = characterInfo.Find("DamagePanel/CharacterDamageText").GetComponent<Text>();
        critPointText = characterInfo.Find("CriticalPanel/CharacterCritText").GetComponent<Text>();

        Transform characterCoins = this.transform.Find("CharacterCoins");
        goldCoinText = characterCoins.Find("GoldCoin/GoldText").GetComponent<Text>();
        diamondText = characterCoins.Find("Diamond/DiamondText").GetComponent<Text>();
    }

    public virtual void ChangeInventory(int inventoryIndex) {
        if (currentInventory != null)
            currentInventory.Hide();
        switch (inventoryIndex) {
            case 1:
                
                break;
            case POTENTIALITY_INVENTORY:
                currentInventory = iPotentialityInventory;
                break;
            case SKILL_INVENTORY:
                currentInventory = iSkillInventory;
                break;
            case MENU_INVENTORY:
                currentInventory = iMenuInventory;
                break;
        }
        if (inventoryIndex == POTENTIALITY_INVENTORY) {
            if (currentCharInfo != this.transform.Find("CharacterInfo/PotentialInfo"))
                currentCharInfo.gameObject.SetActive(false);
            currentCharInfo = this.transform.Find("CharacterInfo/PotentialInfo");
            currentCharInfo.gameObject.SetActive(true);
        } else {
            if (currentCharInfo != this.transform.Find("CharacterInfo/BasicInfo"))
                currentCharInfo.gameObject.SetActive(false);
            currentCharInfo = this.transform.Find("CharacterInfo/BasicInfo");
            currentCharInfo.gameObject.SetActive(true);
        }
        ShowInventory();
    }

    public virtual void ShowInventory() {
        if (currentInventory != null) {
            currentInventory.Show();
        }
    }

    protected virtual void LoadInventoryPanel() {
        Transform inventoryHolder = transform.Find("InventoryHolder").transform;
        iSkillInventory = inventoryHolder.Find("SkillInventory").GetComponent<Panel>();
        iPotentialityInventory = inventoryHolder.Find("PotentialInventory").GetComponent<Panel>();
        iMenuInventory = inventoryHolder.Find("MenuInventory").GetComponent<Panel>();
    }

    private void FixedUpdate() {
        Character character = PlayerController.instance.character;
        
        powerText.text = character.GetPower().ToString();
        healthPointText.text = character.GetHealthPoint().ToString() + "/" + character.GetHealthPointHolder();
        manaPointText.text = character.GetManaPoint().ToString() + "/" + character.GetManaPointHolder();
        damageText.text = character.GetDamage().ToString();
        critPointText.text = character.GetCrit().ToString();

        goldCoinText.text = character.GoldCoin.ToString();
        diamondText.text = character.Diamond.ToString();
    }
}
