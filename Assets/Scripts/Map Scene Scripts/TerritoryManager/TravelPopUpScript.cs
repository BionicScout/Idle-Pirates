using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelPopUpScript : MonoBehaviour {

    [SerializeField]
    private int currentCity = 0;

    [SerializeField]
    private List<CityButtonScript> cityScriptList;

    private void Update() {

    }

    public void YesButtonPressed() {
        //Disactivates the pop-up
        this.gameObject.SetActive(false);

        Node cityNode = cityScriptList[currentCity - 1].cityNode;

        if(cityNode.start == true)
            cityScriptList[currentCity - 1].TraveltoCity();
        else {
            cityNode.end = true;
            TimedActivityManager.instance.mapShip.currentLocation.find = true;
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
        currentCity = 0;
    }


    //Sets the current city the player is trying to raid 
    //based on which city object called this function
    public void CityNumberUpdate(int cityNum) {

        currentCity = cityNum;
    }

}