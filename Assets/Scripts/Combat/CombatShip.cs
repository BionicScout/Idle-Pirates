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

    public void removeHP(int damage) {
        health -= damage;
        if(health <= 0) {
            health = 0;
            dead = true;
            //shipImage.SetActive(false);
        }
    }

    void Update() {

    }
}