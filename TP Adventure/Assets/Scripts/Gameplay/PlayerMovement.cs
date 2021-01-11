using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    [Range(1f,10f)]
    public float speed = 2f;
    public int currentLayer = 0;

    public GameObject roll;
    TrailRenderer trail;
    Rigidbody2D rb2d;
    Animator animator;
    Vector3 moveDir = Vector3.zero;
    bool movable = false;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        trail = gameObject.GetComponentInChildren<TrailRenderer>();
    }

    private void Update()
    {
        if (GameStateManager.Instance.die) return;
        if (PauseMenu.Instance.paused)
        {
            animator.speed = 0;
            return;
        }
        
        HandleMovement();
        if (Input.GetKeyDown(KeyCode.Space))
            GoUp();

        if (movable)
        {
            animator.speed = 1;
            animator.SetFloat("X", moveDir.x);
            animator.SetFloat("Y", moveDir.y);
            animator.SetBool("Running", true);
        }
        else
        {
            animator.SetBool("Running", false);
        }
    }

    private void FixedUpdate()
    {
        if (PauseMenu.Instance.paused || GameStateManager.Instance.die) 
            return;
        if (movable)
            rb2d.velocity = moveDir * speed;
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
        }

        animator.SetFloat("X", moveDir.x);
        animator.SetFloat("Y", moveDir.y);
    }

    public void Jump()
    {
        animator.SetTrigger("Fly");
    }

    IEnumerator Climbing(int up)
    {
        animator.SetTrigger("Climb");
        trail.enabled = false;
        
        GameObject r = Instantiate(roll, transform.position, Quaternion.identity);
        r.GetComponent<RollUp>().Throw(up, moveDir);

        yield return new WaitForSeconds(.8f);

        for (int i = 0; i < 120; i++)
        {
            transform.position += Vector3.up * up * .02f;
            yield return new WaitForSeconds(.01f);  
        }
        for (int i = 0; i < 20; i++)
        {
            transform.Translate(moveDir * .02f);
            yield return new WaitForSeconds(.01f);  
        }

        GameObject[] currentLayerGrounds = GameObject.FindGameObjectsWithTag(currentLayer.ToString());
        foreach (GameObject ground in currentLayerGrounds)
            ground.layer = 9;

        trail.enabled = true;
        animator.SetTrigger("EndClimb");
    }

    private void GoUp()
    {
        int up = 0;
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, moveDir, .4f);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.CompareTag("Handle"))
            {
                up = hit.transform.position.y > transform.position.y ? 1 : -1;
                break;
            }
        }

        if (up == 0)
        return;

        StartCoroutine("Climbing", up);

        GameObject[] currentLayerWalls = GameObject.FindGameObjectsWithTag(currentLayer.ToString());
        foreach (GameObject wall in currentLayerWalls)
            wall.layer = 10;

        currentLayer += up;
        GetComponent<SpriteRenderer>().sortingOrder += up;
    }
}
