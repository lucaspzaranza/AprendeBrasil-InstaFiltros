using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpsideDown : MonoBehaviour
{
    public GameObject camToReverse;
    public bool reverseUpside;
    private TweenSlider slider;

    private void OnEnable()
    {
        slider = GetComponent<TweenSlider>();
        slider.OnUpsideDown += HandleUpsideDown;
    }

    private void OnDisable()
    {
        slider.OnUpsideDown -= HandleUpsideDown;
        slider = null;
    }

    public void HandleUpsideDown()
    {
        var scale = camToReverse.transform.localScale;

        if (reverseUpside)
            camToReverse.transform.localScale = new Vector3(scale.x, -scale.y, 1);
        else
            camToReverse.transform.localScale = new Vector3(scale.x, Mathf.Abs(scale.y), 1);
    }
}
