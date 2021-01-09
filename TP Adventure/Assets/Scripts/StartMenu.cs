using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public GameObject camera;
    public GameObject background;
    public GameObject curtain;

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
        camera.SetActive(false);
        LeanTween.alpha(curtain.GetComponent<RectTransform>(), 0f, 1f);
        yield return new WaitForSeconds(1f);
        SceneManager.UnloadSceneAsync(0);
    }
}
