using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance;
    public CanvasGroup dieCanvasGroup;
    public bool die = false;

    private void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(die)
        {
            if (Input.GetKeyDown(KeyCode.Space)) GetComponent<LevelManager>().ReloadLevel();
        }
    }

    public void Die()
    {
        LeanTween.alphaCanvas(dieCanvasGroup, 1f, 1f);
        PauseMenu.Instance.paused = true;
        die = true;
    }

    public void ResetGameState()
    {
        dieCanvasGroup.alpha = 0;
        PauseMenu.Instance.paused = false;
        die = false;
    }
}
