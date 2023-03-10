using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Resource {
    public enum Type {
        Gold,
        Ship_Build,
        Trade
    }

    [SerializeField]
    private Type type;

    [SerializeField]
    private string resourceName;

    [SerializeField]
    private int amount;

    [SerializeField]
    private int cost;

    public Resource(Type t, string name, int a, int c)
    {
        type = t;
        resourceName = name;
        amount = a;
        cost = c;
    }

    public string Name() {
        return resourceName;
    }

    public int GetAmount() {
        return amount;
    }

    public int GetCost()
    {
        return cost;
    }

    public void Add(Resource newResource) {
        amount += newResource.GetAmount();
    }

    public void Subtract(Resource newResource)
    {
        amount -= newResource.GetAmount();
    }
}
