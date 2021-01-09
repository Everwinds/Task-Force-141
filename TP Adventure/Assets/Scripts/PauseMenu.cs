using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

[RequireComponent(typeof(CanvasGroup))]
public class PauseMenu : MonoBehaviour
{
    public static PauseMenu Instance;
    public GameObject titleTetx;
    public GameObject continueButton;
    public GameObject volumeText;
    public GameObject volumeSlider;
    public GameObject quitButton;
    public CinemachineVirtualCamera vCam;
    public Transform focusPoint;
    [HideInInspector]
    public bool paused = false;

    private CanvasGroup canvasGroup;
    private Transform previousFocus;
    private bool fading = false;

    private void Awake()
    {
        Instance = this;
        canvasGroup = GetComponent<CanvasGroup>();
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) Toggle();
    }

    public void Toggle()
    {
        // camera focus
        if (fading) return;
        if(paused)
        {
            paused = false;
            StartCoroutine(FadeOut());
        }
        else
        {
            paused = true;
            StartCoroutine(FadeIn());
        }
    }

    IEnumerator FadeIn()
    {
        previousFocus = vCam.Follow;
        vCam.Follow = focusPoint;
        fading = true;
        LeanTween.moveLocalZ(titleTetx, 0, 0.6f);
        yield return new WaitForSeconds(0.1f);
        LeanTween.alphaCanvas(canvasGroup, 1f, 1.2f);
        LeanTween.moveLocalZ(continueButton, 0f, 0.6f);
        yield return new WaitForSeconds(0.1f);
        LeanTween.moveLocalZ(volumeText, 0, 0.6f);
        LeanTween.moveLocalZ(volumeSlider, 0, 0.6f);
        yield return new WaitForSeconds(0.1f);
        LeanTween.moveLocalZ(quitButton, 0, 0.6f);

        yield return new WaitForSeconds(0.6f);
        fading = false;
    }

    IEnumerator FadeOut()
    {
        vCam.Follow = previousFocus;
        fading = true;
        LeanTween.alphaCanvas(canvasGroup, 0f, 0.8f);
        yield return new WaitForSeconds(1.2f);
        LeanTween.moveLocalZ(titleTetx, 0.8f, 0);
        LeanTween.moveLocalZ(continueButton, 0.8f, 0);
        LeanTween.moveLocalZ(volumeText, 0.8f, 0);
        LeanTween.moveLocalZ(volumeSlider, 0.8f, 0);
        LeanTween.moveLocalZ(quitButton, 0.8f, 0);
        fading = false;
    }
}
