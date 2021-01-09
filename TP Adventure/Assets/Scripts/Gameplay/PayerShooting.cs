using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PayerShooting : MonoBehaviour
{
    public Transform target;
    public LineRenderer line;
    Vector2 mousePos;

    public Transform[] pointObjects = new Transform[3];
    Vector3[] points = new Vector3[80];
    public LineRenderer indicator;

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            StopCoroutine("ShootBack");
            StartCoroutine("ShootForward");
        }
        if (Input.GetMouseButtonUp(1))
        {
            StopCoroutine("ShootForward");
            StartCoroutine("ShootBack");
            StartCoroutine("Jump");
        }

        line.SetPosition(1, target.localPosition);
    }

    void CreatePath()
    {
        for (int i = 0; i < 80; i++)
        {
            Vector2 pos0 = Vector2.Lerp(pointObjects[0].position, pointObjects[1].position, i / 40f);
            Vector2 pos1 = Vector2.Lerp(pointObjects[1].position, pointObjects[2].position, i / 40f);
            Vector2 pos01 = Vector2.Lerp(pos0, pos1, i / 40f);
            points[i] = pos01;
        }

        indicator.SetPositions(points);
    }

    IEnumerator Jump()
    {
        for (int i = 0; i < 80; i++)
        {
            transform.position = points[i];
            yield return new WaitForSeconds(0.04f);
        }
    }

    IEnumerator ShootForward()
    {
        while (Vector2.Distance(target.position, mousePos) > 0)
        {
            CreatePath();
            target.position = Vector2.Lerp(target.position, mousePos, .2f);
            yield return new WaitForFixedUpdate();
        }
    }

    IEnumerator ShootBack()
    {
        while (Vector2.Distance(target.position, transform.position) > 0)
        {
            target.position = Vector2.Lerp(target.position, transform.position, .2f);
            yield return new WaitForFixedUpdate();
        }
    }
}
