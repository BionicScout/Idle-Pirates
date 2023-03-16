using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MapShip : MonoBehaviour {
    public TimeQuery timeQuery;
    public float percentDone;
    public static bool done = true;

    public GameObject ship;
    public Vector2 start, end;

    public Node currentLocation;

    void Start() {
        timeQuery = null;
        transform.position = currentLocation.gameObject.transform.position;

        loadCurrentLocation();
    }

    public void loadCurrentLocation() {
        if(CityLastVistedInfo.instance.cityName != null) {
            Debug.Log(CityLastVistedInfo.instance.cityName);

            foreach(Node n in Pathfinding.nodes) {
                if(n.nodeName == CityLastVistedInfo.instance.cityName) {
                    currentLocation = n;
                    Debug.Log(currentLocation.name);
                    break;
                }
            }
        }

        Debug.Log("Current City GetName: " + currentLocation.name);

        currentLocation.start = true;
        transform.position = currentLocation.transform.position;
    }

    public void setLocs() {
        if(timeQuery == null)
            return;

        timeQuery.refreshNodes();
        start = timeQuery.startNode.gameObject.transform.position; //#2
        end = timeQuery.endNode.gameObject.transform.position; //#3
    }

    public void updateLocation(Node node) {
        currentLocation.start = false;
        currentLocation = node;
        currentLocation.start = true;
        //Debug.Log("Current City GetName: " + currentLocation.name);
        CityLastVistedInfo.instance.cityName = currentLocation.name;
    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.Q)) {
            Resource r = new Resource(Resource.Type.Gold, "Gold", 1000, 1);
            Inventory.instance.AddResource(r);
        }

        if(timeQuery == null) {
            done = true;
            return;
        }

        done = false;

        TimeSpan timeLeft_TimeSpan = timeQuery.finishTime - System.DateTime.Now;
        double timeLeft_seconds = timeLeft_TimeSpan.TotalSeconds;
        double totalTime = timeQuery.timeInterval.TotalSeconds;

        percentDone = 1 - (float)(timeLeft_seconds / totalTime);

        ship.transform.position = Vector2.Lerp(start, end, percentDone);

        if(UnityEngine.Random.Range(0, 30000)  < 5)
            SceneSwitcher.instance.A_LoadScene(MinigameSelecter.getMinigame());
    }
}
