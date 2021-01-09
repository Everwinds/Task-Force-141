using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Typewritter : MonoBehaviour
{
    [Range(0.01f,0.1f)]
    public float speed = 0.03f;
    public bool isReady { get { return text == ""; } }
    public Text textBox;

    private string text = "";

    public void Write(string str)
    {
        if (!isReady) return;
        text = str;
        StartCoroutine(Writing());
    }

    public void Stop()
    {
        StopAllCoroutines();
        text = "";
    }

    IEnumerator Writing()
    {
        for(int i=0; i<text.Length; i++)
        {
            textBox.text = text.Substring(0, i);
            yield return new WaitForSeconds(0.04f);
        }
        text = "";
    }
}
