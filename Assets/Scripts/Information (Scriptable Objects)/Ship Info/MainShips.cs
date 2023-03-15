using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public List<Resource> resourcesNeeded;


}
