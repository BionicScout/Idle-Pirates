using System;
using System.Collections.Generic;
using System.Net.Sockets;

[Serializable]
public class SavaData_TradeDeal {
    public SaveData_Resource gainedResource;
    public SaveData_Resource lostResource;
    public string shipInUse;
    public SavaData_TimeQuery[] queries;
    public int totalTime;
    public float passedTime;

    public SavaData_TradeDeal(TradeDeal td) {
        gainedResource = new SaveData_Resource(td.gainedResource);
        lostResource = new SaveData_Resource(td.lostResource);
        shipInUse = td.shipInUse.GetShipName();

        queries = new SavaData_TimeQuery[td.queries.Count];
        for(int i = 0; i < queries.Length; i++) {
            queries[i] = new SavaData_TimeQuery(td.queries[i]);
        }

        totalTime = td.totalTime;
        passedTime = td.passedTime;
    }
}
