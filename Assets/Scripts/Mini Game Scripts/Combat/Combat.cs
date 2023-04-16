using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using System.Runtime.InteropServices.WindowsRuntime;

public enum GameState {
    PlayersTurn,
    EnemysTurn,

    Win,
    Lose,
    Ran
}

public class Combat : MonoBehaviour {
    [Header("Combat Info")]
    public CombatGroup playerGroup;
    public CombatGroup enemyGroup;
    Combatant playerCombatant, enemyCombatant;
    int playerCombatant_index, enemyCombatant_index;

    GameState gameState;

    List<Combatant> possibleSwitches = new List<Combatant>();
    [Range(0f, 1f)]
    public float dodgeChance = 0.1f;

    [Header("Main UI")]
    [Range(0.1f, 10)]
    public float updateTextTime = 2f;
    public TMP_Text textUI;

    public GameObject attackButton, switchButton, runButton;
    string mapSceneName = "Map Scene";

    //Ship Display UI
    public GameObject playerUI, enemyUI;

    [Header("UI Windows")]
    public GameObject fleePopUp;
    public GameObject oneCombatant_swapPopUp;
    public GameObject twoCombatant_swapPopUp;
    public GameObject attackPanel;

    private void Start() {
        int i = 0;
        foreach(InventoryShip ship in Inventory.instance.ships) {
            if(ship.use == InventoryShip.USED_IN.combat) {
                playerGroup.ships[i].setCombatant(ship.GetShipName(), true, ship.attacks);
                Debug.Log(playerGroup.ships[i].combatantName);
                i++;
            }
        }

        for(i = 0; i < 3; i++) {
            int rand = Mathf.FloorToInt(Random.Range(0, Inventory.instance.shipTemplates.Count - 0.0000000001f));
            Debug.Log(rand);
            enemyGroup.ships[i].setCombatant(Inventory.instance.shipTemplates[rand]);
        }



        playerCombatant = playerGroup.ships[SelectNewCombatant(playerGroup)];
        enemyCombatant = enemyGroup.ships[SelectNewCombatant(enemyGroup)];

        updateCombatantUI(playerUI, playerCombatant, playerGroup);
        updateCombatantUI(enemyUI, enemyCombatant, enemyGroup);

        //UI.clickToMoveText = true;
        PlayersTurn();
    }

    /***************************************************************************************************************************************
            OTHER METHODS
    ***************************************************************************************************************************************/

    int SelectNewCombatant(CombatGroup fleet) {
        for(int i = 0; i < fleet.ships.Count; i++) {
            if(!fleet.ships[i].dead) {
                return i;
            }
        }

        return -1;
    }

    public int randomizeDamage(int baseDamage) {
        int adjust = Mathf.CeilToInt(baseDamage * 0.1f);
        return Random.Range(baseDamage - adjust, baseDamage + adjust) + Random.Range(-1, 1);
    }

    public int damageTypeEffect(int damage, InventoryShip.COMBAT_TYPE shipType, Attacks.TYPE attType) {
        float percent = 1;

        if(shipType == InventoryShip.COMBAT_TYPE.frigate) {
            if(attType == Attacks.TYPE.CREW) { //Effective
                percent += .25f;
            }
            else if(attType == Attacks.TYPE.HULL) { //Not Effective
                percent -= .25f;
            }
        }
        else if(shipType == InventoryShip.COMBAT_TYPE.brig) {
            if(attType == Attacks.TYPE.SAIL) { //Effective
                percent += .25f;
            }
            else if(attType == Attacks.TYPE.CREW) { //Not Effective
                percent -= .25f;
            }
        }
        else if(shipType == InventoryShip.COMBAT_TYPE.schooner) {
            if(attType == Attacks.TYPE.HULL) { //Effective
                percent += .25f;
            }
            else if(attType == Attacks.TYPE.SAIL) { //Not Effective
                percent -= .25f;
            }
        }

        return Mathf.RoundToInt(damage * percent);
    }

    /***************************************************************************************************************************************
            GAME END STATES
    ***************************************************************************************************************************************/
    IEnumerator Win() {
        gameState = GameState.Win;

        updateTextBox("Win!");
        yield return new WaitForSeconds(updateTextTime);

        AudioManager.instance.Play("Combat Win");

        SceneSwitcher.instance.A_LoadScene(mapSceneName);
    }

    IEnumerator Ran() {
        gameState = GameState.Ran;

        updateTextBox("Ran!");
        yield return new WaitForSeconds(updateTextTime);

        AudioManager.instance.Play("Combat Ran");

        SceneSwitcher.instance.A_LoadScene(mapSceneName);
    }

    IEnumerator Lose() {
        gameState = GameState.Lose;

        updateTextBox("Lose!");
        yield return new WaitForSeconds(updateTextTime);

        AudioManager.instance.Play("Combat Lose");

        SceneSwitcher.instance.A_LoadScene(mapSceneName);
    }

    /***************************************************************************************************************************************
            PLAYER TURN ACTIONS
    ***************************************************************************************************************************************/
    void PlayersTurn() {
        updateTextBox("Player's Action");
        gameState = GameState.PlayersTurn;

        AudioManager.instance.Play("Combat Player's Turn");
    }

    /*
    This method allows the player to attack the enemy ship. The method does damage ot the enemy ship based off the player's ship's
    attack and then checks if the enemy ship was deystroyed. If the ship was deystroyed, the next enemy shipStock comes out. If all 
    shipStock were they deystroyed, the win State is called.
*/
    IEnumerator PlayerAttack(string attackName) 
    {
        disableButton(attackButton);
        disableButton(switchButton);
        disableButton(runButton);

        //Dodge
        if(Random.value < dodgeChance) {
            updateTextBox(enemyCombatant.combatantName + " dodge the attack");
            yield return new WaitForSeconds(updateTextTime);
        }
        //Attack
        else 
        {
            //Get Attack
            Attacks attack = playerCombatant.attacks.Find(x => x.attackName == attackName);

            //Attack Enemy Ship
            int damage = randomizeDamage(attack.baseDamage);
            int effectDamage = damageTypeEffect(damage, enemyCombatant.type, attack.type);
            enemyCombatant.removeHP(effectDamage);

            string extraStr = "";
            if(damage < effectDamage)
                extraStr = "\nIt was super effective";
            else if(damage > effectDamage)
                extraStr = "\nIt was not effective";

            AudioManager.instance.Play("Combat Attack");

            updateCombatantUI(enemyUI, enemyCombatant, enemyGroup);
            updateTextBox(playerCombatant.combatantName + " delt " + effectDamage + " " + attack.type.ToString() + " damage to " + enemyCombatant.combatantName + extraStr);
            yield return new WaitForSeconds(updateTextTime);

            updateTextBox(enemyCombatant.combatantName + " HP: " + enemyCombatant.health + "/" + enemyCombatant.maxHealth);
            yield return new WaitForSeconds(updateTextTime);

            //If Enemy Ship was destroyed
            if(enemyCombatant.health <= 0) {
                enemyGroup.ships[enemyCombatant_index].dead = true;

                AudioManager.instance.Play("Combat Sink");

                updateTextBox(enemyCombatant.combatantName + " Destroyed");
                yield return new WaitForSeconds(updateTextTime);

                //If enemy fleet was deystroyed
                enemyCombatant_index = SelectNewCombatant(enemyGroup);
                if(enemyCombatant_index == -1) { //If no ship can be selected
                    StartCoroutine(Win());
                }
                else {

                    //Switch to other enemy ship
                    enemyCombatant = enemyGroup.ships[enemyCombatant_index];

                    AudioManager.instance.Play("Combat Swapped Ships");

                    updateCombatantUI(enemyUI, enemyCombatant, enemyGroup);
                    updateTextBox(enemyCombatant.combatantName + " has come to fight");
                    yield return new WaitForSeconds(updateTextTime);
                }
            }
        }

        if(enemyCombatant_index != -1)
            StartCoroutine(EnemysTurn());

    }


    IEnumerator PlayerCheckSwitch() {
        //Get List of Ships to switched to 
        possibleSwitches = new List<Combatant>();

        for(int i = 0; i < playerGroup.ships.Count; i++) {
            if(playerGroup.ships[i] == playerCombatant)
                continue;
            if(playerGroup.ships[i].dead)
                continue;

            possibleSwitches.Add(playerGroup.ships[i]);
        }

        //If there are no shipStock to switch to
        if(possibleSwitches.Count == 0) {
            disableButton(switchButton);

            AudioManager.instance.Play("Error");

            updateCombatantUI(enemyUI, enemyCombatant, enemyGroup);
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

        playerGroup.ships[playerCombatant_index] = playerCombatant;
        playerCombatant = possibleSwitches[selectedShip];

        AudioManager.instance.Play("Combat Swapped Ships");

        updateCombatantUI(playerUI, playerCombatant, playerGroup);
        updateTextBox("Switched to " + playerCombatant.combatantName);
        yield return new WaitForSeconds(updateTextTime);

        for(int i = 0; i < playerGroup.ships.Count; i++) {
            if(playerGroup.ships[i] == playerCombatant) {
                playerCombatant_index = i;
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
        int totalWeight = playerCombatant.speed + enemyCombatant.speed;
        float playerEscapeChance = (float)playerCombatant.speed / totalWeight;

        if(Random.value <= playerEscapeChance 
            || Inventory.instance.crew.Find(x => x.active).crewName == "Hall") 
        {
            StartCoroutine(Ran());
        }
        else 
        {
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

        //Dodge
        if(Random.value < dodgeChance) {
            updateTextBox(enemyCombatant.combatantName + " dodge the attack");
            yield return new WaitForSeconds(updateTextTime);
        }
        else {
            //Get Attack
            Attacks attack = enemyCombatant.attacks[Random.Range(0, enemyCombatant.attacks.Count)];

            //Enemy Attack
            int damage = randomizeDamage(attack.baseDamage);
            int effectDamage = damageTypeEffect(damage, playerCombatant.type, attack.type);
            playerCombatant.removeHP(effectDamage);

            AudioManager.instance.Play("Combat Attack");

            string extraStr = "";
            if(damage < effectDamage)
                extraStr = "\nIt was super effective";
            else if(damage > effectDamage)
                extraStr = "\nIt was not effective";

            updateCombatantUI(playerUI, playerCombatant, playerGroup);
            updateTextBox(enemyCombatant.combatantName + " delt " + effectDamage + " " + attack.type.ToString() + " damage to " + playerCombatant.combatantName + extraStr);
            yield return new WaitForSeconds(updateTextTime);

            updateCombatantUI(playerUI, playerCombatant, playerGroup);
            yield return new WaitForSeconds(updateTextTime);

            //If Player ship was deystroyed
            if(playerCombatant.health <= 0) {
                playerGroup.ships[playerCombatant_index].dead = true;

                AudioManager.instance.Play("Combat Sink");

                updateTextBox(playerCombatant.combatantName + " Destroyed");
                yield return new WaitForSeconds(updateTextTime);

                playerCombatant_index = SelectNewCombatant(playerGroup);
                if(playerCombatant_index == -1) { //If no ship can be selected
                    StartCoroutine(Lose());
                }
                else {
                    playerCombatant = playerGroup.ships[playerCombatant_index];

                    AudioManager.instance.Play("Combat Swapped Ships");

                    updateCombatantUI(playerUI, playerCombatant, playerGroup);
                    updateTextBox(playerCombatant.combatantName + " has come to fight");
                    yield return new WaitForSeconds(updateTextTime);
                }
            }
        }

        if(playerCombatant_index != -1) {
            enableButton(switchButton);
            enableButton(attackButton);
            enableButton(runButton);
            PlayersTurn();

            Debug.Log("Test 2");
        }
    }

    /***************************************************************************************************************************************
            MAIN UI
    ***************************************************************************************************************************************/
    public void updateCombatantUI(GameObject shipUI, Combatant ship, CombatGroup fleet) {
        //Update Header
        shipUI.transform.GetChild(0).GetComponent<TMP_Text>().text =
            "Name: " + ship.combatantName + "\nHealth: " + ship.health + "/" + ship.maxHealth;

        //Update Health Bar
        float percent = ((float)ship.health) / ship.maxHealth;
        Vector3 pos = shipUI.transform.GetChild(1).GetChild(0).GetChild(0).localPosition;
        shipUI.transform.GetChild(1).GetChild(0).GetChild(0).localPosition = new Vector3(Mathf.Lerp(fleet.emptyPos, fleet.fullPos, percent), pos.y, pos.z); ; 

        //Update Image
        shipUI.transform.GetChild(2).GetComponent<Image>().sprite = ship.combatantImage;
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

        AudioManager.instance.Play("Button Pressed");

        for(int i = 0; i < playerCombatant.attacks.Count; i++) {
            Transform button = attackPanel.transform.GetChild(i + 1);
            button.GetChild(0).GetComponent<TMP_Text>().text = playerCombatant.attacks[i].attackName;
            button.GetChild(1).GetComponent<TMP_Text>().text = playerCombatant.attacks[i].type.ToString();
            button.GetChild(2).GetComponent<TMP_Text>().text = "Dam: " + playerCombatant.attacks[i].baseDamage;
        }

        attackPanel.SetActive(!attackPanel.activeInHierarchy);
    }

    public void OnSwapButtonPressed() {
        if(gameState != GameState.PlayersTurn) {
            return;
        }

        AudioManager.instance.Play("Button Pressed");

        StartCoroutine(PlayerCheckSwitch());

        if(possibleSwitches.Count == 0)
            return;

        //Switch Art Imagies and Pull up display

        if(possibleSwitches.Count == 1) {
            oneCombatant_swapPopUp.transform.GetChild(2).GetComponent<Image>().sprite = possibleSwitches[0].combatantImage;
            oneCombatant_swapPopUp.transform.GetChild(2).GetChild(0).GetComponent<TMP_Text>().text = possibleSwitches[0].combatantName;

            oneCombatant_swapPopUp.SetActive(true);
        }
        else if(possibleSwitches.Count == 2) {
            twoCombatant_swapPopUp.transform.GetChild(2).GetComponent<Image>().sprite = possibleSwitches[0].combatantImage;
            twoCombatant_swapPopUp.transform.GetChild(2).GetChild(0).GetComponent<TMP_Text>().text = possibleSwitches[0].combatantName;

            twoCombatant_swapPopUp.transform.GetChild(3).GetComponent<Image>().sprite = possibleSwitches[1].combatantImage;
            twoCombatant_swapPopUp.transform.GetChild(3).GetChild(0).GetComponent<TMP_Text>().text = possibleSwitches[1].combatantName;

            twoCombatant_swapPopUp.SetActive(true);
        }
    }

    public void OnFleeButtonPressed() {
        if(gameState != GameState.PlayersTurn) {
            return;
        }

        AudioManager.instance.Play("Button Pressed");

        fleePopUp.SetActive(true);
    }

    /***************************************************************************************************************************************
            WINDOWS UI
    ***************************************************************************************************************************************/
    public void attackButtons(TMP_Text text) {
        string attackName = text.text;
        attackPanel.SetActive(false);
        StartCoroutine(PlayerAttack(attackName));
    }

    public void Ship1_ButtonPressed() {
        StartCoroutine(PlayerSwitch(0));
        oneCombatant_swapPopUp.SetActive(false);
        twoCombatant_swapPopUp.SetActive(false);
        AudioManager.instance.Play("Button Pressed");
    }

    public void Ship2_ButtonPressed() {
        StartCoroutine(PlayerSwitch(1));
        twoCombatant_swapPopUp.SetActive(false);
        AudioManager.instance.Play("Button Pressed");
    }

    public void BackButtonPressed() {
        oneCombatant_swapPopUp.SetActive(false);
        twoCombatant_swapPopUp.SetActive(false);
        AudioManager.instance.Play("Button Pressed");
    }

    public void YesFleeButtonPressed() {
        fleePopUp.SetActive(false);

        AudioManager.instance.Play("Button Pressed");

        StartCoroutine(PlayerRun());
    }

    public void NoFleeButtonPressed() {
        fleePopUp.SetActive(false);

        AudioManager.instance.Play("Button Pressed");
    }
}