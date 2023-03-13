using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;

[System.Serializable]
public class TimeQueryList_Saveable {
    public List<TimeQuery_Saveable> queries;
    public List<TimeQuery_Saveable> shipQueries;

    public TimeQueryList_Saveable(List<TimeQuery> qList) {
        queries = new List<TimeQuery_Saveable>();
        //shipQueries = new List<TimeQuery_Saveable>();

        //Save normal queries
        int i = 0;
        TimeQuery shipQuery = null;
        foreach(TimeQuery q in qList) {
            TimeQuery_Saveable saveableQuery = new TimeQuery_Saveable(q);
            queries.Add(saveableQuery);
            //if(q.shipQuery && q.active) {
            //    shipQuery = q;
            //}
            //else if(!q.shipQuery) {
            //    TimeQuery_Saveable saveableQuery = new TimeQuery_Saveable(q);
            //    queries.Add(saveableQuery);
            //    UnityEngine.Debug.Log(++i);
            //}
        }

    ////Save ship queires
        //TimeQuery temp = shipQuery;
        //i = 0;

        //while(temp != null) {
        //    TimeQuery_Saveable saveableQuery = new TimeQuery_Saveable(temp);
        //    shipQueries.Add(saveableQuery);
        //    UnityEngine.Debug.Log(++i);

        //    temp = temp.nextQuery;
        //}
    }

    public void load() {
    //Get List of all queries
        List<TimeQuery> timeQueries = new List<TimeQuery>();

        foreach(TimeQuery_Saveable q in queries) {
            TimeQuery timeQ = new TimeQuery(q);
            if(timeQ.active && DateTime.Compare(timeQ.startTime, System.DateTime.MinValue) != 0) {
                timeQ.activate(timeQ.startTime);
            }

            timeQueries.Add(timeQ); 
        }

    //Get all queries with refrences
        foreach(TimeQuery q in timeQueries) {                           //For every query
            if(q.nextQueryName != null) {                               //That has a next query
                for(int i = 0; i < timeQueries.Count; i++) {            //Look at each query
                    if(q.nextQueryName == timeQueries[i].queryName) {   //And if the query matches the nextQueryName
                        q.nextQuery = timeQueries[i];                   //Add as next query
                        break;
                    }
                }
            }
        }

        //Check for to complete all active nodes and find current ship spot
        string playerLastLoc = "City 16";

        for(int i = 0; i < timeQueries.Count; i++) {
            TimeQuery currentQuery = timeQueries[i];

            if(currentQuery.active == false)
                continue;

            playerLastLoc = currentQuery.endName;

            if(DateTime.Compare(currentQuery.finishTime, System.DateTime.Now) <= 0) { //If the query time is done
                                                                                      //Remove Current Query from list
                timeQueries.RemoveAt(i);
                i--;

                //UnityEngine.Debug.Log(currentQuery.queryName + " finished at : " + currentQuery.finishTime.ToString("F"));

                //If there is a next query, activate it and reset search to beggining
                if(currentQuery.nextQuery != null) {
                    currentQuery.nextQuery.activate(currentQuery.finishTime);
                    i = -1;
                }
            }
        }

        TimedActivityManager.instance.Load(timeQueries, playerLastLoc);
    }
}
