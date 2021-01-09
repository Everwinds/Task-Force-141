using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Flicker : MonoBehaviour
{
    private bool flickering = false;
    private Light2D halo;

    private void Start()
    {
        flickering = true;
        halo = GetComponent<Light2D>();
        StartCoroutine(Flickering());
    }

    public void StartFlicker()
    {
        flickering = true;
    }

    public void StopFlicker()
    {
        flickering = false;
    }

    IEnumerator Flickering()
    {
        float waitTime = 0.5f;
        while(flickering)
        {
            halo.intensity = 0.6f;
            yield return new WaitForSeconds(waitTime);
            waitTime = Random.Range(0.05f, 0.5f);
            halo.intensity = 1f;
            yield return new WaitForSeconds(waitTime);
            waitTime = Random.Range(0.05f, 0.5f);
        }
    }
}
