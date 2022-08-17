using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PetInventoryButton : MyMonoBehaviour
{
    [SerializeField] protected Button petInfoButton;
    [SerializeField] protected Button petStatusButton;

    protected override void LoadComponents()
    {
        petInfoButton = transform.Find("PetBtn").GetComponent<Button>();
        petStatusButton = transform.Find("PetStatusBtn").GetComponent<Button>();

        AddButtonEvent();
    }

    protected virtual void AddButtonEvent() {
        petInfoButton.onClick.AddListener(delegate {MenuPanel.instance.ChangeInventory(MenuPanel.SKILL_INVENTORY);});
        petStatusButton.onClick.AddListener(delegate {MenuPanel.instance.ChangeInventory(MenuPanel.POTENTIALITY_INVENTORY);});
    }
}
