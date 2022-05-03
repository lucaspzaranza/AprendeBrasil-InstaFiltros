using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapshotMaker : MonoBehaviour
{
    private Webcam _webcam;
    private Webcam Webcam
    {
        get
        {
            if(_webcam == null || !_webcam.gameObject.activeInHierarchy)
                _webcam = _webcam = FindObjectOfType<Webcam>();
            return _webcam;
        }

        set => _webcam = value;
    }

    public void TakeSnapshot()
    {
        Webcam.camTexture.Pause();
    }

    public void ResumeWebcam()
    {
        Webcam.camTexture.Play();
    }
}
