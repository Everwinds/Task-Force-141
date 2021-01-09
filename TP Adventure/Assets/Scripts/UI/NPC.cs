using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public Dialogue[] dialogues;
    
    void Awake()
    {
        dialogues[0].LoadDialogue();
    }
}
