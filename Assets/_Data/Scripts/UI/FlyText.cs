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

    public Text textUI;

    protected override void Awake()
    {
        textUI = GetComponent<Text>();
    }

    public void SetText(string text, int x, int y) {

    }

    public void mFont_Roboto(string text, float x, float y, float color) {
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
        Vector3 screenPoint = mSystem.WorldToScreenPoint(new Vector3(x, y, 0));
        Vector2 anchoredPosition = transform.InverseTransformPoint(screenPoint);
        transform.position = anchoredPosition;
    }
}
