using System;
using Unity.VisualScripting;
using UnityEditor.Search;

[System.Serializable]
public class TimeQuery_Saveable {

    public string queryName;
    public bool triggered, active, shipQuery;
    public DateTime startTime, finishTime;
    public string startName, endName;
    public string nextQuery;

    public TimeQuery_Saveable(TimeQuery q) {
    //Save Basic Data
        queryName = q.queryName;
        triggered = q.triggered;
        active = q.active;
        shipQuery = q.shipQuery;

    //Save DateTime vars
        startTime = q.startTime;
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
