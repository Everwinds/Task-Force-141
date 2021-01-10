using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [Header("Optional")]
    public NPC TalkTriggerer;
    public bool WinTrigger = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (TalkTriggerer != null) TalkTriggerer.Talk();
        else if (WinTrigger) StartCoroutine(LevelManager.Instance.NextLevel());
        else GameStateManager.Instance.Die();

    }
}
