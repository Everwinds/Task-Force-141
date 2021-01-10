using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : MonoBehaviour
{
    public GameObject wave;
    private bool playing = true;
    private Animator animator;
    PauseMenu pauseMenu;

    void Start()
    {
        animator = GetComponent<Animator>();
        pauseMenu = PauseMenu.Instance;
    }
    
    void Update()
    {
        if (pauseMenu.volume < 0.5f)
        {
            if (playing) Toggle(false);
        }
        else
        {
            if (!playing) Toggle(true);
        }

    }

    private void Toggle(bool onOff)
    {
        playing = onOff;
        animator.speed = onOff ? 1 : 0;
        wave.GetComponent<Animator>().speed = onOff ? 1 : 0;
        wave.SetActive(onOff);
    }
}
