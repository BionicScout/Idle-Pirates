using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edge : MonoBehaviour {
    public Node node1, node2;
    public float distance;

    void Start() {
        node1.addEdge(this);
        node2.addEdge(this);
    }
}
