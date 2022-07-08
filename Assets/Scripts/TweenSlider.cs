using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;

public class TweenSlider : MonoBehaviour
{
    public Canvas canvas;
    public int index;
    public float duration;
    public CarouselSelector carousel;
    public Webcam webcam;
    public GameObject webcamMask;

    public Vector2 initPos;
    public bool hasMultipleTextures;
    public bool zoomScale;
    public bool upsideDown;
    public bool changeSize;
    public bool changePos;
    public bool changeMask;
    public float divideBy;
    public float transitionYPos; // Desktop = 1000f

    public Vector2 webcamSize;
    public Vector2 webcamPos;
    public Vector4 maskPadding;
    private Vector2 camInitPos;

    public delegate void SlideDone(float divideBy);
    public static event SlideDone OnSlideDone;

    public delegate void UpsideDown();
    public event UpsideDown OnUpsideDown;

    public void Slide(int finalX)
    {
        transform.DOMoveX(finalX, duration);

        if (zoomScale)
            OnSlideDone?.Invoke(divideBy);

        if (upsideDown)
            OnUpsideDown?.Invoke();

        if (changeSize)
            webcam.GetComponent<AutoSize>().DoAutoSize(webcamSize);

        StartCoroutine(nameof(ChangePos));

        if (changeMask)
            ChangeMask();
    }

    private void OnEnable()
    {
        camInitPos = webcam.transform.localPosition;
    }

    public IEnumerator ChangePos()
    {
        yield return new WaitForSeconds(duration + 0.01f);

        if(changePos)
            webcam.GetComponent<AutoPos>().DoAutoPos(webcamPos);
        else
            webcam.GetComponent<AutoPos>().DoAutoPos(camInitPos);
    }

    public void ChangeMask()
    {
        webcamMask.GetComponent<AutoMask>().DoAutoMask(maskPadding);
    }

    public void BeginDrag()
    {
        if(hasMultipleTextures)
            StartCoroutine(ChangeCamMultipleTexturePos(transitionYPos * 2f, 0f));
        else
            StartCoroutine(ChangeCamTexturePos(transitionYPos, 0f));
        initPos = transform.localPosition;
    }

    public IEnumerator ChangeCamTexturePos(float yPos, float delay)
    {
        yield return new WaitForSeconds(delay);

        if(carousel.multiplesWebcams != null && carousel.multiplesWebcams.activeInHierarchy)
        {
            StartCoroutine(ToggleActivateSingleWebcam(true, 0.5f));
            yield return new WaitForSeconds(delay);
        }

        Vector3 pos = carousel.webcam.transform.position;
        carousel.webcam.transform.localPosition = new Vector3(pos.x, yPos, pos.z);
    }

    public IEnumerator ChangeCamMultipleTexturePos(float yPos, float delay)
    {
        if(carousel.multiplesWebcams != null)
        {
            Vector3 pos = carousel.multiplesWebcams.transform.localPosition;
            yield return new WaitForSeconds(delay);
            carousel.multiplesWebcams.transform.localPosition = new Vector3(pos.x, yPos, pos.z);
        }
    }

    public IEnumerator ToggleActivateMultipleTextures(bool val, float delay)
    {
        yield return new WaitForSeconds(delay);
        carousel.webcam.SetActive(!val);

        if(carousel.multiplesWebcams != null)
            carousel.multiplesWebcams.SetActive(val);
    }

    public IEnumerator ToggleActivateSingleWebcam(bool val, float delay)
    {
        if(carousel.multiplesWebcams != null)
            carousel.multiplesWebcams.SetActive(!val);

        yield return new WaitForSeconds(delay);
        carousel.webcam.SetActive(val);
    }

    public void DragPosition(BaseEventData eventData)
    {
        PointerEventData pointerEventData = (PointerEventData)eventData;
        Vector2 position;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform)canvas.transform,
            pointerEventData.position,
            canvas.worldCamera,
            out position);

        // y SEMPRE zero, senão dá merda!
        Vector2 finalPos = new Vector2(canvas.transform.TransformPoint(position).x, transform.localPosition.y);
        transform.position = finalPos;
    }

    public void ReturnToInitialPos()
    {
        if(hasMultipleTextures)
            StartCoroutine(ChangeCamMultipleTexturePos(0f, duration));
        else
        {
            if (changePos)
                StartCoroutine(ChangeCamTexturePos(webcamPos.y, duration + 0.01f));
            else
                StartCoroutine(ChangeCamTexturePos(camInitPos.y, duration + 0.01f));
        }
        transform.DOMoveX(0, duration); 
    }

    // Que código bosta, o que a pressa não faz...
    public void GetNextPanel()
    {
        Vector2 finalPos = transform.position;
        int nextIndex;

        if (finalPos.x - initPos.x < 0) // Left direction, next index
        {
            nextIndex = index + 1;
            if (nextIndex < carousel.carouselPanels.Count)
                carousel.SelectCarouselPanel(nextIndex);
            else
                ReturnToInitialPos();
        }
        else // Right direction, previous index
        {
            nextIndex = index - 1;
            if(nextIndex > -1)
                carousel.SelectCarouselPanel(nextIndex);
            else
                ReturnToInitialPos();
        }

        if (nextIndex == carousel.carouselPanels.Count - 1 && carousel.sliders[nextIndex].hasMultipleTextures)
        {
            StartCoroutine(ToggleActivateMultipleTextures(true, 0.5f));
            StartCoroutine(ChangeCamMultipleTexturePos(0f, 0.5f));
        }
        else if (nextIndex == carousel.carouselPanels.Count - 1 && !hasMultipleTextures)
        {
            StartCoroutine(ToggleActivateSingleWebcam(true, 0.5f));
            StartCoroutine(ChangeCamTexturePos(webcamPos.y, duration));
        }
        else if(index == carousel.carouselPanels.Count - 1 && nextIndex == index - 1)
        {
            StartCoroutine(ToggleActivateSingleWebcam(true, 0.5f));
            //StartCoroutine(ChangeCamTexturePos(webcamPos.y, 0.5f));
            //StartCoroutine(ChangeCamTexturePos(carousel.sliders[nextIndex].webcamPos.y, 0.5f));
        }
        else if(index != carousel.carouselPanels.Count - 1)
            StartCoroutine(ChangeCamTexturePos(webcamPos.y, duration));
    }
}
