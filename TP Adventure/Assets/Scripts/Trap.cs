using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public int targetLayer = 0;
    [Header("Optional")]
    public NPC TalkTriggerer;
    public bool WinTrigger = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMovement pm = collision.gameObject.GetComponent<PlayerMovement>();
        if (!pm || pm.currentLayer != targetLayer) return;
        if (TalkTriggerer != null) TalkTriggerer.Talk();
        else if (WinTrigger) StartCoroutine(LevelManager.Instance.NextLevel());
        else GameStateManager.Instance.Die();

    }
}
