using System;
using Unity.VisualScripting;
using UnityEngine;

public class TimeQuery : MonoBehaviour {
    public string queryName = "Query #";
    public int minutes;
    public int seconds;
    public bool triggered, active;
    bool updated;

    bool shipQuery = false;
    public Node startNode, endNode;

    public DateTime startTime;
    public DateTime finishTime;
    public TimeSpan timeInterval;
    
    public TimeQuery nextQuery;

    public void startInfo(string name, int min, int sec) {
        queryName = name;
        minutes = min;
        seconds = sec;

        nextQuery = null;
    }

    public void startInfo(string name, int min, int sec, TimeQuery query) {
        queryName = name;
        minutes = min;
        seconds = sec;

        nextQuery = query;
    }

    public void isShipQuery(Node start, Node end) {
        shipQuery = true;
        startNode = start;
        endNode = end;
    }

    public void activate() {
        active = true;
    }

    void Update() {
        if(active && !updated) {
            TimedActivityManager.instance.addQuery(this);

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
