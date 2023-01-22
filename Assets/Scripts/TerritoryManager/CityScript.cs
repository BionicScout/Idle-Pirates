using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CityScript : MonoBehaviour
{

    //public GameObject cityButton;
    public GameObject raidPopUp;
    public GameObject currentTerritory;
    public int cityNumber;
    public bool cityTaken = false;
    
    // Start is called before the first frame update
    void Start()
    {
       //Grab the name of the game object and turn it into an int 
       //and put it in the cityNumber var
       int.TryParse(this.gameObject.name, out cityNumber);
    }

    // Update is called once per frame
    void Update()
    {


    }

    public void OnCityButtonPressed()
    {
        if (cityTaken == false)
        {
            raidPopUp.GetComponent<RaidPopUpScript>()
                .CityNumberUpdate(cityNumber);
            raidPopUp.SetActive(true);
        }
    }

    public void RaidedCity()
    {
        if (cityTaken == true)
        {
            this.gameObject.GetComponent<Image>().color =
                new Color32(77, 77, 77, 255);
            currentTerritory.GetComponent<Territory>().controlledCities += 1;
        }
        
    }

}

