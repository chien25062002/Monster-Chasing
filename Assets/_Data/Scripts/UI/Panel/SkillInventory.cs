using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SkillInventory : Inventory
{
    public List<Button> skills = new List<Button>();

    protected override void LoadComponents()
    {
        Transform scrollViewContent = transform.Find("Scroll View/Viewport/Content");
        foreach (Transform child in scrollViewContent) {
            skills.Add(child.GetComponent<Button>());
        }
    }

    protected override void LoadInventoryData()
    {
        int index = 0;
        foreach (Skill skill in PlayerController.instance.character.skills) {
            skills[index].transform.Find("EquipmentImage/Image").GetComponent<Image>().overrideSprite = MyImage.CreateSprite(skill.imageName);
            skills[index].transform.Find("EquipmentInfo/Text").GetComponent<Text>().text = skill.skillName;
            index++;
        }
    }
}
