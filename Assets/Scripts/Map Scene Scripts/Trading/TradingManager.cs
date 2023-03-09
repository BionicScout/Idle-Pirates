using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TradingManager : MonoBehaviour {
    public GameObject tradeScreen, UIList, shipInfoPrefab;
    List<GameObject> availableShips;

    void Start() {
        availableShips = new List<GameObject>();
    }


    //Button Interactions
    public void openMenu() {
        refreshList();
        tradeScreen.SetActive(true);

        AudioManager.instance.Play("Menu Sound");
    }

    public void closeMenu() {
        tradeScreen.SetActive(false);

        AudioManager.instance.Play("Menu Sound");
    }

//Functions
    public void refreshList() {
        deleteList();

        foreach(InventoryShip ship in Inventory.instance.ships) {
            if(ship.use == InventoryShip.USED_IN.none) {
                GameObject obj = Instantiate(shipInfoPrefab);
                obj.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().sprite = ship.shipImage;
                obj.transform.GetChild(1).GetComponent<TMP_Text>().text = ship.shipName + "\nSpeed: " + ship.speed + "\nMax Cargo: " + ship.maxCargo;

                obj.transform.SetParent(UIList.transform);
                availableShips.Add(obj);
            }
        }
    }

    public void deleteList() {
        foreach(GameObject obj in availableShips) {
            Destroy(obj);
        }
    }
}
