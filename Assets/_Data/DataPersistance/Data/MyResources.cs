using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyResources
{
    public const string path = "Images/";
    public static Sprite LoadSprite(string name) {
        return Resources.Load<Sprite>(path + name);
    }
}
