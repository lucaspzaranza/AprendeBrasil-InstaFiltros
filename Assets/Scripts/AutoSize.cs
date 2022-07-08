using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoSize : MonoBehaviour
{
    public RectTransform rect;

    public void DoAutoSize(Vector2 size)
    { 
        rect.sizeDelta = size;
    }
}
