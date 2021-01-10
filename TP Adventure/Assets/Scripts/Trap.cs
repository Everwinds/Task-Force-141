using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [Header("Optional")]
    public NPC TalkTriggerer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (TalkTriggerer == null) GameStateManager.Instance.Die();
        else TalkTriggerer.Talk();

    }
}
