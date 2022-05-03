using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarouselSelector : MonoBehaviour
{
    public List<GameObject> carouselPanels;
    public List<GameObject> carouselBtns;
    public List<TweenSlider> sliders;
    public GameObject webcam;
    public GameObject multiplesWebcams;

    public Sprite defaultBtnSprite;
    public Sprite selectedBtnSprite;

    public int currentIndex = 0;

    public int leftXPos;
    public int rightXPos;
    public float transitionYPos; // Desktop = 1000f

    // Esse aqui � pra usar no c�digo
    public void SelectCarouselPanel(int newIndex)
    {
        carouselBtns[currentIndex].GetComponent<Image>().sprite = defaultBtnSprite;
        carouselBtns[newIndex].GetComponent<Image>().sprite = selectedBtnSprite;

        int difference = newIndex - currentIndex;

        if(newIndex > currentIndex) // getting a bigger index value
        {
            sliders[currentIndex].Slide(leftXPos);
            sliders[newIndex].Slide(0);

            if(difference > 0)
                SlideRemainingCarousels(currentIndex + 1, newIndex);
        }
        else
        {
            sliders[currentIndex].Slide(rightXPos);
            sliders[newIndex].Slide(0);
            if (difference < 0)
                SlideRemainingCarousels(currentIndex - 1, newIndex, false);
        }

        currentIndex = newIndex;
    }

    // Esses dois debaixo s�o pra usar pelo Editor, mas esse aqui
    // � pro caso de ter o caso de v�rias texturas de webcam
    public void SelectCarouselPanelAndActivateWebcam(int newIndex)
    {
        carouselBtns[currentIndex].GetComponent<Image>().sprite = defaultBtnSprite;
        carouselBtns[newIndex].GetComponent<Image>().sprite = selectedBtnSprite;

        int difference = newIndex - currentIndex;

        StartCoroutine(sliders[currentIndex].ChangeCamTexturePos(transitionYPos, 0f)); 
        StartCoroutine(sliders[currentIndex].ChangeCamMultipleTexturePos(transitionYPos, 0f));

        if (newIndex > currentIndex) // getting a bigger index value
        {
            sliders[currentIndex].Slide(leftXPos);
            sliders[newIndex].Slide(0);

            if (difference > 0)
                SlideRemainingCarousels(currentIndex + 1, newIndex);

            if (newIndex == carouselBtns.Count - 1)
                StartCoroutine(sliders[newIndex].ToggleActivateMultipleTextures(true, 0.5f));
        }
        else
        {
            sliders[currentIndex].Slide(rightXPos);
            sliders[newIndex].Slide(0);
            if (difference < 0)
                SlideRemainingCarousels(currentIndex - 1, newIndex, false);
        }

        float duration = sliders[currentIndex].duration;

        StartCoroutine(sliders[currentIndex].ChangeCamTexturePos(0f, duration));
        StartCoroutine(sliders[currentIndex].ChangeCamMultipleTexturePos(0f, duration));

        currentIndex = newIndex;
    }

    // Esse aqui � pra usar se s� tiver uma textura de webcam.
    // Uma merda, eu sei, mas o tempo urge.
    // Pe�o perd�o ao meu eu do futuro por esta bosta de c�digo.
    public void SelectCarouselPanelAndChangeWebcamPos(int newIndex)
    {
        carouselBtns[currentIndex].GetComponent<Image>().sprite = defaultBtnSprite;
        carouselBtns[newIndex].GetComponent<Image>().sprite = selectedBtnSprite;

        int difference = newIndex - currentIndex;

        StartCoroutine(sliders[currentIndex].ChangeCamTexturePos(transitionYPos, 0f));

        if (newIndex > currentIndex) // getting a bigger index value
        {
            sliders[currentIndex].Slide(leftXPos);
            sliders[newIndex].Slide(0);

            if (difference > 0)
                SlideRemainingCarousels(currentIndex + 1, newIndex);
        }
        else
        {
            sliders[currentIndex].Slide(rightXPos);
            sliders[newIndex].Slide(0);
            if (difference < 0)
                SlideRemainingCarousels(currentIndex - 1, newIndex, false);
        }

        float duration = sliders[currentIndex].duration;

        StartCoroutine(sliders[currentIndex].ChangeCamTexturePos(0f, duration));

        currentIndex = newIndex;
    }

    private void ActivateWebcam()
    {
        webcam.SetActive(true);
    }

    private void DeactivateWebcam()
    {
        webcam.SetActive(true);
    }

    private void SlideRemainingCarousels(int startIndex, int endIndex, bool rightToLeft = true)
    {
        if(rightToLeft)
        {
            for (int i = startIndex; i < endIndex; i++)
            {
                carouselPanels[i].transform.position = new Vector2(leftXPos, carouselPanels[i].transform.localPosition.y);
            }
        }
        else
        {
            for (int i = startIndex; i > endIndex; i--)
            {
                carouselPanels[i].transform.position = new Vector2(rightXPos, carouselPanels[i].transform.localPosition.y);
            }
        }
    }
}
