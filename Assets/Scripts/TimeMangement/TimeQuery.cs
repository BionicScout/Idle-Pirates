using System;
using Unity.VisualScripting;
using UnityEngine;

public class TimeQuery : MonoBehaviour {
    public string queryName = "Query #";
    public int minutes;
    public int seconds;
    public bool triggered;

    public DateTime startTime;
    public DateTime finishTime;
    public TimeSpan timeInterval; 

    public void startInfo(string name, int min, int sec) {
        queryName = name;
        minutes = min;
        seconds = sec;
    }

    void Start() {
        TimedActivityManager.instance.addQuery(this);

        startTime = System.DateTime.Now;
        finishTime = startTime.AddMinutes(minutes);
        finishTime = finishTime.AddSeconds(seconds);

        timeInterval = finishTime - startTime;

        Debug.Log(queryName + " will complete at " + finishTime.ToString("F"));
    }

    public void printLog() {
        Debug.Log(queryName + " was completed at " + finishTime.ToString("F"));
    }
}
