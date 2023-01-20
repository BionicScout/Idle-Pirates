using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaidPopUpScript : MonoBehaviour
{
    public bool tookCity = false;
    public List<CityScript> cityList;
    public int currentCity = 0;

    //try to change color of city when raided

    private void Update()
    {

    }

    public void YesButtonPressed()
    {
        //raidPopUp.SetActive(true);

        //currentCity = cityList;
        tookCity = true;
        this.gameObject.SetActive(false); 



    }

    public void NoButtonPressed()
    {
        this.gameObject.SetActive(false);
        currentCity = 0;
    }

    public void cityNumberUpdate(int cityNum)
    {
        currentCity = cityList[cityNum-1].cityNumber;
    }

}
