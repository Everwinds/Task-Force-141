using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float time = 20f;
    public bool reached = false;

    private Text text;
    private float timer;
    private bool pause;

    private void Start()
    {
        text = GetComponent<Text>();
        ResetTimer();
    }

    void Update()
    {
        if (PauseMenu.Instance.paused || reached || pause) return;
        if(timer<=0)
        {
            reached = true;
            timer = 0;
            text.text = "Time up!";
            GameStateManager.Instance.Die();
        }
        else
        {
            text.text = timer.ToString("F1") + "s";
            timer -= Time.deltaTime;
        }
    }

    public void Pause()
    {
        pause = true;
    }

    public void Resume()
    {
        pause = false;
    }

    public void ResetTimer()
    {
        timer = time;
        text.text = timer.ToString("F1") + "s";
        reached = false;
    }
}
