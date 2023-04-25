using System;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class TimeQuery {
    public string queryName = "Query #";
    public int minutes;
    public int seconds;
    public bool active;

    public bool shipQuery = false, tradeQuery = false;
    public Node startNode, endNode;
    public string startName, endName;

    public DateTime startTime;
    public DateTime finishTime;
    public TimeSpan timeInterval;

    public string nextQueryName;
    public TimeQuery nextQuery;

    public Resource gainedResource, lostResource;

    public TimeQuery(string name, int min, int sec, TimeQuery query, Node start, Node end) {
        queryName = name;
        minutes = min;
        seconds = sec;

        nextQuery = query;
        //shipQuery = true;
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

    public TimeQuery(SavaData_TimeQuery q) {
        queryName = q.queryName;
        minutes = q.seconds / 60;
        seconds = q.seconds % 60;
        active = q.active;

        shipQuery = q.shipQuery;
        tradeQuery = q.shipQuery;
        //ON LOAD OF MAP SCENE GET NODES
        startName = q.startName;
        endName = q.endName;

        startTime = DateTime.FromBinary(q.startTime);
        finishTime = DateTime.FromBinary(q.finishTime);
        timeInterval = finishTime - startTime;

        nextQueryName = q.nextQueryName;
        //ON LOAD OF ALL QUERYS, GET NEXT QUERY
    }

    public void updateResources(Resource r1, Resource r2) {
        gainedResource = r1;
        lostResource = r2;
    }

    public void addResourses() {
        if(gainedResource == null || lostResource == null)
            return;

        Inventory.instance.AddResource(gainedResource);
        Inventory.instance.AddResource(lostResource);
    }

    public void activate(DateTime startAt) {
        active = true;

        //TimedActivityManager.instance.addQuery(this);

        startTime = startAt;
        finishTime = startTime.AddMinutes(minutes);
        finishTime = finishTime.AddSeconds(seconds);

        timeInterval = finishTime - startTime;

        //Debug.Log(minutes + " " + seconds);
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
