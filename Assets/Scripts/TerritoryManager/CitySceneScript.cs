using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitySceneScript : MonoBehaviour
{
    [SerializeField]
    private GameObject raidPopUp;

    
    public static bool cityTaken = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnRaidButtonPressed()
    {
        if (cityTaken == false)
        {
            //activates the pop-up
            raidPopUp.SetActive(true);
        }
    }
}
