using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempShipMovementSctipt : MonoBehaviour {
    TimeQuery timeQuery;
    public string queryName;
    public int minutes, seconds;
    public float percentDone;

    public Vector2 start, end;
    public GameObject ship;

    void Start() {
        ship.transform.position = start;
        
        timeQuery = gameObject.AddComponent(typeof(TimeQuery)) as TimeQuery;
        timeQuery.startInfo(queryName, minutes, seconds);
    }

    void Update() {
        if(!timeQuery.triggered) {
            TimeSpan timeLeft_TimeSpan = timeQuery.finishTime - System.DateTime.Now;
            double timeLeft_seconds = timeLeft_TimeSpan.TotalSeconds;
            double totalTime = timeQuery.timeInterval.TotalSeconds;

            percentDone = 1 - (float)(timeLeft_seconds / totalTime);


            ship.transform.position = Vector2.Lerp(start, end, percentDone);
        }
    }
}
