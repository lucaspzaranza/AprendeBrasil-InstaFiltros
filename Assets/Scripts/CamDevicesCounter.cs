using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CamDevicesCounter : MonoBehaviour
{
    public TextMeshProUGUI camDevicesCounter;

    void Start()
    {
        camDevicesCounter.text = $"Cam devices: {WebCamTexture.devices.Length}.";

        if (WebCamTexture.devices.Length == 0)
            return;

        camDevicesCounter.text += System.Environment.NewLine;
        var devices = WebCamTexture.devices;

        foreach (var device in devices)
        {
            camDevicesCounter.text += System.Environment.NewLine;
            camDevicesCounter.text += device.name;
        }
    }
}
