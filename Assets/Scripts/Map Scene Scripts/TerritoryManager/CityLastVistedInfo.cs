using UnityEngine;

public class CityLastVistedInfo : MonoBehaviour {
    public static CityLastVistedInfo instance;

    public bool cityTaken = false;
    public string cityName = null;

    void Start() {
        if(instance == null)
            instance = this;
        else {
            Destroy(gameObject);
            return;
        }
    }
}
