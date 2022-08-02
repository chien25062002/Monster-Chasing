using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaypointUI : MonoBehaviour
{
    public string currentMapName;
    public int currentMapId;
    public string nextMapName;
    public int nextMapId;

    public float x;
    public float y;
    public float z;
    public float convertedX;
    public float convertedY;
    public float convertedZ;

    public float imageWidth;
    public float imageHeight;

    private void Awake() {
        RectTransform rect = gameObject.GetComponent<RectTransform>();
        imageWidth = rect.rect.width;
        imageHeight = rect.rect.height;
    }

    public void SetData(Waypoint waypoint) {
        currentMapName = waypoint.currentMapName;
        currentMapId = waypoint.currentMapId;
        nextMapName = waypoint.nextMapName;
        nextMapId = waypoint.nextMapId;
        x = waypoint.x;
        y = waypoint.y;
        z = waypoint.z;
        
        transform.GetComponentInChildren<Text>().text = nextMapName;
        transform.position = new Vector3(x, y, z);
    }

    public void Show() {
        gameObject.SetActive(true);
    }

    public void Hide() {
        gameObject.SetActive(false);
    }
}
