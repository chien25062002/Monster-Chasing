using System.Collections;
using System.Collections.Generic;

public class GameData
{
    public static GameData instance;

    public static GameData GetInstance() {
        if (instance == null)
            instance = new GameData();
        return instance;
    }

    public List<Map> maps = new List<Map>();
    public List<MobData> mobDatas = new List<MobData>();
    public List<Skill> skills = new List<Skill>();
    public List<SkillTemplate> skillTemplates = new List<SkillTemplate>();
    public List<Waypoint> waypoints = new List<Waypoint>();

    public Skill GetSkillById(int id) {
        foreach (Skill skill in skills)
            if (skill.id == id) return skill;
        return null;
    }

    public Waypoint GetWaypointById(int id) {
        foreach (Waypoint waypoint in waypoints)
            if (waypoint.currentMapId == id) return waypoint;
        return null;
    }

    public SkillTemplate GetSkillTemplateById(int id) {
        foreach (SkillTemplate skillTemplate in skillTemplates) {
            if (skillTemplate.id == id)
                return skillTemplate;
        }
        return null;
    }
}
