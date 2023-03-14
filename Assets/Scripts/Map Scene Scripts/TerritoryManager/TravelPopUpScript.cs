using System.Collections.Generic;
using UnityEngine;

public class TravelPopUpScript : MonoBehaviour {

    [SerializeField]
    private List<CityButtonScript> cityScriptList;

    string lastCityName;

    public void activate(string cityName) {
        gameObject.SetActive(true);
        lastCityName = cityName;
    }

    public void YesButtonPressed() {
        this.gameObject.SetActive(false);

        CityButtonScript cityButton = cityScriptList.Find(x => x.getObjectName() == lastCityName);
        if(cityButton == null)
            Debug.Log("NULL");
        else
            Debug.Log(lastCityName);

        Node cityNode = cityButton.cityNode;

        if(cityNode.start == true) {
            cityButton.TraveltoCity();
            Debug.Log("Start");
        }
        else {
            cityNode.end = true;
            TimedActivityManager.instance.mapShip.currentLocation.find = true;
            Debug.Log("End");
        }
            



        //Sends the true statement into the SetCityTaken function
        //to the specific object in the list
        //based on the current city the player clicked on 
        //cityScriptList[currentCity - 1].SetCityTaken(true);

        //then it calls the raidedCity function
        //to the specific object in the list
        //based on the current city the player clicked on 
        //cityScriptList[currentCity - 1].RaidedCity();

        //resets the current city the player clicked on
        //currentCity = 0;
    }

    //When they press the no button, the pop-up is disactivated 
    //and resets the current city the player clicked on
    public void NoButtonPressed() {
        this.gameObject.SetActive(false);
    }
}