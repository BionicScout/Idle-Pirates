using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MapShip : MonoBehaviour {
    public TimeQuery timeQuery;
    public float percentDone;

    public GameObject ship;
    Vector2 start, end;

    public Node currentLocation;

    public static MapShip instance;

    void Start() {
        instance = this;

        timeQuery = null;
        transform.position = currentLocation.gameObject.transform.position;

        currentLocation.start = true;
    }

    public void setLocs() {
        start = timeQuery.startNode.gameObject.transform.position;
        end = timeQuery.endNode.gameObject.transform.position;
    }

    public void updateLocation(Node node) {
        currentLocation.start = false;
        currentLocation = node;
        currentLocation.start = true;
    }

    void Update() {
        if(timeQuery == null)
            return;

        TimeSpan timeLeft_TimeSpan = timeQuery.finishTime - System.DateTime.Now;
        double timeLeft_seconds = timeLeft_TimeSpan.TotalSeconds;
        double totalTime = timeQuery.timeInterval.TotalSeconds;

        percentDone = 1 - (float)(timeLeft_seconds / totalTime);

        ship.transform.position = Vector2.Lerp(start, end, percentDone);
    }
}
