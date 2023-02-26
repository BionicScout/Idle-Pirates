using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource {
    public enum Type {
        Gold,
        Ship_Build,
        Trade
    }

    Type type;
    string resourceName;
    int amount;

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
