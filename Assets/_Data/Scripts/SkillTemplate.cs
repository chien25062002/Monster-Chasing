using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SkillTemplate
{
    public List<Skill> skills = new List<Skill>();
    public int id;
    public string name;
    public int maxLevel;
    public int type;
    public string iconPath;
    public string description;
    public int manaUseType;
    public Image iconImage;

    public SkillTemplate() {

    }

    public SkillTemplate(string[] data) {
        id = int.Parse(data[0]);
        name = data[1];
        maxLevel = int.Parse(data[2]);
        type = int.Parse(data[3]);
        iconPath = data[4];
        description = data[5];
        manaUseType = int.Parse(data[6]);

    }
}
