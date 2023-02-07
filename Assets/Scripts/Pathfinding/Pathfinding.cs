using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pathfinding : MonoBehaviour{
    public static List<Node> nodes = new List<Node>();
    static Node startNode, endNode;

    public static void add(Node node) {
        nodes.Add(node);

        if(node.start) {
            startNode = node;
            Debug.Log("Start Node: " + node.nodeName);
        }
        if(node.end) {
            startNode = node;
            Debug.Log("End Node: " + node.nodeName);
        }
    }

    /*
        https://en.wikipedia.org/wiki/Dijkstra%27s_algorithm
    */
    public static void DijkstraSearch() {
        //SortedList<float, Node> openNodes = new SortedList<float, Node>(); //float is equal to distance from start
        //openNodes.Add(0, startNode);

        //bool foundEnd = false;

        //while(!foundEnd) {
        //    Node smallestNode = openNodes.Values[0];
        //    Node nextSmallestNode = 
        //}

    //(SET UP) Set all nodes to max value except start node
        SortedList<float, Node> unvisted = new SortedList<float, Node>();
        Node startNode = null;
        foreach(Node node in nodes) {
            if(node.start) {
                unvisted.Add(0, node);
                startNode = node;
            }
            else
                unvisted.Add(float.MaxValue, node);
        }

    //Find Shortest Path
        bool foundPath = false;
        Node currentNode = startNode;
        while(!foundPath) {
        //For current closest node, look at all adjusted unvisted nodes and calulate distance
            foreach(Node neighboor in currentNode.neighboorNodes.Values) {

            }

        }
    }

}
