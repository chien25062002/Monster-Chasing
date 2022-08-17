using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuInventory : Inventory
{
    public Button petButton;

    protected override void LoadComponents()
    {
        Transform scrollViewContent = transform.Find("Scroll View/Viewport/Content");
        petButton = scrollViewContent.Find("PetButton").GetComponent<Button>();
        petButton.onClick.AddListener(delegate {GameScreen.instance.SetPanel(GameScreen.PET_PANEL);});
    }

    public void PetInventory() {

    }
}
