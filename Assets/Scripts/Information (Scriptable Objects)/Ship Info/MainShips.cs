using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static InventoryShip;

[CreateAssetMenu(fileName = "shipInfo", menuName = "Ship")]
public class MainShips : ScriptableObject
{
    public string shipName;
    public int speed;
    public int attack;
    public int health;
    public int maxCargo;
    public Sprite shipImage;

    public InventoryShip.USED_IN usedIn;
    public COMBAT_TYPE combatType;

    public List<Resource> resourcesNeeded;
    public List<Attacks> attacks;


    public void Delete()
    {
        shipName = "";
        speed = 0;
        attack = 0;
        health = 0;
        maxCargo = 0;
        

        resourcesNeeded = new List<Resource>();
    }
}
