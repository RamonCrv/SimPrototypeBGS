using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vector2Converter : MonoBehaviour
{
    public static Vector2 ConvertToSigleDirection(Vector2 vector2)
    {
        
        if (vector2.x != 1 && vector2.x != -1) 
        {
            vector2.x = 0;

        }
        else
        {
            vector2.x = Mathf.Round(vector2.x);
        }

        if (vector2.y != 1 && vector2.y != -1)
        {
            vector2.y = 0;
        }
        else
        {
            vector2.y = Mathf.Round(vector2.y);
        }


        return vector2;
    }
}
