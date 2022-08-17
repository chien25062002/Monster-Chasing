using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MyMonoBehaviour
{
    public Transform owner;
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    protected override void LoadComponents()
    {
        slider = transform.GetComponent<Slider>();
        fill = transform.Find("Fill").GetComponent<Image>();
    }

    public void SetMaxHealth(int health) {
        slider.maxValue = health;
        slider.value = health;

        fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(int health) {
        slider.value = health;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    private void Update() {
        if (owner != null && owner.gameObject.activeSelf) {
            OnTheHeadOfOwner();
            this.SetHealth(owner.GetComponent<Boss>().GetHealthPoint());
        }
    }

    public void Show() {
        gameObject.SetActive(true);
    }

    public void Hide() {
        gameObject.SetActive(false);
    }

    protected virtual void OnTheHeadOfOwner() {
        Vector3 pos = owner.position;
        pos.y += 2.5f;
        transform.position = pos;
    }
}
