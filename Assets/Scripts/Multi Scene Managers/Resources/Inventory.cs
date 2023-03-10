using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {
    public List<Resource> resources;
    public List<InventoryShip> ships;
    public List<InventoryCrew> crew;
    public static Inventory instance;

    void Awake() {

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

//Resource Managment
    public void AddResource(Resource newResource) {
        int index = FindResource(newResource.Name());

        if(index >= 0) 
        { //If item exsits, combine the objects
            resources[index].Add(newResource);
            return;
        }

        resources.Add(newResource);
    }

    int FindResource(string name) {
        for(int i = 0; i < resources.Count; i++)
            if(resources[i].Name() == name)
                return i;

        return -1;
    }

//Ship Managment
    public void AddShip(InventoryShip s) {
        ships.Add(s);
    }

//Crew Managment
    public void AddCrew(InventoryCrew c) {
        crew.Add(c);
    }


    //TESTING METHODS
    void Update() {

    }

    public void RestockResources()
    {


    }

    public void FindGoldIndexinInventory()
    {
        //Function to find gold in inventory

    }


}
