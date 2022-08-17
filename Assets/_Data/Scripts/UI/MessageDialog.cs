using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageDialog : MyMonoBehaviour
{
    public const string FONT_PATH = "Font/";
    public const int COLOR_BLACK = 0;
    public const int COLOR_RED = 1;
    public const int COLOR_GREEN = 2;
    public const int COLOR_YELLOW = 3;

    public Text textUI;
    public float aliveTime = 1.5f;
    private float timer = 0;
    public Transform focusObject;

    protected override void LoadComponents()
    {
        textUI = transform.Find("Text").GetComponent<Text>();
    }

    private void Update() {
        if (focusObject == null)
            return;
        Vector3 tempPos = focusObject.position;
        tempPos.y += 3f;
        transform.position = tempPos;
        timer += Time.deltaTime;
        if (timer >= aliveTime) {
            if (focusObject == Pet.instance.transform)
                focusObject.GetComponent<Pet>().hasDrawDialog = false;
            Destroy(gameObject);
        }
    }

    public void mFont_Roboto(string text, int color, Transform focusObject) {
        Font font = Resources.Load<Font>(FONT_PATH + "Roboto/" + "Roboto-Bold");
        textUI.text = text;
        textUI.font = font;
        switch (color) {
            case COLOR_BLACK:
                this.textUI.color = Color.black;
                break;
            case COLOR_RED:
                this.textUI.color = Color.red;
                break;
            case COLOR_GREEN:
                this.textUI.color = Color.green;
                break;
            case COLOR_YELLOW:
                this.textUI.color = Color.yellow;
                break;
        }
        this.focusObject = focusObject;
    }
}
