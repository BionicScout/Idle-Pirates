
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class CityButtonScript : MonoBehaviour {
    [SerializeField]
    private GameObject travelPopUp;

    [SerializeField]
    private GameObject currentTerritory;

    [SerializeField]
    private int cityNumber;

    [SerializeField]
    public bool cityTaken = false;

    [SerializeField]
    private string cityName;

    [SerializeField]
    private string citySceneName;

    public Node cityNode;

    void Start() {
        //Grab the name of the game object and turn it into an int 
        //and put it in the cityNumber variable
        int.TryParse(this.gameObject.name, out cityNumber);

        //This checks if the city is not taken (although when the scene is loaded,
        //any data from before you travelled to a city is erased) 
        //AND if the city is inside of the list of city on the static class
        if(CityInbetweenManagementScript.staticCityList.Contains(cityNumber)
            && cityTaken == false /*&& 
            CityInbetweenManagementScript.currentStaticCityNumber == cityNumber*/) {
            cityTaken = true;
            RaidedCity();

        }
        //else if (CityInbetweenManagementScript.staticCityList.Contains(cityNumber)
        //    && cityTaken == false)
        //{
        //    cityTaken = true;
        //    ChangeCityColor();
        //}


    }

    // Update is called once per frame
    void Update() {


    }

    //sets the cityTaken boolean to true or false
    //based on the parameter
    public void SetCityTaken(bool signal) {
        cityTaken = signal;

    }

    //When the button is pressed and the city has not been taken,
    //it shows the raid pop-up and sends its cityNumber
    //to the raid pop-up script
    public void OnCityButtonPressed() {
    //If the Ship is moving from locations, don't allow to click ona new city
        if(MapShip.done == false)
            return;

        travelPopUp.GetComponent<TravelPopUpScript>()
            .CityNumberUpdate(cityNumber);

        //activates the pop-up
        travelPopUp.SetActive(true);

    }


    //If the city has been raided,
    //change the color of the image and
    //call the AddControlledCities() function in the territory object
    //that the city currently resides in.
    public void RaidedCity() {
        if(cityTaken == true) {
            //change the color of image to gray
            this.gameObject.GetComponent<Image>().color =
                new Color32(77, 77, 77, 255);
            currentTerritory.GetComponent<Territory>().AddControlledCities();
        }

    }
    public void ChangeCityColor() {
        if(cityTaken == true) {
            //change the color of image to gray
            this.gameObject.GetComponent<Image>().color =
                new Color32(77, 77, 77, 255);

        }

    }

    public void TraveltoCity() {
        SceneManager.LoadScene(citySceneName);
    }

}