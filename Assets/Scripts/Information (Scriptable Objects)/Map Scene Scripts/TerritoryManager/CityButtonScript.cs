using UnityEngine;
using UnityEngine.UI;

public class CityButtonScript : MonoBehaviour {
    [SerializeField]
    private GameObject travelPopUp;

    [SerializeField]
    private GameObject currentTerritory;

    [SerializeField]
    public bool cityTaken = false;

    public Node cityNode;

    void Start() {
        //This checks if the city is not taken (although when the scene is loaded,
        //any data from before you travelled to a city is erased) 
        //AND if the city is inside of the list of city on the static class
        if(CityInbetweenManagementScript.citesThatHaveBeenRaided.Contains(gameObject.name) && cityTaken == false) {
            cityTaken = true;
            RaidedCity();
        }
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

        //activates the pop-up
        travelPopUp.GetComponent<TravelPopUpScript>().activate(gameObject.name);
    }


    //If the city has been raided,
    //change the color of the image and
    //call the AddControlledCities() function in the territory object
    //that the city currently resides in.
    public void RaidedCity() {
        if(cityTaken == true) { 
            gameObject.GetComponent<Image>().color = Color.gray;
            currentTerritory.GetComponent<Territory>().AddControlledCities();
        }
    }
    public void ChangeCityColor() {
        if(cityTaken == true) {
            gameObject.GetComponent<Image>().color = Color.gray;

        }
    }

    public void TraveltoCity() {
        SceneSwitcher.instance.A_LoadScene("City 1 Scene");
    }

    public string getObjectName() {
        return name;
    }
}