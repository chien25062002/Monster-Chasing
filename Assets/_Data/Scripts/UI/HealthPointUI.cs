using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthPointUI : MyMonoBehaviour
{
    [SerializeField] protected Image hpFillBar;
    [SerializeField] protected Image mpFillBar;
    private float hpPercent;
    private float mpPercent;
    public Character character;
    public float hp, hpHolder, mp, mpHolder;

    protected override void LoadComponents()
    {
        hpFillBar = transform.Find("HealthPointBar/HpFillBar").GetComponent<Image>();
        mpFillBar = transform.Find("HealthPointBar/MpFillBar").GetComponent<Image>();
        character = PlayerController.instance.character;
    }

    // Update is called once per frame
    void Update()
    {
        hp = character.GetHealthPoint();
        hpHolder = character.GetHealthPointHolder();
        mp = character.GetManaPoint();
        mpHolder = character.GetManaPointHolder();
        hpPercent = ((float) character.GetHealthPoint()) / character.GetHealthPointHolder();
        mpPercent = ((float) character.GetManaPoint()) / character.GetManaPointHolder();
        Debug.Log("hpPercent: " + hpPercent + " mpPercent: " + mpPercent);

        hpFillBar.fillAmount = hpPercent;
        mpFillBar.fillAmount = mpPercent;
    }
}
