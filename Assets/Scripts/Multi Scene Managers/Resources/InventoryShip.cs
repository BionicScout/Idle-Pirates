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
    //public InventoryShip(string name, int s, int a, int h, int mC, Sprite sI, List<Resource> r)
    //{

    //    shipName = name;
    //    speed = s;
    //    attack = a;
    //    health = h;
    //    maxCargo = mC;
    //    shipImage = sI;

    //    for (int i = 0; i < r.Count; i++)
    //    {
    //        resourcesNeeded[i].AddName(r[i].Name());
    //        resourcesNeeded[i].Add(r[i]);
    //    }


    //}



    public string GetShipName()
    {
        return shipName;
    }


}
