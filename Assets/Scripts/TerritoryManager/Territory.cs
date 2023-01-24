using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Territory : MonoBehaviour {
    [SerializeField]
    private int totalCities = 0, controlledCities = 0;

    [SerializeField]
    private bool controlled = false, updated = false;

    [SerializeField]
    private List<GameObject> cityList;


    void Start() {
        totalCities = cityList.Count;
    }

    void Update() {

        if(controlledCities == totalCities && !controlled) {
            controlled = true;
        }


        if(controlled && !updated) {
            updated = true;
            ControlManager.controlledTerritories++;
            Debug.Log(ControlManager.controlledTerritories);
        }

    }

    /*
        Public method to see if all cites controled
    */
    public void AddControlledCities() {
        controlledCities += 1;
    }

}