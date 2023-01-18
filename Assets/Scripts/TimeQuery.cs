using System;
using UnityEngine;

public class TimeQuery : MonoBehaviour {
    public string queryName = "Query #";
    public int minutes;
    public int seconds;
    public bool triggered;

    public DateTime finishTime;

    void Start() {
        finishTime = System.DateTime.Now;
        finishTime = finishTime.AddMinutes(minutes);
        finishTime = finishTime.AddSeconds(seconds);

        Debug.Log(queryName + " will complete at " + finishTime.ToString("F"));
    }

    public void printLog() {
        Debug.Log(queryName + " was completed at " + finishTime.ToString("F"));
    }
}
