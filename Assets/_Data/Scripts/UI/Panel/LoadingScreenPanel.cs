using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LoadingScreenPanel : Panel
{
    public Slider slider;
    public Transform njnjaLoading;
    public Transform fillArea;

    protected override void LoadComponents()
    {
        slider = this.transform.Find("Slider").GetComponent<Slider>();
        fillArea = slider.transform.Find("Fill Area");
        njnjaLoading = fillArea.transform.Find("Fill").transform.Find("NjnjaLoading"); 
        SetSliderValue(0);
    }

    public void SetSliderValue(float value) {
        njnjaLoading.position = 2 * fillArea.position;
        slider.value = value;
    }
}
