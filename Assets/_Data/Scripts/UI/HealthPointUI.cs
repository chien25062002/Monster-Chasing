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
    }

    // Update is called once per frame
    void Update()
    {
        character = PlayerController.instance.character;
        hp = character.GetHealthPoint();
        hpHolder = character.GetHealthPointHolder();
        mp = character.GetManaPoint();
        mpHolder = character.GetManaPointHolder();
        hpPercent = ((float) hp) / hpHolder;
        mpPercent = ((float) mp) / mpHolder;

        hpFillBar.fillAmount = hpPercent;
        mpFillBar.fillAmount = mpPercent;
    }
}
