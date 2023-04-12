using UnityEngine;

public class RaidPopUpScript : MonoBehaviour {
    [SerializeField]
    private string mapSceneName;

    public void YesButtonPressed() 
    {
        AudioManager.instance.Play("Menu Sound");

        gameObject.SetActive(false);

        CityInbetweenManagementScript.newRaidedCity(CityLastVistedInfo.instance.cityName);

        SceneSwitcher.instance.A_LoadScene(mapSceneName);
    }

    //When they press the no button, the pop-up is disactivated 
    //and resets the current city the player clicked on
    public void NoButtonPressed() 
    {
        AudioManager.instance.Play("Menu Sound");

        gameObject.SetActive(false);
        //CityLastVistedInfo.instance.cityName = null;
    }
}
