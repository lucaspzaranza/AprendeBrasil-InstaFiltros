using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Webcam : MonoBehaviour
{
    public WebCamTexture camTexture;
    public RawImage image;

    public delegate void WebcamActivated(Texture texture);
    public event WebcamActivated OnWebcamActivated;

    private void OnEnable()
    {
        StartCoroutine(ActivateWebcam());
    }

    private void OnDisable()
    {
        StopWebcam();
    }

    public IEnumerator ActivateWebcam()
    {
        if (camTexture != null)
            StopWebcam();
        else
        {
            WebCamDevice device = WebCamTexture.devices[0];
            camTexture = new WebCamTexture(device.name);
            image.texture = camTexture;

            camTexture.Play();

            yield return new WaitForEndOfFrame();

            OnWebcamActivated?.Invoke(camTexture);
        }
    }

    public void StopWebcam()
    {
        if(camTexture != null)
        {
            image.texture = null;
            camTexture.Stop();
            camTexture = null;
        }
    }
}
