using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {
    static List<Resource> resources;

    public void addResource(Resource newResource) {
        int index = findResource(newResource.name());

        if(index >= 0) { //If item exsits, combine the objects
            resources[index].add(newResource);
            return;
        }

        resources.Add(newResource);
    }

    public int findResource(string name) {
        for(int i = 0; i < resources.Count; i++)
            if(resources[i].name() == name)
                return i;

        return -1;
    }

    
}
