using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System;
using System.Globalization;
using UnityEngine.UI;

public class TakeScreenshot : MonoBehaviour
{
    public static TakeScreenshot instance;

    public static event Action OnScreenshotTaken;
    public static event Action OnDownloadEnd;

    public Image saveImageBtn;

    private WebGLDownload _webGLDownload;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        _webGLDownload = GetComponent<WebGLDownload>();
    }

    public void Screenshot()
    {
        saveImageBtn.enabled = false;
        StartCoroutine(UploadPNG());        
    }

    IEnumerator UploadPNG()
    {
        OnScreenshotTaken?.Invoke();
        // We should only read the screen after all rendering is complete
        yield return new WaitForEndOfFrame();

        // Create a texture the size of the screen, RGB24 format
        int width = Screen.width;
        int height = Screen.height;
        var tex = new Texture2D(width, height, TextureFormat.RGB24, false);

        // Read screen contents into the texture
        tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        tex.Apply();

        // Encode texture into PNG
        byte[] bytes = tex.EncodeToPNG();
        Destroy(tex);

        //string ToBase64String byte[]
        string encodedText = System.Convert.ToBase64String(bytes);

        var image_url = "data:image/png;base64," + encodedText;

        //Debug.Log(image_url);

        string name = "picture " + DateTime.Now.ToString("yyyy-dd-M HH-mm-ss");
        _webGLDownload.DownloadFile(bytes, name, "png");

        saveImageBtn.enabled = true;
        OnDownloadEnd?.Invoke();
    }
}