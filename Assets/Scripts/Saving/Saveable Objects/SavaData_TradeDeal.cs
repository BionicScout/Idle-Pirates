using System;
using System.Collections.Generic;
using System.Net.Sockets;

[Serializable]
public class SavaData_TradeDeal {
    public SaveData_Resource gainedResource;
    public SaveData_Resource lostResource;
    public string shipInUse;
    public int[] queriesIndexs;
    public int totalTime;
    public float passedTime;

    public SavaData_TradeDeal(TradeDeal td) {
        gainedResource = new SaveData_Resource(td.gainedResource);
        lostResource = new SaveData_Resource(td.lostResource);
        shipInUse = td.shipInUse.GetShipName();

        queriesIndexs = new int[td.queries.Count];
        for(int i = 0; i < queriesIndexs.Length; i++) {
            queriesIndexs[i] = TimedActivityManager.instance.timeQueries.FindIndex(x => x == td.queries[i]);
        }

        totalTime = td.totalTime;
        passedTime = td.passedTime;
    }
}
