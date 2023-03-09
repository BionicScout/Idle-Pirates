using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TradingManager : MonoBehaviour {
    public GameObject availableShipScreen, availableShipUI, availableShipInfoPrefab;
    List<GameObject> availableShips;

    public GameObject tradeShipScreen, tradeShipUI, tradeShipInfoPrefab;
    List<GameObject> tradeShips;

    void Start() {
        availableShips = new List<GameObject>();
        tradeShips = new List<GameObject>();
    }


    //Button Interactions
    public void openMenu() {
        refreshTradeShipList();
        tradeShipScreen.SetActive(true);

        AudioManager.instance.Play("Menu Sound");
    }

    public void toAvailiableScreen() {
        tradeShipScreen.SetActive(false);

        refreshAvailableList();
        availableShipScreen.SetActive(true);

        AudioManager.instance.Play("Menu Sound");
    }

    public void toTradeScreen() {
        availableShipScreen.SetActive(false);

        refreshTradeShipList();
        tradeShipScreen.SetActive(true);

        AudioManager.instance.Play("Menu Sound");
    }

    public void closeMenu() {
        availableShipScreen.SetActive(false);
        tradeShipScreen.SetActive(false);

        AudioManager.instance.Play("Menu Sound");
    }

//Available Ship Functions
    public void refreshAvailableList() {
        deleteAvailableList();

        foreach(InventoryShip ship in Inventory.instance.ships) {
            if(ship.use == InventoryShip.USED_IN.none) {
                GameObject obj = Instantiate(availableShipInfoPrefab);
                obj.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().sprite = ship.shipImage;
                obj.transform.GetChild(1).GetComponent<TMP_Text>().text = ship.shipName + "\nSpeed: " + ship.speed + "\nMax Cargo: " + ship.maxCargo;

                obj.transform.SetParent(availableShipUI.transform);
                availableShips.Add(obj);
            }
        }
    }

    public void deleteAvailableList() {
        foreach(GameObject obj in availableShips) {
            Destroy(obj);
        }
    }

//Available Ship Functions
    public void refreshTradeShipList() {
        deleteTradeShipList();

        foreach(InventoryShip ship in Inventory.instance.ships) {
            if(ship.use == InventoryShip.USED_IN.trading) {
                GameObject obj = Instantiate(tradeShipInfoPrefab);
                obj.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().sprite = ship.shipImage;
                obj.transform.GetChild(1).GetComponent<TMP_Text>().text = ship.shipName + "\nTIME LEFT: #######\nPROFIT: #####";

                obj.transform.SetParent(tradeShipUI.transform);
                tradeShips.Add(obj);
            }
        }
    }

    public void deleteTradeShipList() {
        foreach(GameObject obj in tradeShips) {
            Destroy(obj);
        }
    }
}
