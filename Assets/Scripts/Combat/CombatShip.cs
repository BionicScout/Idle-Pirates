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

    //Put this in CombatUI script
    public TMP_Text ui;

    public GameObject shipImage;

    void Start() {
        maxHealth = health;
    }

    void Update() {
        if (health < 0)
        {
            health = 0;
            shipImage.SetActive(false);
        }
        //Put this into CombatUI Script
        
    }
}