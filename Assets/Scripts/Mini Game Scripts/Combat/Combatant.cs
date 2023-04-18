using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Combatant : MonoBehaviour {
    public string combatantName;
    public int speed;
    public int attack;
    public int health, maxHealth;
    public bool dead;
    public InventoryShip.COMBAT_TYPE type;

    //Put this in CombatUI script
    public Sprite combatantImage;
    public List<Attacks> attacks = new List<Attacks>();

    void Start() {
        maxHealth = health;
    }

    public void setCombatant(string newName, bool isShip, List<Attacks> loadAttacks) {
        if(isShip) 
        {
            MainShips ship = Inventory.instance.shipTemplates.Find(x => x.shipName == newName);

            combatantName = ship.shipName;
            speed = ship.speed;
            attack = ship.attack;
            maxHealth = ship.health;
            health = ship.health;
            combatantImage = ship.shipImage;
            type = ship.combatType;

            //if Anthony is active
            if (Inventory.instance.crew.Find(x => x.active).crewName == "Anthony")
            {
                float speedBoostPercentage = .12f;
                float speedBoost = speed * speedBoostPercentage;
                int totalSpeed = speed + (int)speedBoost;

                speed = totalSpeed;
            }

            //if grey is active
            if (Inventory.instance.crew.Find(x => x.active).crewName == "Grey")
            {
                float attackBoostPercentage = .12f;
                float attackBoost = attack * attackBoostPercentage;
                int totalAttack = attack + (int)attackBoost;

                attack = totalAttack;
            }

            if (Inventory.instance.crew.Find(x => x.active).crewName == "Red Coat")
            {
                float redCoatBoostPercentage = .2f;

                float attackBoost = attack * redCoatBoostPercentage;
                int totalAttack = attack + (int)attackBoost;
                attack = totalAttack;


                float speedBoost = speed * redCoatBoostPercentage;
                int totalSpeed = speed + (int)speedBoost;
                speed = totalSpeed;


                float healthBoost = maxHealth * redCoatBoostPercentage;
                int totalHealth = maxHealth + (int)speedBoost;
                maxHealth = totalHealth;
                health = totalHealth;
            }

        }


        if(loadAttacks.Count <= 4)
            attacks = loadAttacks.ToList();
        else {
            List<Attacks> tempAttacks = loadAttacks.ToList();

            for(int i = 0; i < 4; i++) {
                int index = Random.Range(0, tempAttacks.Count - 1);

                Debug.Log("Count: " + tempAttacks.Count + "\tIndex: " + index);

                attacks.Add(tempAttacks[index]);
                tempAttacks.Remove(tempAttacks[index]);
            }
        }
    }

    public void setCombatant(MainShips ship) {
        combatantName = ship.shipName;
        speed = ship.speed;
        attack = ship.attack;
        maxHealth = ship.health;
        health = ship.health;

        combatantImage = ship.shipImage;
        type = ship.combatType;

        attacks = ship.attacks.ToList();
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