using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndScene : MonoBehaviour
{
    public GameObject curtain;

    void Start()
    {
        curtain.GetComponent<Image>().color = new Color(0, 0, 0, 1f);
        LeanTween.alpha(curtain.GetComponent<RectTransform>(), 0f, 1f);
    }

    public void ToStartMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
