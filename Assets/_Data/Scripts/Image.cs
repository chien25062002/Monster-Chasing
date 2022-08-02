using System;
using System.IO;
using UnityEngine;

public class MyImage
{
    public const string path = "Images/";
    public Texture2D texture;
    public MyImage image;
    public int width;
    public int height;

    public static MyImage CreateImage(string fileName) {
        MyImage image = new MyImage();
        Texture2D texture2D = Resources.Load(path + fileName) as Texture2D;
        if (texture2D == null)
		{
			throw new Exception("NULL POINTER EXCEPTION AT Image CreateImage " + fileName);
		}
        image.texture = texture2D;
        image.width = image.texture.width;
        image.height = image.texture.height;
        SetTextureQuality(image.texture);
        return image;
    }

    public static void SetTextureQuality(Texture2D texture) {
        texture.anisoLevel = 0;
		texture.filterMode = FilterMode.Point;
		texture.mipMapBias = 0f;
		texture.wrapMode = TextureWrapMode.Clamp;
    }

    public static Sprite CreateSprite(string fileName) {
        MyImage image = CreateImage(fileName);
        return Sprite.Create(image.texture, new Rect(0, 0, image.width, image.height), new Vector2(0.5f, 0.5f));
    }
}
