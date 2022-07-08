using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoMask : MonoBehaviour
{
    public RectMask2D mask;

    public void DoAutoMask(Vector4 padding)
    {
        mask.padding = padding;
    }
}

/*
    X = Left
    Y = Bottom
    Z = Right
    W = Top
*/