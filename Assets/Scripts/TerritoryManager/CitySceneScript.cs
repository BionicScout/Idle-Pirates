using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CitySceneScript : MonoBehaviour
{
    [SerializeField]
    private GameObject raidPopUp;

    [SerializeField]
    private string mapSceneName;

    [SerializeField]
    private int cityNumber;


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

            raidPopUp.GetComponent<RaidPopUpScript>()
                .CityNumberUpdate(cityNumber);
        }
    }

    public void OnMapButtonPressed()
    {
        SceneManager.LoadScene(mapSceneName);
    }
}
