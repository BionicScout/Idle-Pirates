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
            //Debug.Log("Start Node: " + node.nodeName);
        }
        if(node.end) {
            startNode = node;
            //Debug.Log("End Node: " + node.nodeName);
        }
    }

    /*
        https://en.wikipedia.org/wiki/Dijkstra%27s_algorithm
    */
    public static void DijkstraSearch() {

    //(1 and 2) Set all nodes to max value except start node
        PathfindingList unvisted = new PathfindingList();
        Node startNode = null;
        Node endNode = null;

        int processed = 0; //Use to prevent duplicate keys
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

        Debug.Log("(1+2)");
    //(3-6) Find Shortest Path
        bool foundPath = false;
        Node currentNode = startNode;
        PathfindingList toUpdatedList;
        startNode.previous = null;

        unvisted.printInfo();
        Debug.Log("Before Search");

        while(!foundPath) {
            toUpdatedList = new PathfindingList();

        //(3) For current closest node, look at all adjusted unvisted nodes and calulate distance
            int currentIndex = unvisted.getIndex(currentNode);
            //Debug.Log(currentNode.nodeName + ": " + unvisted.getDistance(currentNode));

            foreach(Node neighboor in currentNode.neighboorNodes.getNodes()) {
            //Check if visted
                if(neighboor.visted)
                    continue;

            //Calculate Distance from current node
                float currentNodeDistance = unvisted.getDistance(currentNode);
                float distance = currentNodeDistance + currentNode.distanceFrom(neighboor);
                //Debug.Log(currentNode.nodeName + ": " + currentNodeDistance + "\n" + neighboor.nodeName + ": " + distance);

            //Update neighboor's distance to smaller value from either current value or new value
                float neighboorCurrentDistance = unvisted.getDistance(neighboor);
                //Debug.Log("Distance: " + distance + "\nNeighboor Distance: " + neighboorCurrentDistance);
                if(neighboorCurrentDistance > distance) {
                    neighboorCurrentDistance = distance;
                    neighboor.previous = currentNode;
                    //Debug.Log(neighboor + "'s previous: " + currentNode.nodeName);
                }

                toUpdatedList.add(distance, neighboor);
                //Debug.Log(neighboor.nodeName + ": " + unvisted.getDistance(neighboor));
            }

        //(3) Update unvisted nodes
            for(int i = 0; i < toUpdatedList.count(); i++) {
                Node node = toUpdatedList.getNode(i);
                float distance = toUpdatedList.getDistance(i);

                unvisted.remove(node);
                unvisted.add(distance, node);
               //Debug.Log("Updated: " + node.nodeName + " = " + distance);
            }

        //(4) Remove Curretn Node from unvisted
            currentNode.visted = true;
            unvisted.remove(currentNode);

            //unvisted.printInfo();
            Debug.Log("After Search");

            //(5) If end node has been visted then alitrithum is done
            if(endNode.visted) {
                foundPath = true;
                break;
            }

        //(6) select next node
            currentNode = unvisted.getFirst();
            Debug.Log("Current Node: " + (currentNode.nodeName));

            Debug.Log("========================================================================================================");
        }

        Debug.Log("========================================================================================================");
        Debug.Log("Found");

        Node tempNode = endNode;
        List<Node> path = new List<Node>();

        while(tempNode != null) {
            Debug.Log(tempNode.nodeName);
            path.Add(tempNode);
            tempNode = tempNode.previous;
        }
    }

}
