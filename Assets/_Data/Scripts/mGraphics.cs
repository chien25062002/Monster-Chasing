using System;
using UnityEngine;

public class mGraphics
{
    public static void DrawImageByDrawTexture(MyImage image, float x, float y) {
        GUI.DrawTexture(new Rect(x - (image.width / 2), y - (image.height / 2), image.width, image.height), image.texture);
    }
}
