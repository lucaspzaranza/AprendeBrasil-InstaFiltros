using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class AutoScaler : MonoBehaviour
{
    public bool loopScale;
    public float duration;
    public Vector2 finalScale;
    void Start()
    {
        ScaleGameObject();
    }

    public void ScaleGameObject()
    {
        if (loopScale)
            gameObject.transform.DOScale(finalScale, duration).SetLoops(-1, LoopType.Yoyo);
        else
            gameObject.transform.DOScale(finalScale, duration);
    }
}
