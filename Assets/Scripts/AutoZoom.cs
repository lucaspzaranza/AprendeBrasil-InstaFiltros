using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AutoZoom : MonoBehaviour
{
    public GameObject camToZoom;

    private void OnEnable()
    {
        TweenSlider.OnSlideDone += ZoomByUnits;
    }

    private void OnDisable()
    {
        TweenSlider.OnSlideDone -= ZoomByUnits;
    }

    public void ZoomByUnits(float divideBy)
    {
        var scale = camToZoom.transform.localScale.normalized;
        float signal = scale.x < 0 ? -1f : 1f;
        float scaleX = (1f / divideBy) * signal;

        if (divideBy > 0)
            camToZoom.transform.localScale = new Vector3(scaleX, 1f / divideBy, 1);
        else
            camToZoom.transform.localScale = new Vector3(1f, 1f, 1f);
    }
}
