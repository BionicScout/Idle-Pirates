using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Territory : MonoBehaviour {
    public int totalCities = 4, controlledCities = 0;
    bool controlled = false, updated = false;

    void Start() {

    }

    void Update() {
        if(controlledCities == totalCities && !controlled)
            controlled = true;

        if(controlled && !updated) {
            updated = true;
            ControlManager.currentTerritories++;
        }
            
    }
}
