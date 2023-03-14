using UnityEngine;

public class CitySceneScript : MonoBehaviour {
    [SerializeField]
    private GameObject raidPopUp;

    [SerializeField]
    private GameObject raidButton;

    void Start() {
        if(CityInbetweenManagementScript.citesThatHaveBeenRaided.Contains(CityLastVistedInfo.instance.cityName)) {
            raidButton.SetActive(false);
        }
    }

    public void OnRaidButtonPressed() {
        if(CityLastVistedInfo.instance.cityTaken == false) {
            raidPopUp.SetActive(true);
        }
    }

    public void OnMapButtonPressed() {
        SceneSwitcher.instance.A_LoadScene("Map Scene");
    }
}
