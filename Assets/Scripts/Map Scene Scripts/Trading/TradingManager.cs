using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.UI;

public class TradingManager : MonoBehaviour {
    [Header("Map Button")]
    public GameObject tradeButton;
    public GameObject exitButton;

    [Header("Available Ship Menu")]
    public GameObject availableShipScreen, availableShipUI, availableShipInfoPrefab;
    List<GameObject> availableShips;

    [Header("Trade Ship Menu")]
    public GameObject tradeShipScreen, tradeShipUI, tradeShipInfoPrefab;
    List<GameObject> tradeShips;

    [Header("Select Ship")]
    public InventoryShip selectedShip;
    public GameObject buySellMenu;

    [Header("Buy/Sell Menus")]
    public GameObject buyMenu;

    enum BuyOrSell{ buying, selling, neither };
    BuyOrSell buySellState = BuyOrSell.neither;


    void Start() {
        availableShips = new List<GameObject>();
        tradeShips = new List<GameObject>();
    }

//Main Trade Menus Button Interactions
    public void openMenu() {
        refreshTradeShipList();
        tradeShipScreen.SetActive(true);
        tradeButton.SetActive(false);


        AudioManager.instance.Play("Menu Sound");
    }

    public void toAvailiableScreen() {
        tradeShipScreen.SetActive(false);
        buySellMenu.SetActive(false);
        selectedShip = null;

        foreach(CityButtonScript city in FindObjectsOfType<CityButtonScript>()) {
            city.gameObject.GetComponent<Button>().enabled = false;
        }

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
        tradeButton.SetActive(true);

        foreach(CityButtonScript city in FindObjectsOfType<CityButtonScript>()) {
            city.gameObject.GetComponent<Button>().enabled = true;
        }

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
        toBuySell();

        selectedShip = selectShipObj.GetComponent<InventoryShipHolder>().ship;
    }

    public void toBuySell() {
        availableShipScreen.SetActive(false);
        buySellMenu.SetActive(true);
        exitButton.SetActive(false);

        foreach(CityButtonScript city in FindObjectsOfType<CityButtonScript>()) {
            city.gameObject.transform.GetChild(0).gameObject.SetActive(false); //Hide Pop up
        }
    }

//Buying
    public void toBuy(bool populateData) {
        buySellMenu.SetActive(false);
        exitButton.SetActive(true);
        buyMenu.SetActive(false);

        buySellState = BuyOrSell.buying;
        if(populateData)
            populateBuyMenu();

        foreach(CityButtonScript city in FindObjectsOfType<CityButtonScript>()) {
            city.gameObject.transform.GetChild(0).gameObject.SetActive(true); //Show Pop up
        }
    }

    public void populateBuyMenu() {
        foreach(CityButtonScript city in FindObjectsOfType<CityButtonScript>()) {
            Image resourceIcon = city.gameObject.transform.GetChild(0).transform.GetChild(2).GetComponent<Image>();
            TMP_Text numberText = city.gameObject.transform.GetChild(0).transform.GetChild(3).GetComponent<TMP_Text>();

            int randomResoureID = Random.Range(0, Inventory.instance.tradeResourceTemplates.Count - 1);
            MainResources resource = Inventory.instance.tradeResourceTemplates[randomResoureID];

            int maxAdjust = Mathf.CeilToInt(resource.buyValue * 0.10f);
            int adjust = Random.Range(-maxAdjust, maxAdjust);
            int buyValue = resource.buyValue + adjust;

            resourceIcon.sprite = resource.sprite;
            numberText.text = buyValue.ToString();

            if(adjust > maxAdjust / 2)
                numberText.color = Color.red;
            else if(adjust < -maxAdjust / 2)
                numberText.color = Color.green;
            else
                numberText.color = Color.yellow;
        }
    }

    public void selectCity(GameObject cityObject) {
        foreach(CityButtonScript city in FindObjectsOfType<CityButtonScript>()) {
            city.gameObject.transform.GetChild(0).gameObject.SetActive(false); //Hide Pop up
        }

        buyMenu.SetActive(true);
        buyMenu.transform.GetChild(2).GetChild(1).GetComponent<TMP_Text>().text = selectedShip.GetShipName() + "\nTIME: #######\nGain: #####\nLose: ######";

        
    }
}
