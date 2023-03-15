using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryShip {
    public enum USED_IN { combat, trading, none}

    [SerializeField]
    private string shipName;
    public int speed;
    public int attack;
    public int health;
    public int maxCargo;
    public Sprite shipImage;
    public USED_IN use = USED_IN.none; //Used to determine if the ship can be used in diffent tasks

    //Materials Needed;
    public List<Resource> resourcesNeeded;

    //Thought it should of had a constructor like resources script
    public InventoryShip(MainShips ships)
    {

        shipName = ships.shipName;
        speed = ships.speed;
        attack = ships.attack;
        health = ships.health;
        maxCargo = ships.maxCargo;
        shipImage = ships.shipImage;

        for (int i = 0; i < ships.resourcesNeeded.Count; i++)
        {
            //Debug.Log(i + " + " + ships.resourcesNeeded[i].GetName());
            //Debug.Log(i + " + " + ships.resourcesNeeded[i].GetAmount());
            //Debug.Log(i + " + " + ships.resourcesNeeded[i].GetCost());
            //Debug.Log(i + " + " + ships.resourcesNeeded[i].GetType());

            resourcesNeeded[i].AddName(ships.resourcesNeeded[i].GetName());
            resourcesNeeded[i].AddAmount(ships.resourcesNeeded[i].GetAmount());
            resourcesNeeded[i].AddCost(ships.resourcesNeeded[i].GetCost());
            resourcesNeeded[i].AddType(ships.resourcesNeeded[i].GetType());

            //resourcesNeeded[i].Add(ships.resourcesNeeded[i]);

            //Error: For loop stops at one iteration
        }


    }

    public InventoryShip(string name, int s, int a, int h, int mC, Sprite sI, List<Resource> r)
    {

        shipName = name;
        speed = s;
        attack = a;
        health = h;
        maxCargo = mC;
        shipImage = sI;

        for (int i = 0; i < r.Count; i++)
        {
            resourcesNeeded[i].AddName(r[i].GetName());
            resourcesNeeded[i].Add(r[i]);
        }


    }


    public void AddfromTemplate(MainShips ships)
    {
        shipName = ships.shipName;
        speed = ships.speed;
        attack = ships.attack;
        health = ships.health;
        maxCargo = ships.maxCargo;
        shipImage = ships.shipImage;

        for (int i = 0; i < ships.resourcesNeeded.Count; i++)
        {
            resourcesNeeded[i].AddName(ships.resourcesNeeded[i].GetName());
            resourcesNeeded[i].Add(ships.resourcesNeeded[i]);
        }


    }


    public string GetShipName()
    {
        return shipName;
    }


}
