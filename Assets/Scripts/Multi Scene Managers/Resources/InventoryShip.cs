using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryShip {
    public enum USED_IN { combat, trading, none}

    public string shipName;
    public int speed;
    public int attack;
    public int health;
    public int maxCargo;
    public Sprite shipImage;
    public USED_IN use = USED_IN.none; //Used to determine if the ship can be used in diffent tasks
}
