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
    public int align;
    public Vector3 charPos;

    [SerializeField] protected Transform waypointCollision;

    private void Awake() {
        RectTransform rect = gameObject.GetComponent<RectTransform>();
        imageWidth = rect.rect.width;
        imageHeight = rect.rect.height;
        waypointCollision = transform.Find("WaypointCollision");
    }

    public void SetData(Waypoint waypoint) {
        currentMapName = waypoint.currentMapName;
        currentMapId = waypoint.currentMapId;
        nextMapName = waypoint.nextMapName;
        nextMapId = waypoint.nextMapId;
        x = waypoint.x;
        y = waypoint.y;
        z = waypoint.z;
        align = waypoint.align;
        charPos = new Vector3(waypoint.charX, waypoint.charY, waypoint.charZ);

        transform.GetComponentInChildren<Text>().text = nextMapName;
        transform.position = new Vector3(x, y, z);
        Vector3 posInScreen = Camera.main.WorldToScreenPoint(new Vector3(x, y, z));
        switch (align) {
            case Waypoint.ALIGN_LEFT:
                posInScreen.x -= imageWidth / 2;
                break;
            case Waypoint.ALIGN_CENTER:
                break;
            case Waypoint.ALIGN_RIGHT:
                posInScreen.x += imageWidth / 2; 
                break;
        }
        posInScreen.y -= imageHeight / 2;
        waypointCollision.transform.SetParent(MapScreen.instance.transform, true);
        waypointCollision.transform.position = Camera.main.ScreenToWorldPoint(posInScreen);
    }

    public void RemoveWayPoint() {
        Destroy(waypointCollision.gameObject);
        Destroy(gameObject);
    }

    public void Show() {
        gameObject.SetActive(true);
    }

    public void Hide() {
        gameObject.SetActive(false);
    }
}
