using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    private List<Node> nodes = new List<Node>();

    void Start()
    {
        for(int i=0; i<transform.childCount; i++) nodes.Add(transform.GetChild(i).GetComponent<Node>());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
