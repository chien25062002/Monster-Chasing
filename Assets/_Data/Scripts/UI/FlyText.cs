using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlyText : MyMonoBehaviour
{
    public const string FONT_PATH = "Font/";
    public const int COLOR_RED = 0;
    public const int COLOR_GREEN = 1;
    public const int COLOR_BLUE = 2;
    public const int COLOR_WHITE = 3;
    public const int COLOR_YELLOW = 4;

    public Transform focusObject;

    public Text textUI;

    public float flyTime;
    private float flyTimer;
    private float lastYPos;

    protected override void Awake()
    {
        textUI = GetComponent<Text>();
        flyTime = 1.5f;
    }

    public void SetText(string text, int x, int y) {

    }

    private void Update() {
        if (focusObject == null)
            return;
        Vector3 tempPos = focusObject.position;
        tempPos.y = lastYPos;
        tempPos.x += 2.5f;
        transform.position = tempPos;

        Fly();
        UpdateFlyTime();
    }

    public void Fly() {
        Vector3 tempPos = transform.position;
        tempPos.y += Time.deltaTime * 1.5f;
        lastYPos = tempPos.y;
        transform.position = tempPos;
    }

    public void UpdateFlyTime() {
        flyTimer += Time.deltaTime;
        if (flyTimer >= flyTime)
            Destroy(gameObject);
    }
    public void mFont_Roboto(string text, float color, Transform focusObject) {
        Font font = Resources.Load<Font>(FONT_PATH + "Roboto/" + "Roboto-Bold");
        textUI.text = text;
        textUI.font = font;
        switch (color) {
            case COLOR_RED:
                textUI.color = Color.red;
                break;
            case COLOR_GREEN:
                textUI.color = Color.green;
                break;
            case COLOR_YELLOW:
                textUI.color = Color.yellow;
                break;
        }
        lastYPos = focusObject.position.y + 3.5f;
        this.focusObject = focusObject;
    }
}
