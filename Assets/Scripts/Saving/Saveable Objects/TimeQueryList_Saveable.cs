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
        shipQueries = new List<TimeQuery_Saveable>();

        //Save normal queries
        int i = 0;
        TimeQuery shipQuery = null;
        foreach(TimeQuery q in qList) {
            if(q.shipQuery && q.active) {
                shipQuery = q;
            }
            else if(!q.shipQuery) {
                TimeQuery_Saveable saveableQuery = new TimeQuery_Saveable(q);
                queries.Add(saveableQuery);
                UnityEngine.Debug.Log(++i);
            }
        }

    //Save ship queires
        TimeQuery temp = shipQuery;
        i = 0;

        while(temp != null) {
            TimeQuery_Saveable saveableQuery = new TimeQuery_Saveable(temp);
            shipQueries.Add(saveableQuery);
            UnityEngine.Debug.Log(++i);

            temp = temp.nextQuery;
        }
    }

    public void load() {
        foreach(TimeQuery_Saveable q in queries) {
                //TimeQuery_Saveable saveableQuery = new TimeQuery_Saveable(q);
                //queries.Add(saveableQuery);
        }

    //Load ship queies
        List<TimeQuery> loadShipQueries = new List<TimeQuery>();
        TimeQuery nextQuery = null;

        for(int i = shipQueries.Count - 1; i >= 0; i--) {
            TimeQuery temp = new TimeQuery(shipQueries[i], nextQuery);
            loadShipQueries.Add(temp);
            //TimedActivityManager.instance.addQuery(temp);
            nextQuery = temp;
        }

        TimedActivityManager.instance.Load(loadShipQueries);
    }
}
