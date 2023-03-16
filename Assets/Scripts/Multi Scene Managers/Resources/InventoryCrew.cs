using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryCrew {
    public string crewName;
    public int speed;
    public int attack;
    public int health;
    public Sprite crewImage;
    public bool inUse; //Used in determining if the crewStock member is being used as fleet managers

    public bool active;

    [SerializeField]
    private int cost;

    //[SerializeField]
    //private int amount;

    public int GetCost()
    {
        return cost;
    }

    //public int GetAmount()
    //{
    //    return amount;
    //}

    //public void ReduceAmount(int cost) 
    //{
    //    amount -= cost;
    
    //}

    public InventoryCrew(MainCrewMembers crew)
    {

        crewName = crew.crewName;
        speed = crew.speed;
        attack = crew.attack;
        health = crew.health;
        crewImage = crew.crewImage;
        cost = crew.cost;
        

    }



}
