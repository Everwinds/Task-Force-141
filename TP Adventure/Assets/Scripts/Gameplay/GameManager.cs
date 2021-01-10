using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    Vector2 mousePos;
    GameObject currentHovering = null, shooting;
    bool shootingClicked;

    void Awake()
    {
        shooting = GameObject.Find("Player");
    }

    void Update()
    {
        SpriteRenderer[] spriteList = GameObject.FindObjectsOfType<SpriteRenderer>();
        foreach (SpriteRenderer spr in spriteList)
        {
            short tagNum;
            bool isParsable = Int16.TryParse(spr.tag, out tagNum);
            if (isParsable && 
                tagNum >= shooting.GetComponent<PlayerMovement>().currentLayer &&
                Vector2.Distance(spr.transform.position, shooting.transform.position) < 1.8f &&
                spr.sortingOrder >= shooting.GetComponent<SpriteRenderer>().sortingOrder)
            {
                spr.color = new Color(1, 1, 1, .5f);
            }
            else
            {
                spr.color = Color.white;
            }
        }


        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D col = Physics2D.OverlapPoint(mousePos);
        if (col != null)
        {
            currentHovering = col.gameObject;
            if (currentHovering.CompareTag("Hook") && !currentHovering.GetComponent<Hook>().used)
            {
                currentHovering.SendMessage("OnHover");
                if (Input.GetMouseButtonDown(1))
                {
                    currentHovering.SendMessage("Use");
                    shooting.SendMessage("OnHook", col.transform);
                    shootingClicked = true;
                }
            }
            
            if (currentHovering.CompareTag("Handle"))
            {
                currentHovering.SendMessage("OnHover");
                if (Input.GetMouseButtonDown(1))
                {
                    shooting.SendMessage("GoUp");
                }
            }
        }
        else if (currentHovering != null)
        {
            if (currentHovering.CompareTag("Hook"))
            {
                currentHovering.SendMessage("OnExit");
                currentHovering = null;
            }

            if (currentHovering.CompareTag("Handle"))
            {
                currentHovering.SendMessage("OnExit");
                currentHovering = null;
            }
        }

        if (shootingClicked)
        {
            shooting.SendMessage("Aiming", mousePos);
            if (Input.GetMouseButtonUp(1))
            {
                shooting.SendMessage("CancleAiming");
                shootingClicked = false;
            }
        }

    }
}
