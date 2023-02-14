using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempShipMovementSctipt : MonoBehaviour {
    TimeQuery timeQuery;
    public float percentDone;

    public Vector2 start, end;
    public GameObject ship;

    public static TempShipMovementSctipt instance;

    void Start() {
        instance = this;
    }

    void Update() {
        if(timeQuery == null)
            return;

        if(!timeQuery.triggered) {
            TimeSpan timeLeft_TimeSpan = timeQuery.finishTime - System.DateTime.Now;
            double timeLeft_seconds = timeLeft_TimeSpan.TotalSeconds;
            double totalTime = timeQuery.timeInterval.TotalSeconds;

            percentDone = 1 - (float)(timeLeft_seconds / totalTime);


            ship.transform.position = Vector2.Lerp(start, end, percentDone);
        }
        if(timeQuery.triggered == true)
            timeQuery = timeQuery.nextQuery;
    }

    public void setQuery(TimeQuery query) {
        timeQuery = query;
    }
}
