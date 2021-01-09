using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    SpriteRenderer rend;
    public Sprite[] sprites = new Sprite[3];
    Vector3 mousePos;
    bool aiming = false;

    void Start()
    {
       rend = gameObject.GetComponent<SpriteRenderer>();
       rend.sprite = sprites[0];
    }

    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D col = Physics2D.OverlapPoint(mousePos);
        if (col != null && col.gameObject == this.gameObject)
        {
            rend.sprite = sprites[1];
            aiming = true; 
        }
        else
        {
            rend.sprite = sprites[0];
        }

        if (Input.GetMouseButton(1))
        {
            if (aiming){
                Debug.DrawRay(transform.position, transform.position - mousePos, Color.yellow);
            }
        } 

        if (Input.GetMouseButtonUp(1) && aiming)
        {
            aiming = false;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>().AddForce((transform.position - mousePos)*1000);
        }
    }
}
