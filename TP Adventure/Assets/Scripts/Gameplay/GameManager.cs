using System.Collections;
using System.Collections.Generic;
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
            //put other objects that need mouse clicking
        }
        else if (currentHovering != null)
        {
            if (currentHovering.CompareTag("Hook"))
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
