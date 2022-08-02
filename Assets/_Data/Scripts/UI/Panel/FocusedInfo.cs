using System;
using UnityEngine.UI;
using UnityEngine;

public class FocusedInfo : MyMonoBehaviour
{
    [SerializeField] protected Text focusedName;
    [SerializeField] protected Text focusedHp;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        focusedName = transform.Find("FocusedName").GetComponent<Text>();
        focusedHp = transform.Find("FocusedHp").GetComponent<Text>();
    }

    private void Update() {
        if (PlayerController.instance.character.mobFocus != null) {
            focusedName.text = PlayerController.instance.character.mobFocus.objectName;
            focusedHp.text = PlayerController.instance.character.mobFocus.GetHealthPoint() + "";
        } else {
            focusedName.text = "";
            focusedHp.text = "";
        }
    }
}
