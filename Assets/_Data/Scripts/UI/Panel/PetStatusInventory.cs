using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PetStatusInventory : Inventory
{
    public Button followStatusButton;
    public Button attackStatusButton;
    public Button goHomeStatusButton;

    protected override void LoadComponents() {
        Transform scrollViewContent = transform.Find("Scroll View/Viewport/Content");
        followStatusButton = scrollViewContent.Find("FollowStatus").GetComponent<Button>();
        attackStatusButton = scrollViewContent.Find("AttackStatus").GetComponent<Button>();
        goHomeStatusButton = scrollViewContent.Find("GoHomeStatus").GetComponent<Button>();
        SetButtonEvent();
    }

    protected virtual void SetButtonEvent() {
        followStatusButton.onClick.AddListener(delegate {Pet.instance.SetStatus(3);});
    } 
}
