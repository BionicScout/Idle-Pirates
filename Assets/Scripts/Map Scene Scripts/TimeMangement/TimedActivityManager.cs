using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/*
        USES SYSTEM TIME
        EVENTALLY WANT TIME OFF INTERNET
 */
public class TimedActivityManager : MonoBehaviour {
    public List<TimeQuery> timeQueries;
    DateTime currentTime;
    public static TimedActivityManager instance;
    public MapShip mapShip;
    public DateTime lastTimeOn;
    public string lastCityStoppedAt;

    public List<TradeDeal> tradeDeals = new List<TradeDeal>();

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
        lastTimeOn = currentTime;
        lastCityStoppedAt = null;
        //Debug.Log("Start Time is " + currentTime.ToString("F"));
    }

    public TimeQuery getShipQuery() {
        for(int i = 0; i < timeQueries.Count; i++) {
            if(timeQueries[i].shipQuery && timeQueries[i].active)
                return timeQueries[i];
        }

        return null;
    }

    void Update() {
        currentTime = System.DateTime.Now;

        for(int i = 0; i < timeQueries.Count; i++) {

            //if(SceneSwitcher.currentScene == "Map Scene" && timeQueries[i].shipQuery)
            //    timeQueries[i].refreshNodes();

            //If Querry Finshed
            if(DateTime.Compare(timeQueries[i].finishTime, currentTime) <= 0) { //If the query time is done
                timeQueries[i].printLog();

            //Check for next qeury
                TimeQuery next = timeQueries[i].nextQuery;
                if(next != null) 
                    next.activate(System.DateTime.Now);

                //AudioManager.instance.Play("Moving");

                if(timeQueries[i].tradeQuery) {
                    timeQueries[i].addResourses();
                }

                //Check if Querry is Ship Queerry
                if(timeQueries[i].shipQuery) {
                    mapShip.timeQuery = next;

                    if(SceneSwitcher.currentScene == "Map Scene") {
                        //If Ship has a next location and is in map scene

                        mapShip.updateLocation(timeQueries[i].endNode); //#1

                        if(next != null) {
                            mapShip.setLocs();

                            AudioManager.instance.Play("Moving");
                        }
                    }
                    else {
                        lastCityStoppedAt = timeQueries[i].endName;
                    }
                }

            //Remove Querry From list
                timeQueries.RemoveAt(i);
                i--;
            }
        }

        for(int i = 0; i < tradeDeals.Count; i++) {
            tradeDeals[i].Update();

            if(tradeDeals[i].queries.Count == 0) {
                tradeDeals.RemoveAt(i);
                i--;
            }
        }

        if(lastCityStoppedAt != null && SceneSwitcher.currentScene == "Map Scene") {

            Node lastNode = FindObjectsOfType<Node>().ToList().Find(x => x.nodeName == lastCityStoppedAt);
            mapShip.updateLocation(lastNode); 
            lastCityStoppedAt = null;
        }
    }

    public void addQuery(TimeQuery query) {
        timeQueries.Add(query);
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

    public void Load(List<TimeQuery> shipQueries, string playerLastLoc) {
        TimeQuery activeShipQuery = null;

        foreach(TimeQuery q in shipQueries) {
            addQuery(q);

            if(q.active && q.shipQuery)
                activeShipQuery = q;
        }

        //if(activeShipQuery != null)
        //    MapShip.instance.timeQuery = activeShipQuery;
        //else {
        //    MapShip.instance.timeQuery = null;
        //    Set last ship loc
        //}
    }
}