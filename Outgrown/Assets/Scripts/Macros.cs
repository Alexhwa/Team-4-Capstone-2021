using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Macros
{
    public static Vector2 Rotate(Vector2 aPoint, float aDegree)
    {
        float rad = aDegree * Mathf.Deg2Rad;
        float s = Mathf.Sin(rad);
        float c = Mathf.Cos(rad);
        return new Vector2(
                aPoint.x * c - aPoint.y * s,
                aPoint.y * c + aPoint.x * s
            );
    }
}
