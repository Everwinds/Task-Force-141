using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerSkills
{
    public static void HandleCliming(Vector2 moveDirection, ref int currentLayer, Transform transform)
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GoUp(moveDirection, ref currentLayer, transform);
        }
    }

    private static void GoUp(Vector2 moveDirection, ref int currentLayer, Transform transform)
    {
        int up = 0;
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, moveDirection, .4f);
        Debug.DrawLine(transform.position, (Vector2)transform.position + moveDirection * 2, Color.blue);
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

        GameObject[] currentLayerWalls = GameObject.FindGameObjectsWithTag(currentLayer.ToString());
        foreach (GameObject wall in currentLayerWalls)
        {
            wall.layer = 10;
        }

        currentLayer += up;

        GameObject[] currentLayerGrounds = GameObject.FindGameObjectsWithTag(currentLayer.ToString());
        foreach (GameObject ground in currentLayerGrounds)
        {
            ground.layer = 9;
        }

        //replace with animation
        transform.position += Vector3.up * 2.5f * up;
        transform.position += (Vector3)moveDirection / 2;
    }
}