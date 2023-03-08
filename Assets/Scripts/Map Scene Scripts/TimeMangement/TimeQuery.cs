using System;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class TimeQuery {
    public string queryName = "Query #";
    public int minutes;
    public int seconds;
    public bool active;

    public bool shipQuery = false;
    public Node startNode, endNode;
    public string startName, endName;

    public DateTime startTime;
    public DateTime finishTime;
    public TimeSpan timeInterval;

    public string nextQueryName;
    public TimeQuery nextQuery;

    public TimeQuery(string name, int min, int sec, TimeQuery query, Node start, Node end) {
        queryName = name;
        minutes = min;
        seconds = sec;

        nextQuery = query;
        shipQuery = true;
        if(query == null)
            nextQueryName = null;
        else
            nextQueryName = nextQuery.queryName;

        startNode = start;
        startName = start.nodeName;

        endNode = end;
        endName = end.nodeName;

        startTime = DateTime.MinValue;
        finishTime = DateTime.MaxValue;
    }

    public TimeQuery(TimeQuery_Saveable saveQuery) {
        queryName = saveQuery.queryName;
        minutes = saveQuery.minutes;
        seconds = saveQuery.seconds;

        active = saveQuery.active;
        shipQuery = saveQuery.shipQuery;

        startTime = saveQuery.startTime;
        finishTime = saveQuery.finishTime;

        startName = saveQuery.startName;
        endName = saveQuery.endName;

        nextQueryName = saveQuery.nextQuery;
    }

    public void activate(DateTime startAt) {
        active = true;

        //TimedActivityManager.instance.addQuery(this);

        startTime = startAt;
        finishTime = startTime.AddMinutes(minutes);
        finishTime = finishTime.AddSeconds(seconds);

        timeInterval = finishTime - startTime;

        Debug.Log(minutes + " " + seconds);
        Debug.Log(queryName + " will complete at " + finishTime.ToString("F"));
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

    public void printLog() {
        Debug.Log(queryName + " was completed at " + finishTime.ToString("F"));
    }
}
