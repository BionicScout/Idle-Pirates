using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TradeDeal {
    public Resource gainedResource;
    public Resource lostResource;
    public InventoryShip shipInUse;
    public DateTime startTime;
    public DateTime finsihTime;
    public List<TimeQuery> queries;

    public TradeDeal(Resource buyResource, Resource costResource, InventoryShip ship, DateTime start, DateTime end) {
        gainedResource = buyResource;
        lostResource = costResource;
        shipInUse = ship;
        startTime = start;
        finsihTime = end;
    }
}
