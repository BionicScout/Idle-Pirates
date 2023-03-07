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

    public string name() {
        return resourceName;
    }

    public int getAmount() {
        return amount;
    }

    public void add(Resource newResource) {
        amount += newResource.getAmount();
    }
}
