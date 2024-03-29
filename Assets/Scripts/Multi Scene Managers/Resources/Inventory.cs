using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour 
{
    public int startingGoldAmount;
    public List<Resource> resources;
    public List<InventoryShip> ships;
    public List<InventoryCrew> crew;
    public static Inventory instance;

    public List<MainResources> tradeResourceTemplates;
    public List<MainResources> shipBuildResourceTemplates;
    public List<MainShips> shipTemplates;
    public List<MainCrewMembers> crewTemplates;

    public List<MainCrewMembers> crewTemplatesForShop;

    

    //Maybe have a reference to the index for Gold resource
    //public int goldIndexReference



    void Awake() {

        if (instance == null)
        {
            instance = this;
            startingGoldAmount = Inventory.instance.resources[0].amount;
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
        int index = FindResource(newResource.GetName());

        if(index >= 0) 
        { //If item exsits, combine the objects
            resources[index].AddNewResource(newResource);
            return;
        }

        resources.Add(newResource);
    }

    int FindResource(string name) {
        for(int i = 0; i < resources.Count; i++)
            if(resources[i].GetName() == name)
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
