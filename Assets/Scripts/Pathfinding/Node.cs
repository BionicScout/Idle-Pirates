using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {
    public string nodeName;
    public SortedList<float, Node> neighboorNodes = new SortedList<float, Node>();
    public bool start, end;
    public bool find;

    bool visted;
    Node previous;

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
            neighboorNodes.Add(edge.distance, edge.node1);
            //Debug.Log(nodeName + " -> " + edge.node1);
        }
        else {
            neighboorNodes.Add(edge.distance, edge.node2);
            //Debug.Log(nodeName + " -> " + edge.node2);
        }
    }

    public void resetSort() {
        visted = false;
        previous = null;
    }
}
