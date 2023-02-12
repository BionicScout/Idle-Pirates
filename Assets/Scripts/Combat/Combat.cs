using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


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
        PlayersCheckSwitch,
        PlayerSwitch,
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

    public CombatUI UI;

    [SerializeField]
    private string mapSceneName;

    List<CombatShip> possibleSwitches = new List<CombatShip>();
    bool startTic;

    void Start() {
        gameState = GameState.PlayersTurn;

        playerShip = playerFleet.ships[SelectNewShip(playerFleet)];
        enemyShip = enemyFleet.ships[SelectNewShip(enemyFleet)];

    //For UI
        startTic = true;

        UI.clickToMoveText = true;
        playersTurn();
    }

    void Update() {
        if(!UI.getUpdate()) {
            return;
        }

        //End States
        else if(gameState == GameState.Win) {
            Win();
        }
        else if(gameState == GameState.Ran) {
            Ran();
        }
        else if(gameState == GameState.Lose) {
            Lose();
        }

        //Game States
        else if(gameState == GameState.PlayersTurn) {
            playersTurn();
        }
        else if(gameState == GameState.PlayersAttack) {
            playerAttack();
        }
        else if(gameState == GameState.PlayersCheckSwitch) {
            playerCheckSwitch();
        }
        else if(gameState == GameState.PlayerSwitch) {
            playerSwitch();
        }
        else if(gameState == GameState.PlayersRun) {
            playerRun();
        }
        else if(gameState == GameState.EnemysTurn) {
            EnemysTurn();
        }

        if(Input.GetKeyDown(KeyCode.Escape)) {
            SceneManager.LoadScene(mapSceneName);
        }
        startTic = false;
    }

    //Game End States
    void Win() {
        UI.add("", "Win!");

        if(UI.getUpdate())
            UI.add("DEBUG ERROR", "Please add code to switch scenes and get rewards (if any)\nPress Esc to Go Back");
    }

    void Ran() {
        UI.add("", "Ran!");
        UI.updateTextBox();  //Run Bug
        UI.clickToMoveText = true;  //Run Bug


        if(UI.getUpdate())
            UI.add("DEBUG ERROR", "Please add code to switch scenes and get rewards (if any)\nPress Esc to Go Back");
    }

    void Lose() {
        UI.add("", "Lose!");

        if(UI.getUpdate())
            UI.add("DEBUG ERROR", "Please add code to switch scenes and get rewards (if any)\nPress Esc to Go Back");
    }

    //Player Turn Methods

    /*
        This method announces the actions the player can currently do and allows the user to input their desired action.
    */
    void playersTurn() {
        //Is player has switched ships, they can't switch again
        if(switched) {
        //UI
            if(UI.getUpdate() && UI.clickToMoveText) {
                UI.add(playerShip.shipName, "(A)ttack or (R)un");
                UI.clickToMoveText = false;
                Debug.Log("PLayer Turn");

                if(!startTic && Input.GetKeyDown(KeyCode.Mouse0)) {
                    UI.updateTextBox();
                }
            }

        //Player Action
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
            if(UI.getUpdate() && UI.clickToMoveText) { //General Message
                UI.add(playerShip.shipName, "(A)ttack, (S)witch, or (R)un");
                UI.clickToMoveText = false;
                Debug.Log("PLayer Turn");

                if(!startTic && Input.GetKeyDown(KeyCode.Mouse0)) {
                    UI.updateTextBox();
                }
            }

            if(Input.GetKeyDown(KeyCode.A)) { //Attack
                gameState = GameState.PlayersAttack;
                updated = false;
            }
            else if(Input.GetKeyDown(KeyCode.S) && !switched) { //Switch
                gameState = GameState.PlayersCheckSwitch;
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
    //Attack Enemy Ship
        enemyShip.health -= playerShip.attack;

        UI.add("", enemyShip.shipName + " HP: " + enemyShip.health + "/" + enemyShip.maxHealth);
        UI.updateTextBox();
        UI.clickToMoveText = true;
        Debug.Log("Player Attack");

        //If Enemy Ship was destroyed
        if(enemyShip.health <= 0) {
            UI.add("", enemyShip.shipName + " Destroyed");
            enemyFleet.ships[enemyShip_index].dead = true;
        
        //If enemy fleet was deystroyed
            enemyShip_index = SelectNewShip(enemyFleet);
            if(enemyShip_index == -1) { //If no ship can be selected
                gameState = GameState.Win;
                return;
            }

        //Switch to other enemy ship
            enemyShip = enemyFleet.ships[enemyShip_index];
            UI.add("", enemyShip.shipName + " has come to fight");
        }

        gameState = GameState.EnemysTurn;
    }

    void playerCheckSwitch() {
        //Get List of Ships to switched to 
        possibleSwitches = new List<CombatShip>();

        for(int i = 0; i < playerFleet.ships.Count; i++) {
            if(playerFleet.ships[i] == playerShip)
                continue;
            if(playerFleet.ships[i].dead)
                continue;

            possibleSwitches.Add(playerFleet.ships[i]);
        }

        //If there are no ships to switch to
        if(possibleSwitches.Count == 0) {
            UI.add("", "No other ships to switched to");
            UI.updateTextBox();
            UI.clickToMoveText = true;
            gameState = GameState.PlayersTurn;

            return;
        }

        //If other ships to switch to
        gameState = GameState.PlayerSwitch;
    }

    /*
        Switches ships 
        WORK IN PROGRESS
    */
    void playerSwitch() {
        if(UI.getUpdate()) { //General Message
            if(possibleSwitches.Count == 1)
                UI.add(" ", "(1) " + possibleSwitches[0].shipName);
            else if(possibleSwitches.Count == 2)
                UI.add(" ", "(1) " + possibleSwitches[0].shipName + "\n" + "(2) " + possibleSwitches[1].shipName);
;
            Debug.Log("Switch");

            UI.updateTextBox();
        }


        if(Input.GetKeyDown(KeyCode.Alpha1)) {
            shipSwitcher(1);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2) && possibleSwitches.Count == 2) {
            shipSwitcher(2);
        }
    }

    void shipSwitcher(int shipIndex) {
        playerFleet.ships[playerShip_index] = playerShip;
        playerShip = possibleSwitches[shipIndex];

        gameState = GameState.PlayersTurn;
        UI.add("", "Switched to " + playerShip.shipName);
        UI.updateTextBox();
        UI.clickToMoveText = true;

        for(int i = 0; i < playerFleet.ships.Count; i++) {
            if(playerFleet.ships[i] == playerShip) {
                playerShip_index = i;
                break;
            }
        }
    }


    /*
        This mechanic allows the use to run away in combat. The players esacpe chance is based on the speed of the player's ship \
        divided by the total speed of both ships currently out. If the 
    */
    void playerRun() {
        int totalWeight = playerShip.speed + enemyShip.speed;
        float playerEscapeChance = (float)playerShip.speed / totalWeight;

        if(Random.value >= playerEscapeChance) {
            gameState = GameState.Ran;
        }
        else {
            UI.add("", "Failed Run");
            UI.updateTextBox();
            UI.clickToMoveText = true;
            gameState = GameState.EnemysTurn;
        }
    }

    //Enemy Turn Method
    void EnemysTurn() {
        enemyAttack();
    }

    void enemyAttack() {
        UI.add(enemyShip.shipName + " (Enemy)", "Enemy Attack");

        playerShip.health -= enemyShip.attack;
        UI.add(enemyShip.shipName + " (Enemy)", playerShip.shipName + " HP: " + playerShip.health + "/" + playerShip.maxHealth);

        if(playerShip.health <= 0) {
            UI.add(enemyShip.shipName + " (Enemy)", playerShip.shipName + " Destroyed");
            playerFleet.ships[playerShip_index].dead = true;

            playerShip_index = SelectNewShip(playerFleet);
            if(playerShip_index == -1) { //If no ship can be selected
                gameState = GameState.Lose;
                return;
            }

            playerShip = playerFleet.ships[playerShip_index];
            UI.add("", playerShip.shipName + " has come to fight");
        }

        gameState = GameState.PlayersTurn;
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