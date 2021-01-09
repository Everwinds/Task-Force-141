using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    [Range(1f,10f)]
    public float speed = 2f;

    private Rigidbody2D rb2d;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Vector3 moveDir = Vector3.zero;
    private bool movable = false;
    private int directionIndex = 1;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (PauseMenu.Instance.paused)
        {
            animator.speed = 0;
            return;
        }
        if (Input.GetMouseButton(0))
        {
            if (!movable) movable = true;
            moveDir = Input.mousePosition-Camera.main.WorldToScreenPoint(transform.position);
            //Debug.DrawLine(transform.position, transform.position + moveDir.normalized, Color.red, 0.1f);
        }
        else
        {
            if (movable) movable = false;
            moveDir = Vector3.zero;
            animator.speed = 0;
        }
    }

    private void FixedUpdate()
    {
        if (PauseMenu.Instance.paused) return;
        if (movable)
        {
            rb2d.velocity = moveDir.normalized * speed;
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
                if (dir == 1) animator.SetTrigger("UpRight");
                else if (dir == 2) animator.SetTrigger("UpLeft");
                else if (dir == 3) animator.SetTrigger("DownLeft");
                else if (dir == 4) animator.SetTrigger("DownRight");

            }
        }
    }
}
