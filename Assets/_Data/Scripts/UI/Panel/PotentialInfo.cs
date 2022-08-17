using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotentialInfo : MyMonoBehaviour
{
    public Text potentialValue;
    protected override void LoadComponents()
    {
        potentialValue = transform.Find("PotentialValue").GetComponent<Text>();
    }

    private void FixedUpdate() {
        potentialValue.text = PlayerController.instance.character.Potential.ToString();
    }
}
