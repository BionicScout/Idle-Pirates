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
    public TMP_Text ui;

    public Texture shipImage;

    void Start() {
        maxHealth = health;
    }

    void Update() {
        if (health <= 0)
        {
            health = 0;
            dead = true;
            //shipImage.SetActive(false);
        }
       
    }
}