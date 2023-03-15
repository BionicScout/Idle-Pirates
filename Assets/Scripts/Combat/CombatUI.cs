using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;


public class CombatUI : MonoBehaviour {
    public TMP_Text headerUI, textUI;

    Queue<string> headerQueue = new Queue<string>();
    Queue<string> textQueue = new Queue<string>();
    bool updated = true, startTic = true;

    public bool clickToMoveText;
    public bool canUseButttons = false;
    public bool switched;

    public List<CombatShip> shipList = new List<CombatShip>();

    public GameObject attackButton, switchButton, runButton;

    //Ship Display UI
    public GameObject playerShipUI, enemyShipUI;

    private void Update() {
        if((Input.GetKeyDown(KeyCode.Mouse0) && !updated && clickToMoveText) || startTic) { //PUT INPUT MANAGER CODE IN
            updateTextBox();
            startTic = false;
        }
        if(Input.GetKeyDown(KeyCode.Mouse0))
            Debug.Log("Update: " + updated + "\nClick: " + clickToMoveText);


    //ALlow Button Use again
        if(!canUseButttons && updated) {
            canUseButttons = true;
        }

        if(!canUseButttons) {
            disableButton(attackButton);
            disableButton(switchButton);
            disableButton(runButton);
        }
        else if(canUseButttons && switched) {
            enableButton(attackButton);
            disableButton(switchButton);
            enableButton(runButton);
        }
        else {
            enableButton(attackButton);
            enableButton(switchButton);
            enableButton(runButton);
        }
    }

    public void updateShipUI(GameObject shipUI, CombatShip ship) {
    //Update Header
        shipUI.transform.GetChild(0).GetComponent<TMP_Text>().text =
            "GetName: " + ship.shipName + "\nHealth: " + ship.health + "/" + ship.maxHealth;

        //Update Health Bar
        //Update Image
        shipUI.transform.GetChild(2).GetComponent<Image>().sprite = ship.shipImage;
    }

    public void enableButton(GameObject button) {
        button.GetComponent<Graphic>().color = Color.white;
        button.GetComponent<Button>().enabled = true;
    }

    public void disableButton(GameObject button) {
        button.GetComponent<Graphic>().color = Color.grey;
        button.GetComponent<Button>().enabled = false;
    }

    public void updateTextBox() {
        Debug.Log("Before");

        headerUI.text = headerQueue.Dequeue();
        textUI.text = textQueue.Dequeue();

        if(headerQueue.Count == 0) {
            updated = true;
        }

        print();
        Debug.Log(headerQueue.Count);
        Debug.Log("After");
    }

    public void print() {
        for(int i = 0; i < headerQueue.Count; i++) {
            string temp = textQueue.Dequeue();
            Debug.Log(temp);
            textQueue.Enqueue(temp);
        }
    }

    public void add(string header, string text) {
        headerQueue.Enqueue(header);
        textQueue.Enqueue(text);

        if(updated)
            updated = false;

        Debug.Log("added");
    }

    public bool getUpdate() {
        return updated;
    }
}
