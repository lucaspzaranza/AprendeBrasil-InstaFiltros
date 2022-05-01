using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WebcamDuplicate : MonoBehaviour
{
    public RawImage rawImg;
    public Webcam webcam;

    private void OnEnable()
    {
        webcam.OnWebcamActivated += HandleWebcamActivated;
    }

    private void OnDisable()
    {
        webcam.OnWebcamActivated -= HandleWebcamActivated;
    }

    public void HandleWebcamActivated(Texture texture)
    {
        rawImg.texture = texture;
    }

    private IEnumerator HandlerSetup()
    {
        yield return new WaitForSeconds(0.5f);
        webcam.OnWebcamActivated += HandleWebcamActivated;
    }
}
