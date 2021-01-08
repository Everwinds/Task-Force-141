using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Range(1f,10f)]
    public float speed = 2f;

    private Rigidbody2D rb2d;
    private SpriteRenderer spriteRenderer;
    private Vector3 moveDir = Vector3.zero;
    private bool movable = false;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (!movable) movable = true;
            moveDir = Input.mousePosition-Camera.main.WorldToScreenPoint(transform.position);
            Debug.DrawLine(transform.position, transform.position + moveDir.normalized, Color.red, 0.1f);
        }
        else
        {
            if (movable) movable = false;
            moveDir = Vector3.zero;
        }
    }

    private void FixedUpdate()
    {
        if(movable)
        {
            rb2d.velocity = moveDir.normalized * speed;
            spriteRenderer.flipX = moveDir.x > 0;
        }
    }
}
