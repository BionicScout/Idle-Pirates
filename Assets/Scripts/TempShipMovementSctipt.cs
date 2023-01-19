using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempShipMovementSctipt : MonoBehaviour {
    public TimeQuery timeQuery;
    public float percentDone;

    public Vector2 point1, point2;
    public GameObject ship;

    void Start() {
        ship.transform.position = point1;
    }

    void Update() {
        if(!timeQuery.triggered) {
            TimeSpan timeLeft_TimeSpan = timeQuery.finishTime - System.DateTime.Now;
            double timeLeft_seconds = timeLeft_TimeSpan.TotalSeconds;
            double totalTime = timeQuery.timeInterval.TotalSeconds;

            percentDone = (float)(timeLeft_seconds / totalTime);


            ship.transform.position = Vector2.Lerp(point1, point2, 1 - percentDone);
        }
    }
}
