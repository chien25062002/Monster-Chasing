using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MyMonoBehaviour
{
    public static CameraFollow instance;
    public Vector3 playerPosition;
    public Vector3 camPosition;
    public Transform mapBounds;
    public Vector3 leftBounds, rightBounds, topBounds, bottomBounds;
    public float width, height;

    public float minHorizontal, maxHorizontal, minVertical, maxVertical;
    public float camLeftBounds, camRightBounds, camTopBounds, camBottomBounds;

    protected override void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (instance != null)
            Debug.LogWarning("Only 1 CameraFollow instance is allowed to be created");
        instance = this;
    }
    protected override void LoadComponents()
    {
        mapBounds = GameObject.FindGameObjectWithTag("MapBounds").transform;
        leftBounds = mapBounds.Find("LeftBounds").transform.position;
        rightBounds = mapBounds.Find("RightBounds").transform.position;
        topBounds = mapBounds.Find("TopBounds").transform.position;
        bottomBounds = mapBounds.Find("BottomBounds").transform.position;
        height = 2 * Camera.main.orthographicSize;
        width = height * Camera.main.aspect;

        minHorizontal = leftBounds.x + (width / 2);
        maxHorizontal = rightBounds.x - (width / 2);
        minVertical = bottomBounds.y + (height / 2);
        maxVertical = topBounds.y - (height / 2);
    }

    private void LateUpdate() {
        if (mapBounds == null) {
            LoadComponents();
        }
        MoveCamera();
        UpdateCameraBounds();
    }

    protected virtual void MoveCamera() {
        playerPosition = PlayerController.instance.character.transform.position;
        camPosition = this.transform.position;
        camPosition.x = playerPosition.x;
        camPosition.y = playerPosition.y + 2.5f;

        camPosition.x = Mathf.Clamp(camPosition.x, minHorizontal, maxHorizontal);
        camPosition.y = Mathf.Clamp(camPosition.y, minVertical, maxVertical);

        this.transform.position = camPosition;
    }

    protected virtual void UpdateCameraBounds() {
        camLeftBounds = camPosition.x - width / 2;
        camRightBounds = camPosition.x + width / 2;
        camTopBounds = camPosition.y + height / 2;
        camBottomBounds = camPosition.y - height / 2;
    }
}
