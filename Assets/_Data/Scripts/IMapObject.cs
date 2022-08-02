using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMapObject
{
    public float GetX();
    public float GetY();
    public bool IsInvisible();
}
