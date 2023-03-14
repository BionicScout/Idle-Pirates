using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using Unity.VisualScripting;
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
            Pathfinding.refresh();
            List<Node> path = Pathfinding.DijkstraSearch();

            TimeQuery query = null;
            GameObject queryManagerObj = TimedActivityManager.instance.GameObject();
            for(int i = 0; i < path.Count-1; i++) {
                Debug.Log("Here");
                TimeQuery newQuery = new TimeQuery("To " + path[i].nodeName, 0, (int)path[i].distanceFrom(path[i + 1]), query, path[i + 1], path[i]);
                TimedActivityManager.instance.addQuery(newQuery);
                query = newQuery;
            }

            //TimedActivityManager.instance.addQuery(query);
            query.activate(System.DateTime.Now);
            TimedActivityManager.instance.mapShip.timeQuery = query;
            TimedActivityManager.instance.mapShip.setLocs();
        }
    }

    public void addEdge(Edge edge) {
        if(nodeName != edge.node1.nodeName) {
            neighboorNodes.add(edge.distance, edge.node1);
        }
        else {
            neighboorNodes.add(edge.distance, edge.node2);
        }
    }

    public void resetSort() {
        end = false;
        visted = false;
        previous = null;
    }

    public float distanceFrom(Node other) {
        return neighboorNodes.getDistance(other);
    }
}
