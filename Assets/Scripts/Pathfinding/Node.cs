using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {
    public string nodeName;
    public PathfindingList neighboorNodes = new PathfindingList();
    public bool start, end;
    public bool find;

    public bool visted;
    public Node previous;

    void Start() {
        Pathfinding.add(this);
    }

    private void Update() {
        if(find) {
            find = false;
            Pathfinding.DijkstraSearch();
        }
    }

    public void addEdge(Edge edge) {
        if(nodeName != edge.node1.nodeName) {
            neighboorNodes.add(edge.distance, edge.node1);
            //Debug.Log(nodeName + " -> " + edge.node1);
        }
        else {
            neighboorNodes.add(edge.distance, edge.node2);
            //Debug.Log(nodeName + " -> " + edge.node2);
        }
    }

    public void resetSort() {
        visted = false;
        previous = null;
    }

    public float distanceFrom(Node other) {
        return neighboorNodes.getDistance(other);
    }
}
