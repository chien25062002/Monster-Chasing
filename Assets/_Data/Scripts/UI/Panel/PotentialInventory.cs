using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PotentialInventory : Inventory
{
    public List<Button> potentials = new List<Button>();

    protected override void LoadComponents()
    {
        Transform scrollViewContent = transform.Find("Scroll View/Viewport/Content");
        foreach (Transform child in scrollViewContent) {
            this.SetData(child);
            Button b = child.Find("PotentialInfo").GetComponent<Button>();
            b.onClick.AddListener(delegate {IncreasementInfo.instance.SetData(child); });
            potentials.Add(child.GetComponent<Button>());
        }
    }

    protected virtual void SetData(Transform potential) {
        string valueText = "";
        string increasementText = "";
        int rootValue;
        rootValue = PlayerController.instance.character.GetPropertyByName(potential.name);

        Transform potentialInfo = potential.Find("PotentialInfo");
        Text rootPotential = potentialInfo.Find("RootText").GetComponent<Text>();
        Text increasePotential = potentialInfo.Find("IncreaseText").GetComponent<Text>();
        if (potential.name == "HealthPoint") {
            valueText += "HP gốc: ";
            increasementText += rootValue + 200 + " tiềm năng tăng: 20";
        }
        if (potential.name == "ManaPoint") {
            valueText += "MP gốc: ";
            increasementText += rootValue + 200 + " tiềm năng tăng: 20";
        }
        if (potential.name == "Damage") {
            valueText += "Sức đánh gốc: ";
            increasementText += rootValue * 10 + " tiềm năng tăng: 1";
        }
        if (potential.name == "Crit") {
            valueText += "Chí mạng gốc: ";
            increasementText += rootValue * 1000 + " tiềm năng tăng: 1";
        }
        rootPotential.text = valueText + rootValue;
        increasePotential.text = increasementText;
    }

    protected override void LoadInventoryData()
    {
        
    }
}
