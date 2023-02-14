using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections;
using UnityEngine;

public class CombatUI : MonoBehaviour {
    public TMP_Text headerUI, textUI;

    Queue<string> headerQueue = new Queue<string>();
    Queue<string> textQueue = new Queue<string>();
    bool updated = true, startTic = true;
    public bool clickToMoveText;

    public List<CombatShip> shipList = new List<CombatShip>();

    private void Update() {
        if((Input.GetKeyDown(KeyCode.Mouse0) && !updated && clickToMoveText) || startTic) { //PUT INPUT MANAGER CODE IN
            updateTextBox();
            startTic = false;
        }
        if(Input.GetKeyDown(KeyCode.Mouse0))
            Debug.Log("Update: " + updated + "\nClick: " + clickToMoveText);

        for(int i = 0; i < shipList.Count; i++)
        {
            shipList[i].ui.text = shipList[i].shipName + "\n" 
                + shipList[i].health + "/" + shipList[i].maxHealth;
        }


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
