using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransitionText : MonoBehaviour
{
    public string[] transitionTexts;
    public Text text;

    private CanvasGroup canvasGroup;
    private bool fading = false;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public IEnumerator FadeIn(int level)
    {
        if (fading) yield break;
        fading = true;
        canvasGroup.alpha = 0f;
        text.text = transitionTexts[level - 1];
        LeanTween.alphaCanvas(canvasGroup, 1f, 1f);
        yield return new WaitForSeconds(5f);
        LeanTween.alphaCanvas(canvasGroup, 0f, 1f);
        yield return new WaitForSeconds(1f);
        fading = false;
    }
}
