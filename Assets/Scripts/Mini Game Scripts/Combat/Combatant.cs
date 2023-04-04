using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class Combatant : MonoBehaviour {
    public string combatantName;
    public int speed;
    public int attack;
    public int health, maxHealth;
    public bool dead;

    //Put this in CombatUI script
    public Sprite combatantImage;

    void Start() {
        maxHealth = health;
    }

    public void setCombatant(string newName, bool isShip) {
        if(isShip) {
            MainShips ship = Inventory.instance.shipTemplates.Find(x => x.shipName == newName);

            combatantName = ship.shipName;
            speed = ship.speed;
            attack = ship.attack;
            maxHealth = ship.health;
            health = ship.health;

            combatantImage = ship.shipImage;
        }
    }

    public void setCombatant(MainShips ship) {
        combatantName = ship.shipName;
        speed = ship.speed;
        attack = ship.attack;
        maxHealth = ship.health;
        health = ship.health;

        combatantImage = ship.shipImage;
    }

    public void removeHP(int damage) {
        health -= damage;
        if(health <= 0) {
            health = 0;
            dead = true;
            //shipImage.SetActive(false);
        }
    }
}