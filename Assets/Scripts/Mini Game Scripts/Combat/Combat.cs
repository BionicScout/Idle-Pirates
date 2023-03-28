using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;

public enum GameState {
    PlayersTurn,
    PlayersAttack,
    //PlayersCheckSwitch,
    PlayerSwitch,
    PlayersRun,

    EnemysTurn,

    Win,
    Lose,
    Ran
}

public class Combat : MonoBehaviour {
    [Header("Combat Info")]
    public CombatFleet playerFleet;
    public CombatFleet enemyFleet;
    CombatShip playerShip, enemyShip;
    int playerShip_index, enemyShip_index;

    GameState gameState;

    List<CombatShip> possibleSwitches = new List<CombatShip>();

    [Header("Main UI")]
    [Range(0.1f, 10)]
    public float updateTextTime = 2f;
    public TMP_Text textUI;

    public GameObject attackButton, switchButton, runButton;
    string mapSceneName = "Map Scene";

    //Ship Display UI
    public GameObject playerShipUI, enemyShipUI;

    [Header("UI Windows")]
    public GameObject fleePopUp;
    public GameObject oneShip_swapPopUp;
    public GameObject twoShip_swapPopUp;

    private void Start() {
        int i = 0;
        foreach(InventoryShip ship in Inventory.instance.ships) {
            if(ship.use == InventoryShip.USED_IN.combat) {
                playerFleet.ships[i].setShip(ship.GetShipName());
                Debug.Log(playerFleet.ships[i].shipName);
                i++;
            }
        }



        Debug.Log(playerFleet.ships[0].shipName);

        playerShip = playerFleet.ships[SelectNewShip(playerFleet)];
        enemyShip = enemyFleet.ships[SelectNewShip(enemyFleet)];

        updateShipUI(playerShipUI, playerShip);
        updateShipUI(enemyShipUI, enemyShip);

        //UI.clickToMoveText = true;
        PlayersTurn();
    }

    int SelectNewShip(CombatFleet fleet) {
        for(int i = 0; i < fleet.ships.Count; i++) {
            if(!fleet.ships[i].dead) {
                return i;
            }
        }

        return -1;
    }

    /***************************************************************************************************************************************
            GAME END STATES
    ***************************************************************************************************************************************/
    IEnumerator Win() {
        gameState = GameState.Win;

        updateTextBox("Win!");
        yield return new WaitForSeconds(updateTextTime);

        SceneSwitcher.instance.A_LoadScene(mapSceneName);
    }

    IEnumerator Ran() {
        gameState = GameState.Ran;

        updateTextBox("Ran!");
        yield return new WaitForSeconds(updateTextTime);

        SceneSwitcher.instance.A_LoadScene(mapSceneName);
    }

    IEnumerator Lose() {
        gameState = GameState.Lose;

        updateTextBox("Lose!");
        yield return new WaitForSeconds(updateTextTime);

        SceneSwitcher.instance.A_LoadScene(mapSceneName);
    }

    /***************************************************************************************************************************************
            PLAYER TURN ACTIONS
    ***************************************************************************************************************************************/
    void PlayersTurn() {
        enableButton(attackButton);
        enableButton(runButton);

        updateTextBox("Player's Action");
        gameState = GameState.PlayersTurn;
    }

    /*
    This method allows the player to attack the enemy ship. The method does damage ot the enemy ship based off the player's ship's
    attack and then checks if the enemy ship was deystroyed. If the ship was deystroyed, the next enemy shipStock comes out. If all 
    shipStock were they deystroyed, the win State is called.
*/
    IEnumerator PlayerAttack() {
        disableButton(attackButton);
        disableButton(switchButton);
        disableButton(runButton);

        gameState = GameState.PlayersAttack;

        //Attack Enemy Ship
        enemyShip.removeHP(playerShip.attack);

        updateShipUI(enemyShipUI, enemyShip);
        updateTextBox(enemyShip.shipName + " HP: " + enemyShip.health + "/" + enemyShip.maxHealth);
        yield return new WaitForSeconds(updateTextTime);

        //If Enemy Ship was destroyed
        if(enemyShip.health <= 0) {
            enemyFleet.ships[enemyShip_index].dead = true;

            updateTextBox(enemyShip.shipName + " Destroyed");
            yield return new WaitForSeconds(updateTextTime);

            //If enemy fleet was deystroyed
            enemyShip_index = SelectNewShip(enemyFleet);
            if(enemyShip_index == -1) { //If no ship can be selected
                StartCoroutine(Win());
            }
            else {

                //Switch to other enemy ship
                enemyShip = enemyFleet.ships[enemyShip_index];

                updateShipUI(enemyShipUI, enemyShip);
                updateTextBox(enemyShip.shipName + " has come to fight");
                yield return new WaitForSeconds(updateTextTime);
            }
        }

        if(enemyShip_index != -1)
            StartCoroutine(EnemysTurn());

    }


    IEnumerator PlayerCheckSwitch() {
        //Get List of Ships to switched to 
        possibleSwitches = new List<CombatShip>();

        for(int i = 0; i < playerFleet.ships.Count; i++) {
            if(playerFleet.ships[i] == playerShip)
                continue;
            if(playerFleet.ships[i].dead)
                continue;

            possibleSwitches.Add(playerFleet.ships[i]);
        }

        //If there are no shipStock to switch to
        if(possibleSwitches.Count == 0) {
            disableButton(switchButton);

            updateShipUI(enemyShipUI, enemyShip);
            updateTextBox("No other shipStock to switched to");
            yield return new WaitForSeconds(updateTextTime);
        }
        

    }

    /*
        Switches shipStock 
        WORK IN PROGRESS
    */
    IEnumerator PlayerSwitch(int selectedShip) {
        disableButton(switchButton);

        playerFleet.ships[playerShip_index] = playerShip;
        playerShip = possibleSwitches[selectedShip];

        updateShipUI(playerShipUI, playerShip);
        updateTextBox("Switched to " + playerShip.shipName);
        yield return new WaitForSeconds(updateTextTime);

        for(int i = 0; i < playerFleet.ships.Count; i++) {
            if(playerFleet.ships[i] == playerShip) {
                playerShip_index = i;
                break;
            }
        }

        PlayersTurn();
    }

    /*
    This mechanic allows the use to run away in combat. The players esacpe chance is based on the horizontalSpeed of the player's ship \
    divided by the total horizontalSpeed of both shipStock currently out. If the 
    */
    IEnumerator PlayerRun() {
        int totalWeight = playerShip.speed + enemyShip.speed;
        float playerEscapeChance = (float)playerShip.speed / totalWeight;

        if(Random.value <= playerEscapeChance || Inventory.instance.crew.Find(x => x.active).crewName == "Hall") {
            StartCoroutine(Ran());
        }
        else {
            updateTextBox("Failed to Run");
            yield return new WaitForSeconds(updateTextTime);

            StartCoroutine(EnemysTurn());
        }
    }

    /***************************************************************************************************************************************
            ENEMY TURN ACTIONS
    ***************************************************************************************************************************************/
    IEnumerator EnemysTurn() {
    //State Enemy Attack/Action
        updateTextBox("Enemy Attack");
        yield return new WaitForSeconds(updateTextTime);

    //Enemy Attack
        playerShip.removeHP(enemyShip.attack);

        updateShipUI(playerShipUI, playerShip);
        updateTextBox(playerShip.shipName + " HP: " + playerShip.health + "/" + playerShip.maxHealth);
        yield return new WaitForSeconds(updateTextTime);

        //If Player ship was deystroyed
        if(playerShip.health <= 0) {
            playerFleet.ships[playerShip_index].dead = true;

            updateTextBox(playerShip.shipName + " Destroyed");
            yield return new WaitForSeconds(updateTextTime);

            playerShip_index = SelectNewShip(playerFleet);
            if(playerShip_index == -1) { //If no ship can be selected
                StartCoroutine(Lose());
            }
            else {
                playerShip = playerFleet.ships[playerShip_index];

                updateShipUI(playerShipUI, playerShip);
                updateTextBox(playerShip.shipName + " has come to fight");
                yield return new WaitForSeconds(updateTextTime);
            }
        }

        if(playerShip_index != -1) {
            enableButton(switchButton);
            PlayersTurn();
        }
    }

    /***************************************************************************************************************************************
            MAIN UI
    ***************************************************************************************************************************************/
    public void updateShipUI(GameObject shipUI, CombatShip ship) {
        //Update Header
        shipUI.transform.GetChild(0).GetComponent<TMP_Text>().text =
            "Name: " + ship.shipName + "\nHealth: " + ship.health + "/" + ship.maxHealth;

        //Update Health Bar
        //Update Image
        shipUI.transform.GetChild(2).GetComponent<Image>().sprite = ship.shipImage;
    }

    public void updateTextBox(string str) {
        textUI.text = str;
    }

    public void enableButton(GameObject button) {
        button.GetComponent<Graphic>().color = Color.white;
        button.GetComponent<Button>().enabled = true;
    }

    public void disableButton(GameObject button) {
        button.GetComponent<Graphic>().color = Color.grey;
        button.GetComponent<Button>().enabled = false;
    }

    /***************************************************************************************************************************************
            PLAYER TURN BUTTON'S
    ***************************************************************************************************************************************/

    public void OnAttackButtonPressed() {
        if(gameState != GameState.PlayersTurn) {
            return;
        }

        StartCoroutine(PlayerAttack());
    }

    public void OnSwapButtonPressed() {
        if(gameState != GameState.PlayersTurn) {
            return;
        }

        StartCoroutine(PlayerCheckSwitch());

        if(possibleSwitches.Count == 0)
            return;

        //Switch Art Imagies and Pull up display

        if(possibleSwitches.Count == 1) {
            oneShip_swapPopUp.transform.GetChild(2).GetComponent<Image>().sprite = possibleSwitches[0].shipImage;
            oneShip_swapPopUp.transform.GetChild(2).GetChild(0).GetComponent<TMP_Text>().text = possibleSwitches[0].shipName;

            oneShip_swapPopUp.SetActive(true);
        }
        else if(possibleSwitches.Count == 2) {
            twoShip_swapPopUp.transform.GetChild(2).GetComponent<Image>().sprite = possibleSwitches[0].shipImage;
            twoShip_swapPopUp.transform.GetChild(2).GetChild(0).GetComponent<TMP_Text>().text = possibleSwitches[0].shipName;

            twoShip_swapPopUp.transform.GetChild(3).GetComponent<Image>().sprite = possibleSwitches[1].shipImage;
            twoShip_swapPopUp.transform.GetChild(3).GetChild(0).GetComponent<TMP_Text>().text = possibleSwitches[1].shipName;

            twoShip_swapPopUp.SetActive(true);
        }
    }

    public void OnFleeButtonPressed() {
        if(gameState != GameState.PlayersTurn) {
            return;
        }

        fleePopUp.SetActive(true);
    }

    /***************************************************************************************************************************************
            WINDOWS UI
    ***************************************************************************************************************************************/
    public void Ship1_ButtonPressed() {
        StartCoroutine(PlayerSwitch(0));
        oneShip_swapPopUp.SetActive(false);
        twoShip_swapPopUp.SetActive(false);
    }

    public void Ship2_ButtonPressed() {
        StartCoroutine(PlayerSwitch(1));
        twoShip_swapPopUp.SetActive(false);
    }

    public void BackButtonPressed() {
        oneShip_swapPopUp.SetActive(false);
        twoShip_swapPopUp.SetActive(false);
    }

    public void YesFleeButtonPressed() {
        fleePopUp.SetActive(false);

        StartCoroutine(PlayerRun());
    }

    public void NoFleeButtonPressed() {
        fleePopUp.SetActive(false);
    }
}