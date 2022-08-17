using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryButton : MyMonoBehaviour
{
    [SerializeField] protected Button missionButton;
    [SerializeField] protected Button equipmentButton;
    [SerializeField] protected Button potentialityButton;
    [SerializeField] protected Button skillButton;
    [SerializeField] protected Button menuButton;

    protected override void LoadComponents()
    {
        missionButton = transform.Find("MissionBtn").GetComponent<Button>();
        equipmentButton = transform.Find("EquipmentBtn").GetComponent<Button>();
        potentialityButton = transform.Find("PotentialityBtn").GetComponent<Button>();
        skillButton = transform.Find("SkillBtn").GetComponent<Button>();
        menuButton = transform.Find("MenuBtn").GetComponent<Button>();

        AddButtonEvent();
    }

    protected virtual void AddButtonEvent() {
        skillButton.onClick.AddListener(delegate {MenuPanel.instance.ChangeInventory(MenuPanel.SKILL_INVENTORY);});
        potentialityButton.onClick.AddListener(delegate {MenuPanel.instance.ChangeInventory(MenuPanel.POTENTIALITY_INVENTORY);});
        menuButton.onClick.AddListener(delegate {MenuPanel.instance.ChangeInventory(MenuPanel.MENU_INVENTORY);});
    }
}
