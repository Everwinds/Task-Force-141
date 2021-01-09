using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    [Range(1f,10f)]
    public float speed = 2f;
    public int currentLayer = 0;

    Rigidbody2D rb2d;
    Animator animator;
    Vector3 moveDir = Vector3.zero;
    bool movable = false;
    int directionIndex = 1;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (PauseMenu.Instance.paused)
        {
            animator.speed = 0;
            return;
        }
        
        HandleMovement();
        PlayerSkills.HandleCliming((Vector2)moveDir, ref currentLayer, this.transform);
    }

    private void FixedUpdate()
    {
        if (PauseMenu.Instance.paused) 
            return;
        if (movable)
            Move();
    }

    private void Move()
    {
        rb2d.velocity = moveDir * speed;
        animator.speed = 1;
        int dir = 1;
        if(moveDir.y>0)
        {
            if (moveDir.x > 0) dir = 1;
            else dir = 2;
        }
        else
        {
            if (moveDir.x < 0) dir = 3;
            else dir = 4;
        }
        if(directionIndex!=dir)
        {
            directionIndex = dir;
            switch (dir)
            {
                case 1:
                    animator.SetTrigger("UpRight");
                    break;

                case 2:
                    animator.SetTrigger("UpLeft");
                    break;

                case 3:
                    animator.SetTrigger("DownLeft");
                    break;

                case 4:
                    animator.SetTrigger("DownRight");
                    break;
            }
        }
    }

    private void HandleMovement()
    {
        Vector2 mouseDir = Input.mousePosition-Camera.main.WorldToScreenPoint(transform.position);
        if (Input.GetMouseButton(0))
        {
            if (!movable) movable = true;
            moveDir = mouseDir.normalized;
        }
        else
        {
            if (movable) movable = false;
            //moveDir = Vector3.zero;
            animator.speed = 0;
        }
    }
}
