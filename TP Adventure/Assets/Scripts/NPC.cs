using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class NPC : MonoBehaviour
{
    public Dialogue[] dialogues;
    public GameObject talkingBubble;
    public GameObject talkTrigger;
    [HideInInspector]
    public bool talked = false;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {

    }

    void Awake()
    {
        dialogues[0].LoadDialogue();
    }

    public void Talk()
    {
        if (PauseMenu.Instance.paused || talked) return;
        talkingBubble.SetActive(false);
        talkTrigger.SetActive(false);
        DialogueManager.Instance.StartDialogue(transform, dialogues[0]);
        animator.SetTrigger("Talk");
        talked = true;
    }

    public void StopTalk()
    {
        DialogueManager.Instance.EndDialogue();
        animator.SetTrigger("Stop");
    }
}
