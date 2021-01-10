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
    public Slider volumeSlider;
    public GameObject quitButton;
    public CinemachineVirtualCamera vCam;
    public Transform focusPoint;
    [HideInInspector]
    public bool paused = false;
    public float volume = 1f;

    private CanvasGroup canvasGroup;
    private Transform previousFocus;
    private bool fading = false;
    private bool talking = false;
    private bool shownMenu = false;

    private void Awake()
    {
        Instance = this;
        canvasGroup = GetComponent<CanvasGroup>();
        volumeSlider.onValueChanged.AddListener(delegate { UpdateVolume(); });
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) Toggle();
    }

    public void Toggle()
    {
        if (fading || GameStateManager.Instance.die) return;
        if(talking)
        {
            if(shownMenu) StartCoroutine(FadeOut());
            else StartCoroutine(FadeIn());
        }
        else
        {
            if (paused)
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
    }

    public void TalkingPauseToggle()
    {
        if(talking)
        {
            talking = false;
            paused = false;
        }
        else
        {
            talking = true;
            paused = true;
            shownMenu = false;
        }
    }

    IEnumerator FadeIn()
    {
        previousFocus = vCam.Follow;
        vCam.Follow = focusPoint;
        if (talking) shownMenu = true;
        fading = true;
        LeanTween.moveLocalZ(titleTetx, 0, 0.6f);
        yield return new WaitForSeconds(0.1f);
        LeanTween.alphaCanvas(canvasGroup, 1f, 1.2f);
        LeanTween.moveLocalZ(continueButton, 0f, 0.6f);
        yield return new WaitForSeconds(0.1f);
        LeanTween.moveLocalZ(volumeText, 0, 0.6f);
        LeanTween.moveLocalZ(volumeSlider.gameObject, 0, 0.6f);
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
        LeanTween.moveLocalZ(volumeSlider.gameObject, 0.8f, 0);
        LeanTween.moveLocalZ(quitButton, 0.8f, 0);
        fading = false;
        if (talking) shownMenu = false;
    }

    private void UpdateVolume()
    {
        volume = volumeSlider.value;
    }
}
