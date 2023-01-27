using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEditor;
using UnityEngine;

public class Combat : MonoBehaviour {
    public enum GameState {
        PlayersTurn,
        PlayersAttack,
        PlayersSwitch,
        PlayersRun,

        EnemysTurn,

        Win,
        Lose,
        Ran
    }

    public CombatFleet playerFleet, enemyFleet;
    CombatShip playerShip, enemyShip;
    int playerShip_index, enemyShip_index;

    public GameState gameState;
    bool updated, switched;

    void Start() {
        gameState = GameState.PlayersTurn;

        playerShip = playerFleet.ships[SelectNewShip(playerFleet)];
        enemyShip = playerFleet.ships[SelectNewShip(enemyFleet)];
    }

    void Update() {
        if(gameState == GameState.PlayersTurn) {
            playersTurn();
        }
        else if(gameState == GameState.PlayersAttack) {
            playerAttack();
        }
        else if(gameState == GameState.PlayersSwitch) {
            playerSwitch();
        }
        else if(gameState == GameState.PlayersRun) {
            playerRun();
        } else if(gameState == GameState.EnemysTurn) {
            EnemysTurn();
        }
    }

//Player Turn Methods
    void playersTurn() {

        if(switched) {
            if(!updated) {
                Debug.Log("(A)ttack or (R)un");
                updated = true;
            }

            if(Input.GetKeyDown(KeyCode.A)) {
                gameState = GameState.PlayersAttack;
                updated = false;
            }
            else if(Input.GetKeyDown(KeyCode.R)) {
                gameState = GameState.PlayersRun;
                updated = false;
            }
        }
        else {
            if(!updated) {
                Debug.Log("(A)ttack, (S)witch, or (R)un");
                updated = true;
            }

            if(Input.GetKeyDown(KeyCode.A)) {
                gameState = GameState.PlayersAttack;
                updated = false;
            }
            else if(Input.GetKeyDown(KeyCode.S) && !switched) {
                gameState = GameState.PlayersSwitch;
                updated = false;
            }
            else if(Input.GetKeyDown(KeyCode.R)) {
                gameState = GameState.PlayersRun;
                updated = false;
            }
        }
    }

    void playerAttack() {
        if(!updated) {
            Debug.Log("Attack");
            updated = true;
        }

        enemyShip.health -= playerShip.attack;
        Debug.Log("Hit\n" + enemyShip.shipName + " HP: " + enemyShip.health + "/" + enemyShip.maxHealth);

        if(enemyShip.health <= 0) {
            Debug.Log(enemyShip.shipName + " Destroyed");
            enemyFleet.ships[enemyShip_index].dead = true;

            enemyShip_index = SelectNewShip(enemyFleet);
            if(enemyShip_index == -1) { //If no ship can be selected
                gameState = GameState.Win;
                Debug.Log("Win");
                return;
            }

            enemyShip = enemyFleet.ships[enemyShip_index];
            Debug.Log(enemyShip.shipName + " has come to fight");
        }

        gameState = GameState.EnemysTurn;
        updated = false;
    }

    void playerSwitch() {
        if(!updated) {
            Debug.Log("Switch");
            updated = true;
        }



        gameState = GameState.PlayersTurn;
        updated = false;
        switched = true;
    }

    void playerRun() {
        if(!updated) {
            Debug.Log("Running...");
            updated = true;
        }

        int totalWeight = playerShip.speed + enemyShip.speed;
        float playerEscapeChance = (float)playerShip.speed / totalWeight;
        
        if(Random.value >= playerEscapeChance) {
            Debug.Log("Escaped");
            gameState = GameState.Ran;
        }
        else {
            Debug.Log("Failed Run");
            gameState = GameState.EnemysTurn;
            updated = false;
        }
    }

//Enemy Turn Method
    void EnemysTurn() {
        if(!updated) {
            Debug.Log("Enemy");
            updated = true;
        }

        gameState = GameState.PlayersTurn;
        updated = false;
        switched = false;
    }

//Other Functions
    int SelectNewShip(CombatFleet fleet) {
        for(int i = 0; i < fleet.ships.Count; i++) {
            if(!fleet.ships[i].dead) {
                return i;
            }
        }

        return -1;
    }
}