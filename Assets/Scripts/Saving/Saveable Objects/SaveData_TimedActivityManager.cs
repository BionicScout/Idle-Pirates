using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

[Serializable]
public class SaveData_TimedActivityManager {
    public SavaData_TimeQuery[] timeQueries;
    public long lastTimeOn;
    public SavaData_TradeDeal[] tradeDeals;

    public SaveData_TimedActivityManager() {
        TimedActivityManager tam = TimedActivityManager.instance;

        timeQueries = new SavaData_TimeQuery[tam.timeQueries.Count];
        for(int i = 0; i < timeQueries.Length; i++) {
            timeQueries[i] = new SavaData_TimeQuery(tam.timeQueries[i]);
        }

        //lastTimeOn = tam.lastTimeOn.ToBinary();

        tradeDeals = new SavaData_TradeDeal[tam.tradeDeals.Count];
        for(int i = 0; i < tradeDeals.Length; i++) {
            tradeDeals[i] = new SavaData_TradeDeal(tam.tradeDeals[i]);
        }
    }

    public void Load(DateTime lastSave) {
        UnityEngine.Debug.Log("Loading TimeActivityManager");

        TimedActivityManager tam = TimedActivityManager.instance;

        //Get All queires plus load there next quieries 
        foreach(SavaData_TimeQuery q in timeQueries) {
            TimeQuery newQuery = new TimeQuery(q);
            tam.timeQueries.Add(newQuery);

        }
        for(int i = 0; i < tam.timeQueries.Count; i++) {
            if(tam.timeQueries[i].nextQueryName != null)
                tam.timeQueries[i].nextQuery = tam.timeQueries.Find(x => x.queryName == tam.timeQueries[i].nextQueryName);
        }

        //tam.lastTimeOn = DateTime.FromBinary(lastTimeOn);

        //Load trade Deals
        foreach(SavaData_TradeDeal td in tradeDeals) {
            //Load Deal
            TradeDeal deal = new TradeDeal(td, lastSave);
            tam.tradeDeals.Add(deal);

            //Check Trade Deal Queries for completion 
            for(int i = deal.queries.Count - 1; i >= 0; i--) {

                if(DateTime.Compare(deal.queries[i].finishTime, System.DateTime.Now) <= 0) { //If the query time is done
                    //deal.queries[i].printLog();

                    if(deal.queries[i].nextQuery != null)
                        deal.queries[i].nextQuery.activate(deal.queries[i].finishTime);

                    deal.queries.RemoveAt(i);
                }
                else
                    break;
            }
            if(deal.queries.Count == 0) {
                deal.shipInUse.use = InventoryShip.USED_IN.none;
                SaveStateManager.tradeInfo.Add(
                    "One of your trades were completed.\nYou gained " + deal.gainedResource.amount + " " + deal.gainedResource.GetName());

                UnityEngine.Debug.Log("ADDED INFO *********************************");
            }
        }

        foreach(TradeDeal deal in TimedActivityManager.instance.tradeDeals) {
            for(int i = deal.queries.Count - 1; i >= 0; i--) {
                if(DateTime.Compare(deal.queries[i].finishTime, System.DateTime.Now) <= 0) { //If the query time is done
                    TimedActivityManager.instance.timeQueries.Remove(deal.queries[i]);
                }
            }
        }




        //Check ALl queires for completion
        for(int i = 0; i < tam.timeQueries.Count; i++) {
            //If Querry Finshed
            if(DateTime.Compare(tam.timeQueries[i].finishTime, System.DateTime.Now) <= 0) { //If the query time is done
                tam.timeQueries[i].printLog();

                //Check if Querry is Ship Queerry
                if(tam.timeQueries[i].shipQuery) {
                    tam.lastCityStoppedAt = timeQueries[i].endName;
                }

                //Check for next qeuries for completion
                TimeQuery next = tam.timeQueries[i].nextQuery;
                if(next != null)
                    next.activate(tam.timeQueries[i].finishTime);

                while(next != null) {
                    UnityEngine.Debug.Log(next.queryName);

                    if(DateTime.Compare(next.finishTime, System.DateTime.Now) <= 0) { //If the query time is done
                        tam.timeQueries[i].printLog();
                        if(next.nextQuery != null) {
                            UnityEngine.Debug.Log(next.nextQuery.queryName + ": " + next.nextQuery.finishTime);
                            next.nextQuery.activate(next.finishTime);
                        }

                        //Check if Querry is Ship Queerry
                        if(next.shipQuery) {
                            tam.lastCityStoppedAt = next.endName;
                        }

                        int index = tam.timeQueries.FindIndex(x => x == next);
                        if(index != -1) {
                            if(index < i)
                                i--;

                            tam.timeQueries.RemoveAt(index);
                        }
                        next = next.nextQuery;
                    }
                    else {
                        next = null;
                    }
                }

                //Remove Querry From list
                tam.timeQueries.RemoveAt(i);
                i = i - 1; 
            }
        }
    }
}
