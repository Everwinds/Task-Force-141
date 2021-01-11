using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PayerShooting : MonoBehaviour
{
    public Transform target;
    public LineRenderer line;

    Transform hookPos, endPos;
    Vector3[] points = new Vector3[100];

    public LineRenderer indicator;
    Vector2 originalEndPos;

    public void OnHook(Transform hookTrans)
    {
        hookPos = hookTrans;
        endPos = hookTrans.GetChild(0).transform;

        originalEndPos = endPos.position;
        indicator.enabled = true;
        line.enabled = true;
        StopCoroutine("ShootBack");
        StartCoroutine("ShootForward", hookPos);
    }

    public void Aiming(Vector2 mousePos)
    {
        //change end pos
        endPos.position = originalEndPos + ((Vector2)hookPos.position - mousePos) / 2f;
        
        for (int i = 0; i < 100; i++)
        {
            Vector2 pos0 = Vector2.Lerp(transform.position, hookPos.position, i / 50f);
            Vector2 pos1 = Vector2.Lerp(hookPos.position, endPos.position, i / 50f);
            Vector2 pos01 = Vector2.Lerp(pos0, pos1, i / 50f);
            points[i] = pos01;
        }

        indicator.SetPositions(points);
    }

    public void CancleAiming()
    {
        indicator.enabled = false;
        endPos.position = originalEndPos;
        StopCoroutine("ShootForward");
        StartCoroutine("ShootBack");
        StartCoroutine("Jump");
    }

    IEnumerator Jump()
    {
        for (int i = 0; i < 100; i++)
        {
            transform.position = points[i];
            yield return new WaitForFixedUpdate();
        }
    }

    IEnumerator ShootForward(Transform hookPos)
    {
        while (Vector2.Distance(target.position, hookPos.position) > 0)
        {
            line.SetPosition(1, target.localPosition);
            target.position = Vector2.Lerp(target.position, hookPos.position, .2f);
            yield return new WaitForFixedUpdate();
        }
    }

    IEnumerator ShootBack()
    {
        while (target.localPosition.magnitude > 0)
        {
            line.SetPosition(1, target.localPosition);
            target.localPosition = Vector2.Lerp(target.localPosition, Vector2.zero, .2f);
            yield return new WaitForFixedUpdate();
        }
        line.enabled = false;
    }
}
