using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapScreen : MyMonoBehaviour
{
    public static MapScreen instance;

    public Map currentMap;
    public List<WaypointUI> waypoints = new List<WaypointUI>();
    public MyImage focusImage;

    protected override void Awake()
    {
        base.Awake();
        if (instance != null)
            Debug.Log("Only 1 MapScreen instance is allowed to be created");
        instance = this;
        DontDestroyOnLoad(gameObject);
        focusImage = MyImage.CreateImage("FocusArrow");
    }
/*
    private void OnGUI() {
        if (PlayerController.instance.character.mobFocus != null) {
            Vector3 pos = PlayerController.instance.character.mobFocus.transform.position;
            pos.y += 3.5f;
            Vector3 position = Camera.main.WorldToScreenPoint(pos);
            position.y = GameScreen.instance.height - position.y;
            GUI.DrawTexture(new Rect(position.x - focusImage.width, position.y - focusImage.height, focusImage.width, focusImage.height), focusImage.texture);
        }
    } */

    public void AddWaypoint(WaypointUI waypointUI) {
        waypoints.Add(waypointUI);
    }

    public void ClearWaypoint() {
        foreach (WaypointUI waypoint in waypoints.ToArray()) {
            if (waypoint != null) {
                waypoint.RemoveWayPoint();
                if (waypoint != null)
                    waypoints.Remove(waypoint);
            }
        }
    }

    public void Show() {
        foreach (WaypointUI waypoint in waypoints) {
            if (waypoint != null) {
                Debug.Log(waypoint);
            }
        }
    }

    public Transform GetWaypointWithId(int id) {
        foreach (WaypointUI waypointUI in waypoints)
            if (waypointUI.currentMapId == id)
                return waypointUI.transform;
        return null;
    }

    public Transform GetWaypointWithLastId(int lastId) {
        foreach (WaypointUI waypointUI in waypoints)
            if (waypointUI.nextMapId == lastId) {
                return waypointUI.transform;
            }
        return null;
    }

    protected override void LoadComponents()
    {
        
    }
} 
