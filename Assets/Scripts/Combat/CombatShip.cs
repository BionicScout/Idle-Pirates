using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatShip : MonoBehaviour {
    public string shipName;
    public int speed;
    public int attack;
    public int health, maxHealth;
    public bool dead;

    void Start() {
        maxHealth = health;
    }
}