using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CombatShip : MonoBehaviour {
    public string shipName;
    public int speed;
    public int attack;
    public int health, maxHealth;
    public bool dead;
    public TMP_Text ui;

    void Start() {
        maxHealth = health;
    }

    void Update() {
        if(health < 0)
            health = 0;

        ui.text = shipName + "\n" + health + "/" + maxHealth + "\nAttack " + attack;
    }
}