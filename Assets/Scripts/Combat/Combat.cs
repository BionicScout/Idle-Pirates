using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using TMPro;
using UnityEditor;
using UnityEditor.TextCore.Text;
using UnityEngine;

/*
    TO DO LIST:
    -Player Switching Clean Up
    -Enemy Turn
        -Enemy Attack
        -Enemy Switch (Maybe)
    -Comments
 */


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

    public TMP_Text shipTitle, text;

    void Start() {
        gameState = GameState.PlayersTurn;

        playerShip = playerFleet.ships[SelectNewShip(playerFleet)];
        enemyShip = enemyFleet.ships[SelectNewShip(enemyFleet)];
    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.Mouse0)) {
            updated = false;
            //Debug.Log("Pressed\nUpdated: "+updated);
        }
        if(updated == true) {
            //Debug.Log("A");
            return;
        }
        else if(gameState == GameState.Lose) {

        }
        else if(gameState == GameState.PlayersTurn) {
            shipTitle.text = playerShip.shipName;
            playersTurn();
        }
        else if(gameState == GameState.PlayersAttack) {
            shipTitle.text = "";
            playerAttack();
        }
        else if(gameState == GameState.PlayersSwitch) {
            shipTitle.text = "";
            playerSwitch();
        }
        else if(gameState == GameState.PlayersRun) {
            shipTitle.text = "";
            playerRun();
        }
        else if(gameState == GameState.EnemysTurn) {
            shipTitle.text = "Enemy";
            EnemysTurn();
        }
    }

//Player Turn Methods

    /*
        This method announces the actions the player can currently do and allows the user to input their desired action.
    */
    void playersTurn() {

    //Is player has switched ships, they can't switch again
        if(switched) {
            if(!updated) { //General Message
                text.text = "(A)ttack or (R)un";
            }

            if(Input.GetKeyDown(KeyCode.A)) { //Attacked
                gameState = GameState.PlayersAttack;
                updated = false;
                switched = false;
            }
            else if(Input.GetKeyDown(KeyCode.R)) { //Run
                gameState = GameState.PlayersRun;
                updated = false;
                switched = false;
            }
        }
    //They player hasn't switched ships yet
        else {
            if(!updated) {  //General Message
                text.text = "(A)ttack, (S)witch, or (R)un";
            }

            if(Input.GetKeyDown(KeyCode.A)) { //Attack
                gameState = GameState.PlayersAttack;
                updated = false;
            }
            else if(Input.GetKeyDown(KeyCode.S) && !switched) { //Switch
                gameState = GameState.PlayersSwitch;
                updated = false;
                switched = true;
            }
            else if(Input.GetKeyDown(KeyCode.R)) { //Run
                gameState = GameState.PlayersRun;
                updated = false;
            }
        }
    }

    /*
        This method allows the player to attack the enemy ship. The method does damage ot the enemy ship based off the player's ship's
        attack and then checks if the enemy ship was deystroyed. If the ship was deystroyed, the next enemy ships comes out. If all 
        ships were they deystroyed, the win State is called.
    */ 
    void playerAttack() {
        if(!updated) {
            updated = true;
        }

        enemyShip.health -= playerShip.attack;
        text.text = "Hit\n" + enemyShip.shipName + " HP: " + enemyShip.health + "/" + enemyShip.maxHealth;

        if(enemyShip.health <= 0) {
            text.text = text.text + "\n" + enemyShip.shipName + " Destroyed";
            enemyFleet.ships[enemyShip_index].dead = true;

            enemyShip_index = SelectNewShip(enemyFleet);
            if(enemyShip_index == -1) { //If no ship can be selected
                gameState = GameState.Win;
                text.text = text.text + "\n" + "Win";
                return;
            }

            enemyShip = enemyFleet.ships[enemyShip_index];
            text.text = text.text + "\n" + enemyShip.shipName + " has come to fight";
        }

        gameState = GameState.EnemysTurn;
    }


    /*
        Switches ships 
        WORK IN PROGRESS
    */
    void playerSwitch() {

        //Get List of Ships to switched to 
        List<CombatShip> possibleSwitches = new List<CombatShip>();

        for(int i = 0; i < playerFleet.ships.Count; i++) {
            if(playerFleet.ships[i] == playerShip)
                continue;
            if(playerFleet.ships[i].dead)
                continue;

            possibleSwitches.Add(playerFleet.ships[i]);  
        }

    //If there are no ships to switch to
        if(possibleSwitches.Count == 0) {
            text.text = "No other ships to switched to";
            gameState = GameState.PlayersTurn;

            return;
        }

        //If other ships to switch to

        if(!updated) {
            text.text = "(1) " + possibleSwitches[0].shipName;

            if(possibleSwitches.Count == 2) {
                text.text = text.text + "\n" + "(2) " + possibleSwitches[1].shipName;
            }
        }

        if(Input.GetKeyDown(KeyCode.Alpha1)) {
            playerFleet.ships[playerShip_index] = playerShip;
            playerShip = possibleSwitches[0];

            gameState = GameState.PlayersTurn;
            text.text = "Switched to " + playerShip.shipName;
            updated = true;

            for(int i = 0; i < playerFleet.ships.Count; i++) {
                if(playerFleet.ships[i] == playerShip) {
                    playerShip_index = i;
                    break;
                }                    
            }
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2) && possibleSwitches.Count == 2) {
            playerFleet.ships[playerShip_index] = playerShip;
            playerShip = possibleSwitches[1];

            gameState = GameState.PlayersTurn;
            text.text = "Switched to " + playerShip.shipName;
            updated = true;

            for(int i = 0; i < playerFleet.ships.Count; i++) {
                if(playerFleet.ships[i] == playerShip) {
                    playerShip_index = i;
                    break;
                }
            }
        }
    }


    /*
        This mechanic allows the use to run away in combat. The players esacpe chance is based on the speed of the player's ship \
        divided by the total speed of both ships currently out. If the 
    */
    void playerRun() {
        if(!updated) {
            updated = true;
        }

        int totalWeight = playerShip.speed + enemyShip.speed;
        float playerEscapeChance = (float)playerShip.speed / totalWeight;
        
        if(Random.value >= playerEscapeChance) {
            text.text = "Escaped";
            gameState = GameState.Ran;
        }
        else {
            text.text = "Failed Run";
            gameState = GameState.EnemysTurn;
        }
    }

//Enemy Turn Method
    void EnemysTurn() {
        if(!updated) {
            updated = true;
        }

         enemyAttack();


        gameState = GameState.PlayersTurn;
    }

    void enemyAttack() {
        text.text = "Enemy Attack";

        playerShip.health -= enemyShip.attack;
        text.text = text.text + "\n" + playerShip.shipName + " HP: " + playerShip.health + "/" + playerShip.maxHealth;

        if(playerShip.health <= 0) {
            text.text = text.text + "\n" + playerShip.shipName + " Destroyed";
            playerFleet.ships[playerShip_index].dead = true;

            playerShip_index = SelectNewShip(playerFleet);
            if(playerShip_index == -1) { //If no ship can be selected
                gameState = GameState.Lose;
                text.text = text.text + "\n" + "Lose";
                return;
            }

            playerShip = playerFleet.ships[playerShip_index];
            text.text = text.text + "\n" + playerShip.shipName + " has come to fight";
        }
    }
    void enemySwitch() {
        text.text = text.text + "\n" + "Enemy Switch";
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