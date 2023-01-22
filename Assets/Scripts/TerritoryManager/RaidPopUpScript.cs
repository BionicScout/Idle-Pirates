using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaidPopUpScript : MonoBehaviour
{

    [SerializeField]
    private int currentCity = 0;

    [SerializeField]
    private List<CityScript> cityScriptList;


    //try to change color of city when raided

    private void Update()
    {

    }

    public void YesButtonPressed()
    {
        //raidPopUp.SetActive(true);

        //currentCity = cityScriptList;

        this.gameObject.SetActive(false);
        cityScriptList[currentCity - 1].SetCityTaken(true);
        cityScriptList[currentCity - 1].RaidedCity();
        currentCity = 0;


    }

    public void NoButtonPressed()
    {
        this.gameObject.SetActive(false);
        currentCity = 0;
    }

    public void CityNumberUpdate(int cityNum)
    {
        //currentCity = cityScriptList[cityNum - 1].cityNumber;
        currentCity = cityNum;
    }

}
