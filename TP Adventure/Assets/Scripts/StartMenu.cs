using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    public GameObject mainCamera;
    public GameObject background;
    public GameObject curtain;
    public GameObject button;

    public void GameStart()
    {
        StartCoroutine(GameStartCoroutine());
    }

    public IEnumerator GameStartCoroutine()
    {
        LeanTween.alpha(curtain.GetComponent<RectTransform>(), 1f, 1f);
        yield return new WaitForSeconds(1f);
        background.SetActive(false);
        SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
        yield return new WaitForSeconds(0.2f);
        mainCamera.SetActive(false);
        button.SetActive(false);
        LeanTween.alpha(curtain.GetComponent<RectTransform>(), 0f, 1f);
        yield return new WaitForSeconds(1f);
        SceneManager.UnloadSceneAsync(0);
    }

    private void OnEnable()
    {
        curtain.GetComponent<Image>().color = new Color(0, 0, 0, 1f);
        LeanTween.alpha(curtain.GetComponent<RectTransform>(), 0f, 1f);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
