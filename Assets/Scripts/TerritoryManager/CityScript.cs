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
    
    // Start is called before the first frame update
    void Start()
    {
       //raidPopUp.GetComponent<RaidPopUpScript>().
       //this.gameObject.name.ToIntArray();
       int.TryParse(this.gameObject.name, out cityNumber);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCityButtonPressed()
    {
        raidPopUp.gameObject.GetComponent<RaidPopUpScript>()
            .cityNumberUpdate(this.gameObject.GetComponent<CityScript>().cityNumber);
        raidPopUp.SetActive(true);
        //raidPopUp.GetComponent<RaidPopUpScript>().
    }

    public void RaidedCity()
    {
        //if(raidPopUp.to)
    }

}

