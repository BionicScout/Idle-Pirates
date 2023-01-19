using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using UnityEditor;
using UnityEngine;

/*
        USES SYSTEM TIME
        EVENTALLY WANT TIME OFF INTERNET
 */
public class TimedActivityManager : MonoBehaviour {
    public List<TimeQuery> timeQueries;
    DateTime currentTime;

    void Awake() {
        currentTime = System.DateTime.Now;
        Debug.Log("Start Time is " + currentTime.ToString("F"));
    }

    void Update() {
        currentTime = System.DateTime.Now;

        for(int i = 0; i < timeQueries.Count; i++) {
            if(DateTime.Compare(timeQueries[i].finishTime, currentTime) <= 0) { //If the query time is done
                timeQueries[i].triggered = true;
                timeQueries[i].printLog();

                timeQueries.RemoveAt(i);
                i--;
            }
        }
    }
}