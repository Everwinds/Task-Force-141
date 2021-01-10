using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

[RequireComponent(typeof(CanvasGroup))]
public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;
    public Typewritter typewritter;
    public CinemachineVirtualCamera vCam;
    private CanvasGroup canvasGroup;
    private Dialogue curDialogue;
    private Transform preTarget;
    
    private void Awake()
    {
        Instance = this;
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void StartDialogue(Transform pos, Dialogue dialogue)
    {
        if (curDialogue == dialogue) curDialogue.ResetDialogue();
        else curDialogue = dialogue;
        transform.position = pos.position;
        PauseMenu.Instance.TalkingPauseToggle();

        preTarget = vCam.Follow;
        vCam.Follow = pos;
        LeanTween.alphaCanvas(canvasGroup, 1f, 0.2f);
        PlayNextLine();
    }

    public void EndDialogue()
    {
        typewritter.Stop();

        PauseMenu.Instance.TalkingPauseToggle();

        vCam.Follow = preTarget;
        preTarget = null;
        LeanTween.alphaCanvas(canvasGroup, 0f, 0.2f);
        if (curDialogue) curDialogue.ResetDialogue();
    }
    
    public void PlayNextLine()
    {
        if (!curDialogue)
        {
            Debug.LogError("curDialogue not set!");
            return;
        }
        if (!typewritter.isReady) return;
        string line = curDialogue.GetLine();
        if(line!=null) typewritter.Write(line);
        else EndDialogue();
    }
}
