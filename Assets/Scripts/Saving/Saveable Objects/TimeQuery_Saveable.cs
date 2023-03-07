using System;
using Unity.VisualScripting;


[System.Serializable]
public class TimeQuery_Saveable {

    public string queryName;
    public int minutes, seconds;
    public bool active, shipQuery;
    public DateTime startTime, finishTime;
    public string startName, endName;
    public string nextQuery;

    public TimeQuery_Saveable(TimeQuery q) {
    //Save Basic Data
        queryName = q.queryName;
        minutes = q.minutes;
        seconds = q.seconds;

        active = q.active;
        shipQuery = q.shipQuery;

    //Save DateTime vars
        startTime = q.startTime;
        UnityEngine.Debug.Log(queryName + ": " + startTime);
        finishTime = q.finishTime;

        //Start and End node names
        if(q.startName != null)
            UnityEngine.Debug.Log("NOT NULL");

        startName = q.startName;
        endName = q.endName;

    //Save nextQuery refrence as string
        if(q.nextQuery == null)
            nextQuery = null;
        else
            nextQuery = q.nextQuery.queryName;
    }
}
