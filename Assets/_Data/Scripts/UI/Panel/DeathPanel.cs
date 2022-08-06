using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DeathPanel : Panel
{
    public Button waitButton;
    public Button goHomeButton;
    public Button recoveryButton;

    protected override void LoadComponents()
    {
        goHomeButton = transform.Find("DeathButton/GoHomeButton").GetComponent<Button>();
        recoveryButton = transform.Find("DeathButton/RecoveryButton").GetComponent<Button>();
        SetButtonEvent();
    }

    protected void SetButtonEvent() {
        goHomeButton.onClick.AddListener(delegate {GameScreen.instance.CharacterGoHome();});
    }
}