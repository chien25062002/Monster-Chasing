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

    [SerializeField] protected Panel currentInventory;
    [SerializeField] protected Panel iMissionInventory;
    [SerializeField] protected Panel iEquipmentInventory;
    [SerializeField] protected Panel iPotentialityInventory;
    [SerializeField] protected Panel iSkillInventory;
    [SerializeField] protected Panel iMenuInventory;

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
        
        Transform characterInfo = this.transform.Find("CharacterInfo");
        powerText = characterInfo.Find("PowerPanel").Find("CharacterPowerText").GetComponent<Text>();
        healthPointText = characterInfo.Find("HPPanel").Find("CharacterHPText").GetComponent<Text>();
        manaPointText = characterInfo.Find("ManaPanel").Find("CharacterMPText").GetComponent<Text>();
        damageText = characterInfo.Find("DamagePanel").Find("CharacterDamageText").GetComponent<Text>();
        //defPointText = characterInfo.Find("DefensePanel").Find("CharacterDefText").GetComponent<Text>();
        critPointText = characterInfo.Find("CriticalPanel").Find("CharacterCritText").GetComponent<Text>();

        this.LoadInventoryPanel();
        currentInventory = null;
    }

    public virtual void ChangeInventory(int inventoryIndex) {
        switch (inventoryIndex) {
            case 1:
                
                break;
            case 4:
                currentInventory = iSkillInventory;
                break;
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
    }

    private void FixedUpdate() {
        Character character = PlayerController.instance.character;
        
        powerText.text = character.GetPower().ToString();
        healthPointText.text = character.GetHealthPoint().ToString();
        manaPointText.text = character.GetManaPoint().ToString();
        damageText.text = character.GetDamage().ToString();
        critPointText.text = character.GetCrit().ToString();
    }
}
