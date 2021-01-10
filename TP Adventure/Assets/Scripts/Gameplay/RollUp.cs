using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollUp : MonoBehaviour
{
    Vector2 dir;

    public void Throw(int up, Vector2 d)
    {
        dir = d;
        if (d.x < 0) 
            transform.localScale *= new Vector2(-1, 1);
        if (d.y < 0) 
            transform.localScale *= new Vector2(1, -1);
        StartCoroutine("Up", up);
    }

    IEnumerator Up(int up)
    {
        yield return new WaitForSeconds(.5f);  
        for (int i = 0; i < 30; i++)
        {
            transform.position += Vector3.up * up * .08f;
            yield return new WaitForSeconds(.01f);  
        }
        for (int i = 0; i < 8; i++)
        {
            transform.Translate(dir * .05f);
            yield return new WaitForSeconds(.01f);  
        }

        yield return new WaitForSeconds(2.15f);
        Destroy(this.gameObject);
    }

}
