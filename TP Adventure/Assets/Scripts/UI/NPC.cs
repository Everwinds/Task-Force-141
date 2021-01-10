using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class NPC : MonoBehaviour
{
    public Dialogue[] dialogues;
    public GameObject talkingBubble;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)) Talk();
        if (Input.GetKeyDown(KeyCode.S)) StopTalk();
    }

    void Awake()
    {
        dialogues[0].LoadDialogue();
    }

    public void Talk()
    {
        if (PauseMenu.Instance.paused) return;
        talkingBubble.SetActive(false);
        DialogueManager.Instance.StartDialogue(transform, dialogues[0]);
        animator.SetTrigger("Talk");
    }

    public void StopTalk()
    {
        DialogueManager.Instance.EndDialogue();
        animator.SetTrigger("Stop");
    }
}
