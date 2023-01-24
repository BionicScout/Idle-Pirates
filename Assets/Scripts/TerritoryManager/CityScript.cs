using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CityScript : MonoBehaviour
{
    [SerializeField] 
    private GameObject raidPopUp;

    [SerializeField] 
    private GameObject currentTerritory;

    [SerializeField] 
    private int cityNumber;

    [SerializeField] 
    private bool cityTaken = false;
    
    // Start is called before the first frame update
    void Start()
    {
       //Grab the name of the game object and turn it into an int 
       //and put it in the cityNumber variable
       int.TryParse(this.gameObject.name, out cityNumber);
    }

    // Update is called once per frame
    void Update()
    {


    }

    //sets the cityTaken boolean to true or false
    //based on the parameter
    public void SetCityTaken(bool signal) 
    {
        cityTaken = signal;
    
    }

    //When the button is pressed and the city has not been taken,
    //it shows the raid pop-up and sends its cityNumber
    //to the raid pop-up script
    public void OnCityButtonPressed()
    {
        if (cityTaken == false)
        {
            raidPopUp.GetComponent<RaidPopUpScript>()
                .CityNumberUpdate(cityNumber);

            //activates the pop-up
            raidPopUp.SetActive(true);
        }
    }


    //If the city has been raided,
    //change the color of the image and
    //call the AddControlledCities() function in the territory object
    //that the city currently resides in.
    public void RaidedCity()
    {
        if (cityTaken == true)
        {
            //change the color of image to gray
            this.gameObject.GetComponent<Image>().color =
                new Color32(77, 77, 77, 255);
            currentTerritory.GetComponent<Territory>().AddControlledCities();
        }
        
    }

}

