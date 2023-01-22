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
    public int cityNumber;
    public bool cityTaken;
    
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
        raidPopUp.gameObject.GetComponent<RaidPopUpScript>()
            .cityNumberUpdate(/*this.gameObject.GetComponent<CityScript>().*/cityNumber);
        raidPopUp.SetActive(true);
        
    }

    public void RaidedCity()
    {
        //if(raidPopUp.to)
    }

}

