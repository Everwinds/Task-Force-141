using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;
    public Typewritter typewritter;
    public GameObject startButton;
    public GameObject endButton;

    private Dialogue curDialogue;
    
    private void Awake()
    {
        Instance = this;
    }

    public void Open()
    {
        if (!curDialogue) return;
        GetComponent<Image>().enabled = true;
        GetComponent<Button>().enabled = true;
        typewritter.textBox.enabled = true;
        PlayNextLine();
    }

    public void Close()
    {
        GetComponent<Image>().enabled = false;
        GetComponent<Button>().enabled = false;
        typewritter.Stop();
        typewritter.textBox.enabled = false;
        if (curDialogue) curDialogue.Reset();
        startButton.SetActive(true);
        endButton.SetActive(false);
    }

    public void SetDialogue(Dialogue dialogue)
    {
        if (curDialogue) curDialogue.Reset();
        curDialogue = dialogue;
    }

    // Always set the first dialogue. For testing purpose
    public void SetDialogue(NPC npc)
    {
        if (npc.dialogues[0])
        {
            SetDialogue(npc.dialogues[0]);
            startButton.transform.GetChild(0).GetComponent<Text>().text = "和" + npc.name + "开始对话吧!";
        }
    }

    public void PlayNextLine()
    {
        if (!curDialogue)
        {
            Debug.LogError("curDialogue not set!");
            return;
        }
        if (!typewritter.isReady) return;
        //textBox.text = curDialogue.GetLine();
        string line = curDialogue.GetLine();
        if(line!=null) typewritter.Write(line);
        else Close();
    }
}
