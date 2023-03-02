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
    public static TimedActivityManager instance;

    void Awake() {
        if(instance == null)
            instance = this;
        else {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        timeQueries = new List<TimeQuery>();

        currentTime = System.DateTime.Now;
        //Debug.Log("Start Time is " + currentTime.ToString("F"));
    }

    public TimeQuery getShipQuery() {
        for(int i = 0; i < timeQueries.Count; i++) {
            if(timeQueries[i].shipQuery)
                return timeQueries[i];
        }

        return null;
    }

    void Update() {
        currentTime = System.DateTime.Now;

        for(int i = 0; i < timeQueries.Count; i++) {
            timeQueries[i].Update();

            //if(SceneSwitcher.currentScene == "Map Scene" && timeQueries[i].shipQuery)
            //    timeQueries[i].refreshNodes();

            //If Querry Finshed
            if(DateTime.Compare(timeQueries[i].finishTime, currentTime) <= 0) { //If the query time is done
                timeQueries[i].triggered = true;
                timeQueries[i].printLog();

            //Check for next qeury
                TimeQuery next = timeQueries[i].nextQuery;
                if(next != null) 
                    next.activate();

                //AudioManager.instance.Play("Moving");

                //Check if Querry is Ship Queerry
                if(timeQueries[i].shipQuery) {
                    MapShip.instance.timeQuery = next;

                    if(SceneSwitcher.currentScene == "Map Scene") {
                        //If Ship has a next location and is in map scene
                        
                        MapShip.instance.updateLocation(timeQueries[i].endNode); //#1

                        if(next != null) {
                            MapShip.instance.setLocs();

                            //AudioManager.instance.Play("Moving");
                        }
                    }
                }

            //Remove Querry From list
                timeQueries.RemoveAt(i);
                i--;
            }
        }
    }

    public void addQuery(TimeQuery query) {
        timeQueries.Add(query);
        //Debug.Log("Start Time is " + currentTime.ToString("F"));
        //query.activate();
    }

    public void refreshQuerries() {
        Pathfinding.refresh();

        foreach(TimeQuery q in timeQueries) {
            TimeQuery temp = q;
            while(temp != null) {
                temp.refreshNodes();
                //Debug.Log(temp.queryName + " has been refreshed");
                temp = temp.nextQuery;
            }
        }
    }

    public void Load(List<TimeQuery> shipQueries) {
        DateTime earliest = DateTime.MaxValue;
        foreach(TimeQuery q in shipQueries) {
            if(earliest.Ticks > q.finishTime.Ticks)
                earliest = q.finishTime;
        }

        TimeSpan passedTime = System.DateTime.Now - earliest;



    }
}