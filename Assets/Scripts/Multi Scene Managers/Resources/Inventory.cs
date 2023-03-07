using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {
    public List<Resource> resources;
    public List<InventoryShip> ships;
    public List<InventoryCrew> crew;
    public static Inventory instance;

    void Awake() {

        if(instance == null)
            instance = this;
        else {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

//Resource Managment
    public void addResource(Resource newResource) {
        int index = findResource(newResource.name());

        if(index >= 0) { //If item exsits, combine the objects
            resources[index].add(newResource);
            return;
        }

        resources.Add(newResource);
    }

    int findResource(string name) {
        for(int i = 0; i < resources.Count; i++)
            if(resources[i].name() == name)
                return i;

        return -1;
    }

//Ship Managment
    public void addShip(InventoryShip s) {
        ships.Add(s);
    }

//Crew Managment
    public void addShip(InventoryCrew c) {
        crew.Add(c);
    }


    //TESTING METHODS
    void Update() {

    }


}
