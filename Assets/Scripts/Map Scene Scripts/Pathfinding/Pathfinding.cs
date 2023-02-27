using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Pathfinding : MonoBehaviour{
    public static List<Node> nodes = new List<Node>();
    static Node startNode, endNode;

    public static void add(Node node) {
        nodes.Add(node);

        if(node.start) {
            startNode = node;
        }
        if(node.end) {
            startNode = node;
        }
    }

    public static void clear() {
        nodes.Clear();
    }

    public static void refresh() {
        clear();

        //Object[] nodes = Resources.FindObjectsOfTypeAll(typeof(Node));
        Node[] nodes = FindObjectsOfType(typeof(Node)) as Node[];

        foreach(Node n in nodes) {
            add(n);
        }
    }

    /*
        https://en.wikipedia.org/wiki/Dijkstra%27s_algorithm
    */
    public static List<Node> DijkstraSearch() {

    //(1 and 2) Set all nodes to max value except start node
        PathfindingList unvisted = new PathfindingList();
        Node startNode = null;
        Node endNode = null;

        foreach(Node node in nodes) {
            if(node.start) {
                unvisted.add(0, node);
                startNode = node;
            }
            else {
                unvisted.add(60*100, node);
            }

            if(node.end)
                endNode = node;
        }

    //(3-6) Find Shortest Path
        bool foundPath = false;
        Node currentNode = startNode;
        PathfindingList toUpdatedList;
        startNode.previous = null;

        unvisted.printInfo();

        while(!foundPath) {
            toUpdatedList = new PathfindingList();

        //(3) For current closest node, look at all adjusted unvisted nodes and calulate distance
            foreach(Node neighboor in currentNode.neighboorNodes.getNodes()) {
            //Check if visted
                if(neighboor.visted)
                    continue;

            //Calculate Distance from current node
                float currentNodeDistance = unvisted.getDistance(currentNode);
                float distance = currentNodeDistance + currentNode.distanceFrom(neighboor);

            //Update neighboor's distance to smaller value from either current value or new value
                float neighboorCurrentDistance = unvisted.getDistance(neighboor);

                if(neighboorCurrentDistance > distance) {
                    neighboorCurrentDistance = distance;
                    neighboor.previous = currentNode;
                }

                toUpdatedList.add(distance, neighboor);
            }

        //(3) Update unvisted nodes
            for(int i = 0; i < toUpdatedList.count(); i++) {
                Node node = toUpdatedList.getNode(i);
                float distance = toUpdatedList.getDistance(i);

                unvisted.remove(node);
                unvisted.add(distance, node);
            }

        //(4) Remove Curretn Node from unvisted
            currentNode.visted = true;
            unvisted.remove(currentNode);

            Debug.Log("After Search");

        //(5) If end node has been visted then alitrithum is done
            if(endNode.visted) {
                foundPath = true;
                break;
            }

        //(6) select next node
            currentNode = unvisted.getFirst();
        }

        Debug.Log("Found");

    //Get Path
        Node tempNode = endNode;
        List<Node> path = new List<Node>();

        while(tempNode != null) {
            Debug.Log(tempNode.nodeName);
            path.Add(tempNode);
            tempNode = tempNode.previous;
        }

    //Reset
        foreach(Node node in nodes) {
            node.resetSort();
        }

        return path;
    }

}
