using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class CombatShip : MonoBehaviour {
    public string shipName;
    public int speed;
    public int attack;
    public int health, maxHealth;
    public bool dead;

    //Put this in CombatUI script
    public Sprite shipImage;

    void Start() {
        maxHealth = health;
    }

    public void setShip(string newShipName) {
        MainShips ship = Inventory.instance.shipTemplates.Find(x => x.shipName == newShipName);

        shipName = ship.shipName;
        speed = ship.speed;
        attack = ship.attack;
        maxHealth = ship.health;
        health = ship.health;

        shipImage = ship.shipImage;

        Debug.Log(shipName);
        Debug.Log(ship.shipName);
    }

    public void removeHP(int damage) {
        health -= damage;
        if(health <= 0) {
            health = 0;
            dead = true;
            //shipImage.SetActive(false);
        }
        Debug.Log(shipName);
    }
}