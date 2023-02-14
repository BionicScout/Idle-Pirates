using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class RaidPopUpScript : MonoBehaviour
{
    [SerializeField]
    private int cityNumber;

    [SerializeField]
    private string mapSceneName;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void YesButtonPressed()
    {
        //Disactivates the pop-up
        this.gameObject.SetActive(false);

        //sets the int of the static class equal to the cityNumber of this scene
        //and inserts the cityNumber into the list in the static class
        CityInbetweenManagementScript.currentStaticCityNumber = cityNumber;
        CityInbetweenManagementScript.staticCityList.Add(cityNumber);


        SceneManager.LoadScene(mapSceneName);

    }

    //When they press the no button, the pop-up is disactivated 
    //and resets the current city the player clicked on
    public void NoButtonPressed()
    {
        this.gameObject.SetActive(false);
        cityNumber = 0;
    }

    public void CityNumberUpdate(int cityNum)
    {

        cityNumber = cityNum;
    }

}
