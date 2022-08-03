using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointCollision : MonoBehaviour
{
    public Transform trueParent;
    private void Awake() {
        trueParent = transform.parent;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            int currentMapId = trueParent.GetComponent<WaypointUI>().currentMapId;
            PlayerController.instance.character.lastMapIndex = currentMapId;
            int nextMapId = trueParent.GetComponent<WaypointUI>().nextMapId;
            MapLoader.instance.LoadMap(nextMapId);
        }
    }
}
