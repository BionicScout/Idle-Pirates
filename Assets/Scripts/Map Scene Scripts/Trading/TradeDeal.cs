using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class TradeDeal {
    public Resource gainedResource;
    public Resource lostResource;
    public InventoryShip shipInUse;
    public List<TimeQuery> queries;
    public int totalTime;
    public float passedTime;

    public TradeDeal(Resource buyResource, Resource costResource) {
        gainedResource = buyResource;
        lostResource = costResource;
    }

    public TradeDeal(Resource buyResource, Resource costResource, InventoryShip ship, List<TimeQuery> q) {
        gainedResource = buyResource;
        lostResource = costResource;
        shipInUse = ship;
        queries = q;
    }

    public TradeDeal(SavaData_TradeDeal td, DateTime lastSave) {
        gainedResource = new Resource(td.gainedResource);
        lostResource = new Resource(td.lostResource);
        shipInUse = Inventory.instance.ships.Find(x => x.GetShipName() == td.shipInUse && x.use == InventoryShip.USED_IN.trading);

        queries = new List<TimeQuery>();
        foreach(int i in td.queriesIndexs) {
            queries.Add(TimedActivityManager.instance.timeQueries[i]);
        }

        totalTime = td.totalTime;
        TimeSpan span = DateTime.Now - lastSave;
        passedTime = td.passedTime + (DateTime.Now - lastSave).Seconds;
    }

    public void activate() {
        totalTime = 0;
        foreach(TimeQuery query in queries) {
            totalTime += query.seconds;
        }

        queries[queries.Count - 1].activate(DateTime.Now);
        Inventory.instance.AddResource(lostResource);
        passedTime = 0;
    }

    public void Update() {
        for(int i = queries.Count - 1; i >= 0; i--) {
            if(queries[i].active && DateTime.Compare(queries[i].finishTime, DateTime.Now) <= 0) {
                queries.RemoveAt(i);
            }
        }

        if(queries.Count == 0) {
            Inventory.instance.AddResource(gainedResource);
            //Inventory.instance.AddResource(lostResource);

            shipInUse.use = InventoryShip.USED_IN.none;
        }

        passedTime += Time.deltaTime;
    }
}
