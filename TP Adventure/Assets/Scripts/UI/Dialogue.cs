using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Dialogue : ScriptableObject
{
    public TextAsset textFile;
    [HideInInspector]
    public string[] lines;

    private int pointer = 0;
    private int count = 0;

    public void LoadDialogue()
    {
        lines = textFile.ToString().Split('\n');
        count = lines.Length;
        pointer = 0;
    }

    public string GetLine()
    {
        if (pointer < count) return lines[pointer++];
        else return null;
    }

    public void Reset()
    {
        pointer = 0;
    }
}
