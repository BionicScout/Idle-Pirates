using System;
using Unity.VisualScripting;
using UnityEditor.Search;
using UnityEngine;

[System.Serializable]
public class TimeQuery {
    public string queryName = "Query #";
    public int minutes;
    public int seconds;
    public bool triggered, active;
    bool updated;

    public bool shipQuery = false;
    public Node startNode, endNode;
    public string startName, endName;

    public DateTime startTime;
    public DateTime finishTime;
    public TimeSpan timeInterval;
    
    public TimeQuery nextQuery;

    public TimeQuery(string name, int min, int sec, TimeQuery query, Node start, Node end) {
        queryName = name;
        minutes = min;
        seconds = sec;

        nextQuery = query;
        shipQuery = true;

        startNode = start;
        startName = start.nodeName;

        endNode = end;
        endName = end.nodeName;

        finishTime = DateTime.MaxValue;
    }

    public TimeQuery(TimeQuery_Saveable saveQuery, TimeQuery q) {
        queryName = saveQuery.queryName;
        triggered = saveQuery.triggered;
        active = saveQuery.active;
        shipQuery = saveQuery.shipQuery;

        startTime = saveQuery.startTime;
        finishTime = saveQuery.finishTime;

        startName = saveQuery.startName;
        endName = saveQuery.endName;

        nextQuery = q;
    }

    public void activate() {
        active = true;
    }

    public void refreshNodes() {
        startNode = refreshNodes(startName);
        endNode = refreshNodes(endName);
    }

    public Node refreshNodes(string nodeName) {
        foreach(Node n in Pathfinding.nodes) {
            if(n.nodeName == nodeName) {
                return n;
            }

        }

        return null;
    }

    public void Update() {
        if(active && !updated) {
            //TimedActivityManager.instance.addQuery(this);

            startTime = System.DateTime.Now;
            finishTime = startTime.AddMinutes(minutes);
            finishTime = finishTime.AddSeconds(seconds);

            timeInterval = finishTime - startTime;

            Debug.Log(queryName + " will complete at " + finishTime.ToString("F"));

            updated = true;
        }
    }

    public void printLog() {
        Debug.Log(queryName + " was completed at " + finishTime.ToString("F"));
    }
}
