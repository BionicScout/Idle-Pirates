using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryShip {
    public enum USED_IN { combat, trading, none}
    public enum COMBAT_TYPE { frigate, brig, schooner, normal }

    [SerializeField]
    private string shipName;
    public int speed;
    public int attack;
    public int health;
    public int maxCargo;
    public Sprite shipImage;
    public USED_IN use = USED_IN.none; //Used to determine if the ship can be used in diffent tasks
    public COMBAT_TYPE combatType = COMBAT_TYPE.normal;
    public List<Attacks> attacks = new List<Attacks>();

    //Materials Needed;
    public List<Resource> resourcesNeeded = new List<Resource>();

    //Thought it should of had a constructor like resourceStock script
    public InventoryShip(MainShips ships)
    {

        shipName = ships.shipName;
        speed = ships.speed;
        attack = ships.attack;
        health = ships.health;
        maxCargo = ships.maxCargo;
        shipImage = ships.shipImage;
        combatType = ships.combatType;

        for (int i = 0; i < ships.resourcesNeeded.Count; i++)
        {

            resourcesNeeded.Add(ships.resourcesNeeded[i]);


            Debug.Log(i + " + " + resourcesNeeded[i].GetName());
            Debug.Log(i + " + " + resourcesNeeded[i].GetAmount());
            Debug.Log(i + " + " + resourcesNeeded[i].GetCost());
            Debug.Log(i + " + " + resourcesNeeded[i].GetResourceType());



        }

    //Add Attacks
        foreach(Attacks a in ships.attacks) {

            Debug.Log(a.attackName);
            attacks.Add(a);
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
            resourcesNeeded[i].AddNewResource(r[i]);
        }


    }


    //public void AddfromTemplate(MainShips ships)
    //{
    //    shipName = ships.shipName;
    //    speed = ships.speed;
    //    attack = ships.attack;
    //    health = ships.health;
    //    maxCargo = ships.maxCargo;
    //    shipImage = ships.shipImage;

    //    for (int i = 0; i < ships.resourcesNeeded.Count; i++)
    //    {
    //        resourcesNeeded[i].AddName(ships.resourcesNeeded[i].GetName());
    //        resourcesNeeded[i].Add(ships.resourcesNeeded[i]);
    //    }


    //}


    public string GetShipName()
    {
        return shipName;
    }


}
