using System;
using System.Collections.Generic;

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

        lastTimeOn = tam.lastTimeOn.ToBinary();

        tradeDeals = new SavaData_TradeDeal[tam.tradeDeals.Count];
        for(int i = 0; i < tradeDeals.Length; i++) {
            tradeDeals[i] = new SavaData_TradeDeal(tam.tradeDeals[i]);
        }
    }

    public void Load(DateTime lastSave) {
        TimedActivityManager tam = TimedActivityManager.instance;

        foreach(SavaData_TimeQuery q in timeQueries) {
            tam.timeQueries.Add(new TimeQuery(q));
        }

        tam.lastTimeOn = DateTime.FromBinary(lastTimeOn);

        foreach(SavaData_TradeDeal td in tradeDeals) {
            tam.tradeDeals.Add(new TradeDeal(td, lastSave));
        }
    }
}
