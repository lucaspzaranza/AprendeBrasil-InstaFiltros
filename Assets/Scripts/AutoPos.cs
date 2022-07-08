using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoPos : MonoBehaviour
{
    public RectTransform rect;

   public void DoAutoPos(Vector2 newPos)
    {
        rect.transform.localPosition = newPos;
    }
}
