using System;

[Serializable]
public class SavaData_TimeQuery  {
    public string queryName;
    public int seconds;
    public bool active;

    public bool shipQuery = false, tradeQuery = false;
    public string startName, endName;

    public long startTime;
    public long finishTime;

    public string nextQueryName;

    SaveData_Resource gainedResource, lostResource;

    public SavaData_TimeQuery(TimeQuery q) {
        queryName = q.queryName;
        seconds = q.minutes * 60 + q.seconds;
        active = q.active;

        shipQuery = q.shipQuery;
        tradeQuery = q.shipQuery;
        startName = q.startName;
        endName = q.endName;

        startTime = q.startTime.ToBinary();
        finishTime = q.finishTime.ToBinary();

        nextQueryName = q.nextQueryName;

        gainedResource = new SaveData_Resource(q.gainedResource);
        lostResource = new SaveData_Resource(q.lostResource);
    }
}
