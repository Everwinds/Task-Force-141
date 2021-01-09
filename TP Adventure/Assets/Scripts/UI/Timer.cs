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

    private void Start()
    {
        text = GetComponent<Text>();
        ResetTimer();
    }

    void Update()
    {
        if (PauseMenu.Instance.paused || reached) return;
        if(timer<=0)
        {
            reached = true;
            timer = 0;
            text.text = "Time up!";
        }
        text.text = timer.ToString("F1") + "s";
        timer -= Time.deltaTime;
    }

    public void ResetTimer()
    {
        timer = time;
        reached = false;
    }
}
