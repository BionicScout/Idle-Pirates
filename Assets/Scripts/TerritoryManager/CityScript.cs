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
    
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCityButtonPressed()
    {
        raidPopUp.SetActive(true);
    }

    public void RaidedCity()
    {
        //if(raidPopUp.to)
    }

}

