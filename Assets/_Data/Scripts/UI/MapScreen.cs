using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapScreen : MyMonoBehaviour
{
    public static MapScreen instance;

    public Map currentMap;
    public List<WaypointUI> waypoints = new List<WaypointUI>();

    protected override void Awake()
    {
        base.Awake();
        if (instance != null)
            Debug.Log("Only 1 MapScreen instance is allowed to be created");
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void AddWaypoint(WaypointUI waypointUI) {
        waypoints.Add(waypointUI);
    }

    public void ClearWaypoint() {
        foreach (WaypointUI waypoint in waypoints) {
            if (waypoint != null) {
                waypoint.RemoveWayPoint();
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
            if (waypointUI.nextMapId == lastId)
                return waypointUI.transform;
        return null;
    }

    protected override void LoadComponents()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
} 
