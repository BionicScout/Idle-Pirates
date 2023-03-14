using UnityEngine;

public class RaidPopUpScript : MonoBehaviour {
    [SerializeField]
    private string mapSceneName;

    public void YesButtonPressed() {
        gameObject.SetActive(false);

        CityInbetweenManagementScript.newRaidedCity(CityLastVistedInfo.instance.cityName);

        SceneSwitcher.instance.A_LoadScene(mapSceneName);
    }

    //When they press the no button, the pop-up is disactivated 
    //and resets the current city the player clicked on
    public void NoButtonPressed() {
        gameObject.SetActive(false);
        //CityLastVistedInfo.instance.cityName = null;
    }
}
