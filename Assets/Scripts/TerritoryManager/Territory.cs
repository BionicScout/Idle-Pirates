using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Territory : MonoBehaviour 
{
    public int totalCities = 0, controlledCities = 0;

    [SerializeField]
    public bool controlled = false, updated = false;
    public List<GameObject> cityList;


    void Start() 
    {
        totalCities = cityList.Count;
    }

    void Update() 
    {

        if (controlledCities == totalCities && !controlled)
        {
            controlled = true;
        }


        if(controlled && !updated) 
        {
            updated = true;
            ControlManager.controlledTerritories++;
        }
            
    }
}
