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
    
    private Vector2 moveDirection;
    private bool movable = false;

    private Vector2[] horizontals = {new Vector2(1, .57735f), new Vector2(1, -.57735f), new Vector2(-1, -.57735f), new Vector2(-1, .57735f)};
    public int currentLayer;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentLayer = 0;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            movable = true;
            moveDirection = GetMoveDirection().normalized;
        }
        else
        {
            movable = false;
        }

        int climingDir = Climing();
        if (climingDir != 0 && Input.GetMouseButtonDown(1))
        {
            print(climingDir);
            GoUp(climingDir);
        }
    }

    private void FixedUpdate()
    {
        if(movable)
        {
            rb2d.velocity = moveDirection * speed;
        }
    }

    private Vector2 GetMoveDirection()
    {
        Vector2 moveDir = Vector2.zero;
        //get mouse pointing direction
        Vector2 mouseDir = Input.mousePosition-Camera.main.WorldToScreenPoint(transform.position);

        //align to grids, avoid being too close
        if (mouseDir.magnitude > 30) 
        {
            if (mouseDir.x >= 0) moveDir.x = 1;
            else moveDir.x = -1;
            if (mouseDir.y >= 0) moveDir.y = .57735f;
            else moveDir.y = -.57735f;
        }

        spriteRenderer.flipX = moveDir.x > 0;
        return moveDir;
    }

    private int Climing() 
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, moveDirection, .4f);
        Debug.DrawLine(transform.position, (Vector2)transform.position + moveDirection, Color.blue);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.CompareTag("Handle"))
            {
                return hit.transform.position.y > transform.position.y ? 1 : -1;
            }
        }
        return 0;
    }

    private void GoUp(int up)
    {
        GameObject[] currentLayerGrounds = GameObject.FindGameObjectsWithTag(currentLayer.ToString());
        foreach (GameObject ground in currentLayerGrounds)
        {
            //change to wall
            ground.layer = 9;
        }

        currentLayer += up;

        GameObject[] currentLayerWalls = GameObject.FindGameObjectsWithTag(currentLayer.ToString());
        foreach (GameObject wall in currentLayerWalls)
        {
            //change to ground
            wall.layer = 10;
        }

        //animation 
        transform.position += Vector3.up * 2.1f * up;
        transform.position += (Vector3)moveDirection / 2;
    }
}