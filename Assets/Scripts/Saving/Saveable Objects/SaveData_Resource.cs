using System;
using UnityEngine;

[Serializable]
public class SaveData_Resource {
    public int type;
    public string resourceName;
    public int amount;
    public int cost;

    public SaveData_Resource(Resource r) {
        switch(r.type) {
            case Resource.Type.Gold:
                type = 1;
                break;

            case Resource.Type.Ship_Build:
                type = 2;
                break;

            case Resource.Type.Trade:
                type = 3;
                break;

            default:
                break;
        }

        resourceName = r.GetName();
        amount = r.amount;
        cost = r.GetCost();
    }
}
