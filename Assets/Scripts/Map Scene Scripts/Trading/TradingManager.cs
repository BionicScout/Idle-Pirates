using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TradingManager : MonoBehaviour {
    [Header("Available Ship Menu")]
    public GameObject availableShipScreen, availableShipUI, availableShipInfoPrefab;
    List<GameObject> availableShips;

    [Header("Trade Ship Menu")]
    public GameObject tradeShipScreen, tradeShipUI, tradeShipInfoPrefab;
    List<GameObject> tradeShips;

    [Header("Select Ship")]
    public InventoryShip selectedShip;
    public GameObject buySellMenu;

    void Start() {
        availableShips = new List<GameObject>();
        tradeShips = new List<GameObject>();
    }

//Main Trade Menus Button Interactions
    public void openMenu() {
        refreshTradeShipList();
        tradeShipScreen.SetActive(true);

        AudioManager.instance.Play("Menu Sound");
    }

    public void toAvailiableScreen() {
        tradeShipScreen.SetActive(false);
        buySellMenu.SetActive(false);
        selectedShip = null;

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
                obj.GetComponent<InventoryShipHolder>().ship = ship;
                obj.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().sprite = ship.shipImage;
                obj.transform.GetChild(0).transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() => { toBuySell(obj); });
                obj.transform.GetChild(1).GetComponent<TMP_Text>().text = ship.GetShipName() + "\nSpeed: " + ship.speed + "\nMax Cargo: " + ship.maxCargo;

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

//Trade Ship Functions
    public void refreshTradeShipList() {
        deleteTradeShipList();

        foreach(InventoryShip ship in Inventory.instance.ships) {
            if(ship.use == InventoryShip.USED_IN.trading) {
                GameObject obj = Instantiate(tradeShipInfoPrefab);
                obj.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().sprite = ship.shipImage;
                obj.transform.GetChild(1).GetComponent<TMP_Text>().text = ship.GetShipName() + "\nTIME LEFT: #######\nPROFIT: #####";

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

//Selecting to Trade
    public void toBuySell(GameObject selectShipObj) {
        availableShipScreen.SetActive(false);
        buySellMenu.SetActive(true);

        selectedShip = selectShipObj.GetComponent<InventoryShipHolder>().ship;
    }
}
