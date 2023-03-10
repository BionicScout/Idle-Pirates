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
    public bool inUse; //Used in determining if the crew member is being used as fleet managers

    [SerializeField]
    private int cost;

    [SerializeField]
    private int amount;

    public int GetCost()
    {
        return cost;
    }

    public int GetAmount()
    {
        return amount;
    }

    public void ReduceAmount(int cost) 
    {
        amount -= cost;
    
    }
}
