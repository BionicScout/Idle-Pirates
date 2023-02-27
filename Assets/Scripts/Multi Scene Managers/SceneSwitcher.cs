using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour {
    public static SceneSwitcher instance;

    public static string currentScene;

    void Awake() {
        if(instance == null)
            instance = this;
        else {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    public void A_ExitButton() {
        Application.Quit();
    }

    public void A_LoadScene(string sceneName) {
    //Switch Scene
        currentScene = sceneName;
        SceneManager.LoadScene(sceneName);

    //Map Scene Extra Set Up
        if(sceneName == "Map Scene") {
            //Debug.Log("Map Scene");
            StartCoroutine(MapSceneRefresh());
        }
        else if(sceneName != "Map Scene") {
            Pathfinding.clear();
        }
    }

    IEnumerator MapSceneRefresh() {
        yield return new WaitForSeconds(Time.deltaTime);
        MapShip.instance.ship = GameObject.Find("Ship (MapShip)");
        MapShip.instance.timeQuery = TimedActivityManager.instance.getShipQuery();
        Pathfinding.refresh();
        TimedActivityManager.instance.refreshQuerries();
    }

    private void Update() {

    }
}
