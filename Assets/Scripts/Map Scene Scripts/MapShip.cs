using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MapShip : MonoBehaviour {
    public TimeQuery timeQuery;
    public float percentDone;
    public static bool done = true;

    public GameObject ship;
    public Vector2 start, end;

    public Node currentLocation;

    void Start() {
        timeQuery = null;
        transform.position = currentLocation.gameObject.transform.position;

        currentLocation.start = true;
    }

    public void setLocs() {
        if(timeQuery == null)
            return;

        timeQuery.refreshNodes();
        start = timeQuery.startNode.gameObject.transform.position; //#2
        end = timeQuery.endNode.gameObject.transform.position; //#3
    }

    public void updateLocation(Node node) {
        currentLocation.start = false;
        currentLocation = node;
        currentLocation.start = true;
    }

    void Update() {
        if(timeQuery == null) {
            done = true;
            return;
        }

        done = false;

        TimeSpan timeLeft_TimeSpan = timeQuery.finishTime - System.DateTime.Now;
        double timeLeft_seconds = timeLeft_TimeSpan.TotalSeconds;
        double totalTime = timeQuery.timeInterval.TotalSeconds;

        percentDone = 1 - (float)(timeLeft_seconds / totalTime);

        ship.transform.position = Vector2.Lerp(start, end, percentDone);
    }
}
