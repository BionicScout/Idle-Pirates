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

    public Type type;
    public string resourceName;
    public int amount;

    public Resource(Type t, string name, int a) {
        type = t;
        resourceName = name;
        amount = a;
    }

    public string Name() {
        return resourceName;
    }

    public int GetAmount() {
        return amount;
    }

    public void Add(Resource newResource) {
        amount += newResource.GetAmount();
    }
}
