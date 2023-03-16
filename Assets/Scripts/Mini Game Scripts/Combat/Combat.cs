using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;


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

    public CombatUI UI;

    [SerializeField]
    private string mapSceneName;

    List<CombatShip> possibleSwitches = new List<CombatShip>();


    //[Header("Buttons")]
    //[SerializeField]
    //private GameObject attackButton;
    //[SerializeField]
    //private GameObject swapButton;
    //[SerializeField]
    //private GameObject fleeButton;

    [SerializeField]
    private GameObject fleePopUp;
    [SerializeField]
    private GameObject oneShip_swapPopUp;
    [SerializeField]
    private GameObject twoShip_swapPopUp;


    [Range(0, 15f)]
    public float textWaitSpeed = 3f;

    void Start() {
        loadScene();
    }

    public void loadScene() {
        int i = 0;
        foreach(InventoryShip ship in Inventory.instance.ships) {
            if(ship.use == InventoryShip.USED_IN.combat) {
                playerFleet.ships[i].setShip(ship.GetShipName());
                Debug.Log(ship.GetShipName());
                i++;
            }
        }

        gameState = GameState.PlayersTurn;

        playerShip = playerFleet.ships[SelectNewShip(playerFleet)];
        enemyShip = enemyFleet.ships[SelectNewShip(enemyFleet)];

        UI.clickToMoveText = true;
        PlayersTurn();
    }

    void Update() {
        if(!UI.getUpdate()) {
            return;
        }

        //End States
        else if(gameState == GameState.Win) {
            SceneSwitcher.instance.A_LoadScene(mapSceneName);
        }
        else if(gameState == GameState.Ran) {
            SceneSwitcher.instance.A_LoadScene(mapSceneName);
        }
        else if(gameState == GameState.Lose) {
            SceneSwitcher.instance.A_LoadScene(mapSceneName);
        }
    }

    //Game End States
    void Win() {
        UI.add("", "Win!");
        gameState = GameState.Win;
    }

    void Ran() {
        UI.add("", "Ran!");
        gameState = GameState.Ran;
    }

    void Lose() {
        UI.add("", "Lose!");
        gameState = GameState.Lose;
    }

    //Player Turn Methods

    /*
        This method sets the game state to PlayersTurn and updates UI.
    */
    void PlayersTurn() {
        UI.add(playerShip.shipName, "Player's Action");
        gameState = GameState.PlayersTurn;
        Debug.Log("Player Turn");
    }

    /*
        This method allows the player to attack the enemy ship. The method does damage ot the enemy ship based off the player's ship's
        attack and then checks if the enemy ship was deystroyed. If the ship was deystroyed, the next enemy ships comes out. If all 
        ships were they deystroyed, the win State is called.
    */
    void PlayerAttack() {
        gameState = GameState.PlayersAttack;

        //Attack Enemy Ship
        enemyShip.removeHP(playerShip.attack);

        UI.add("", enemyShip.shipName + " HP: " + enemyShip.health + "/" + enemyShip.maxHealth);
        UI.updateTextBox();
        Debug.Log("Player Attack");

        //If Enemy Ship was destroyed
        if(enemyShip.health <= 0) 
        {
            UI.add("", enemyShip.shipName + " Destroyed");
            //yield return new WaitForSeconds(textWaitSpeed);
            enemyFleet.ships[enemyShip_index].dead = true;

            //If enemy fleet was deystroyed
            enemyShip_index = SelectNewShip(enemyFleet);
            if(enemyShip_index == -1) { //If no ship can be selected
                Win();
                return;
            }

            //Switch to other enemy ship
            enemyShip = enemyFleet.ships[enemyShip_index];
            //enemyShip.shipImage.SetActive(true);
            UI.add("", enemyShip.shipName + " has come to fight");
            //yield return new WaitForSeconds(textWaitSpeed);
        }

        EnemysTurn();
    }

    void PlayerCheckSwitch() {
        //Get List of Ships to switched to 
        gameState = GameState.PlayersCheckSwitch;
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
            //yield return new WaitForSeconds(textWaitSpeed);
            PlayersTurn();
        }

        //If other ships to switch to
        gameState = GameState.PlayerSwitch;
    }

    /*
        Switches ships 
        WORK IN PROGRESS
    */
    void PlayerSwitch(int selectedShip) {
        playerFleet.ships[playerShip_index] = playerShip;
        playerShip = possibleSwitches[selectedShip];

        gameState = GameState.PlayersTurn;
        UI.add("", "Switched to " + playerShip.shipName);
        UI.updateTextBox();
        UI.clickToMoveText = true;
        UI.updateShipUI(UI.playerShipUI, playerShip);

        for(int i = 0; i < playerFleet.ships.Count; i++) {
            if(playerFleet.ships[i] == playerShip) {
                playerShip_index = i;
                break;
            }
        }
    }


    /*
        This mechanic allows the use to run away in combat. The players esacpe chance is based on the horizontalSpeed of the player's ship \
        divided by the total horizontalSpeed of both ships currently out. If the 
    */
    void PlayerRun() {
        int totalWeight = playerShip.speed + enemyShip.speed;
        float playerEscapeChance = (float)playerShip.speed / totalWeight;

        if(Random.value >= playerEscapeChance) {
            Ran();
            UI.updateTextBox();
            Debug.Log("Got Away");
            return;
        }

        UI.add("", "Failed Run");
        UI.updateTextBox();
        //yield return new WaitForSeconds(textWaitSpeed);
        EnemysTurn();

    }

    //Enemy Turn Method
    void EnemysTurn() {
        EnemyAttack();

        UI.updateShipUI(UI.playerShipUI, playerShip);
        UI.updateShipUI(UI.enemyShipUI, enemyShip);
    }

    void EnemyAttack() {
        UI.add(enemyShip.shipName + " (Enemy)", "Enemy Attack");

        playerShip.removeHP(enemyShip.attack);
        UI.add(enemyShip.shipName + " (Enemy)", playerShip.shipName + " HP: " + playerShip.health + "/" + playerShip.maxHealth);

        if(playerShip.health <= 0) {
            UI.add(enemyShip.shipName + " (Enemy)", playerShip.shipName + " Destroyed");
            //yield return new WaitForSeconds(textWaitSpeed);
            playerFleet.ships[playerShip_index].dead = true;


            playerShip_index = SelectNewShip(playerFleet);
            if(playerShip_index == -1) { //If no ship can be selected
                Lose();
                return;
            }

            playerShip = playerFleet.ships[playerShip_index];
            //playerShip.shipImage.SetActive(true);
            UI.add("", playerShip.shipName + " has come to fight");
            //yield return new WaitForSeconds(textWaitSpeed);
        }

        PlayersTurn();
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



    //Button Methods

//Attack Button
    public void OnAttackButtonPressed() {
        if(gameState != GameState.PlayersTurn || !UI.canUseButttons) {
            return;
        }

        UI.switched = false;
        PlayerAttack();
        UI.canUseButttons = false;

        //Attack
    }

//Run Button
    public void OnFleeButtonPressed() {
        if(gameState != GameState.PlayersTurn || !UI.canUseButttons) {
            return;
        }

        fleePopUp.SetActive(true);
        UI.canUseButttons = false;
        //Open flee window

    }

    public void YesFleeButtonPressed() {
        //SceneManager.LoadScene(mapSceneName);
        fleePopUp.SetActive(false);

        UI.switched = false;
        PlayerRun();
    }

    public void NoFleeButtonPressed() {
        fleePopUp.SetActive(false);
    }

//Swap Button
    public void OnSwapButtonPressed() {
        if(gameState != GameState.PlayersTurn || !UI.canUseButttons) {
            return;
        }

        PlayerCheckSwitch();

        if(possibleSwitches.Count == 0)
            return;

    //Switch Art Imagies and Pull up display

        if(possibleSwitches.Count == 1) {
            //Texture shipImage = possibleSwitches[0].shipImage;
            Debug.Log(oneShip_swapPopUp.transform.GetChild(2).name); //GetComponent<Image>();
            //image.image = shipImage;

            oneShip_swapPopUp.SetActive(true);
        }
        else if(possibleSwitches.Count == 2) {
            twoShip_swapPopUp.SetActive(true);
        }
    }

    public void Ship1_ButtonPressed() {
        UI.switched = true;
        PlayerSwitch(0);
        oneShip_swapPopUp.SetActive(false);
        twoShip_swapPopUp.SetActive(false);
    }

    public void Ship2_ButtonPressed() {
        UI.switched = true;
        PlayerSwitch(1);
        twoShip_swapPopUp.SetActive(false);
    }

    public void BackButtonPressed() {
        oneShip_swapPopUp.SetActive(false);
        twoShip_swapPopUp.SetActive(false);
    }
}