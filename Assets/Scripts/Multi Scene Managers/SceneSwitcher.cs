using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour {
    public static SceneSwitcher instance;

    public static string currentScene;
    public static bool firstMapLoad = true;

    void Awake() {
        if(instance == null)
            instance = this;
        else {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        currentScene = SceneManager.GetActiveScene().name;
        //Debug.Log(currentScene);
    }

    public void A_ExitButton() {
        Application.Quit();
    }

    public void A_LoadScene(string sceneName) {
    //Switch Scene
        SceneManager.LoadScene(sceneName);

    //Map Scene Extra Set Up
        if(sceneName == "Map Scene") {
            //Debug.Log("Map Scene");
            StartCoroutine(MapSceneRefresh());
        }
        else if(sceneName != "Map Scene") {
            Pathfinding.clear();
        }

        if (sceneName == "Title")
        {
            //Delete items in inventory except for templates
            TitleSceneReset();
        }

        currentScene = sceneName;
    }

    IEnumerator MapSceneRefresh() {
        yield return new WaitForSeconds(Time.deltaTime);
        MapShip mapShip = GameObject.Find("SceneController").GetComponent<MapShip>();
        TimedActivityManager.instance.mapShip = mapShip;

        mapShip.timeQuery = TimedActivityManager.instance.getShipQuery();
        Pathfinding.refresh();
        TimedActivityManager.instance.refreshQuerries();
        mapShip.loadCurrentLocation();

        //This just load testing ships and crew
        if (firstMapLoad)
        {
            firstMapLoad = false;

            for(int i = 0; i < 3; i++)
            {
                InventoryShip ship = new InventoryShip(Inventory.instance.shipTemplates[Random.Range(0, Inventory.instance.shipTemplates.Count - 1)]);
                ship.use = InventoryShip.USED_IN.combat;
                Inventory.instance.AddShip(ship);
            }

            //foreach (MainShips template in Inventory.instance.shipTemplates)
            //{
            //    Inventory.instance.AddShip(new InventoryShip(template));
            //    break;
            //}
            //foreach (MainCrewMembers template in Inventory.instance.crewTemplates)
            //{
            //    InventoryCrew crew =
            //        new InventoryCrew(Inventory.instance.crewTemplates
            //        [Random.Range(0, Inventory.instance.shipTemplates.Count - 1)]);
            //    crew.active = true;
            //    Inventory.instance.AddCrew(crew);
            //    break;
            //}

            InventoryCrew crew =
                    new InventoryCrew(Inventory.instance.crewTemplates
                    [Random.Range(0, Inventory.instance.crewTemplates.Count - 1)]);
            crew.active = true;
            Inventory.instance.AddCrew(crew);

        }
    }


    void TitleSceneReset()
    {
        for (int i = 1; i < Inventory.instance.resources.Count; i++)
        {
            Inventory.instance.resources[i].amount = 0;
        }
        Inventory.instance.crew.Clear();
        Inventory.instance.ships.Clear();

        firstMapLoad = true;

        //Inventory.instance.resources.Clear();

    }


}
