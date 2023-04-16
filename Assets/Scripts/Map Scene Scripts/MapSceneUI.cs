using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MapSceneUI : MonoBehaviour {
    [Header("Base UI")]
    public GameObject baseUI;

    [Header("Fleet UI")]
    public GameObject fleetBaseMenu;
    public GameObject availableCombatShips;

    public GameObject prefabCombatShip;
    List<GameObject> combatShips = new List<GameObject>();
    List<GameObject> availableCombatShipList = new List<GameObject>();

    [Header("Trade UI")]
    public GameObject tradeOverview;
    public GameObject tradeResources;
    public GameObject activeShipScrollWheel;
    public GameObject buySellButtons;

    public GameObject resourceBarUI;

    public GameObject availableTradeShip;
    public GameObject availableTradeShipScrollWheel;

    public GameObject prefabActiveTradeShips;
    List<ActiveTradeShip> tradeShipList = new List<ActiveTradeShip>();

    /**************************************************************************************************************************
        BASE UI 
    **************************************************************************************************************************/

    //This method is called every update
    public void updateResources() {
        string[] resourceNames = { "Gold", "Cloth", "Metal", "Wood" };
        Transform resourceHeader = baseUI.transform.GetChild(0);

        for(int i = 0; i < resourceNames.Length; i++) {
            Resource r = Inventory.instance.resources.Find(x => x.GetName() == resourceNames[i]);
            resourceHeader.GetChild(i).GetChild(1).GetComponent<TMP_Text>().text = r.GetAmount().ToString("n0");
        }
    }

    public void B_FleetButton() {
        baseUI.SetActive(false);
        fleetBaseMenu.SetActive(true);
        availableCombatShips.SetActive(false);

        refreshCombatShips();
    }

    public void B_TradingButton() {
        baseUI.SetActive(false);
        tradeOverview.SetActive(true);
    }

    /**************************************************************************************************************************
        FLEET UI 
    **************************************************************************************************************************/

//Base Menu
    public void refreshCombatShips() {
        for(int i = combatShips.Count - 1; i >= 0; i--) {
            GameObject obj = combatShips[i];
            combatShips.Remove(obj);
            Destroy(obj);
        }
        
        foreach(InventoryShip ship in Inventory.instance.ships) {
            if(ship.use == InventoryShip.USED_IN.combat) {
                GameObject obj = Instantiate(prefabCombatShip);
                obj.transform.GetChild(0).GetComponent<Image>().sprite = ship.shipImage;
                string shipInfo = ship.GetShipName() + "\n" + ship.combatType + "\nSpeed: " + ship.speed;
                obj.transform.GetChild(1).GetComponent<TMP_Text>().text = shipInfo;

                obj.GetComponent<Button>().onClick.AddListener(() => { B_AvailableCombatShips(ship.shipImage, shipInfo, ship.GetShipName()); });

                obj.transform.SetParent(fleetBaseMenu.transform.GetChild(2).GetChild(0));
                combatShips.Add(obj);
            }
        }
    }

//Available Combat Ships
    public void B_AvailableCombatShips(Sprite shipImage, string shipInfo, string oldShipName) {
        fleetBaseMenu.SetActive(false);
        availableCombatShips.SetActive(true);

        Transform currentShip = availableCombatShips.transform.GetChild(2).GetChild(1);

        currentShip.GetChild(0).GetComponent<Image>().sprite = shipImage;
        currentShip.GetChild(1).GetComponent<TMP_Text>().text = shipInfo;

        listAvailableCombatShips(oldShipName);
    }

    public void listAvailableCombatShips(string oldShipName) {
        for(int i = availableCombatShipList.Count - 1; i >= 0; i--) {
            GameObject obj = availableCombatShipList[i];
            availableCombatShipList.Remove(obj);
            Destroy(obj);
        }

        foreach(InventoryShip ship in Inventory.instance.ships) {
            if(ship.use == InventoryShip.USED_IN.none) {
                GameObject obj = Instantiate(prefabCombatShip);
                obj.transform.GetChild(0).GetComponent<Image>().sprite = ship.shipImage;
                string shipInfo = ship.GetShipName() + "\n" + ship.combatType + "\nSpeed: " + ship.speed;
                obj.transform.GetChild(1).GetComponent<TMP_Text>().text = shipInfo;

                obj.GetComponent<Button>().onClick.AddListener(() => { B_SelectedCombatShip(oldShipName, ship.GetShipName()); });

                obj.transform.SetParent(availableCombatShips.transform.GetChild(3).GetChild(0));
                availableCombatShipList.Add(obj);
            }
        }
    }

    public void B_SelectedCombatShip(string oldShipName, string newShipName) {
        Debug.Log("Old: " + oldShipName + "\nNew: " + newShipName);
        Inventory.instance.ships.Find(x => (x.GetShipName() == oldShipName && x.use == InventoryShip.USED_IN.combat)).use = InventoryShip.USED_IN.none;
        Inventory.instance.ships.Find(x => (x.GetShipName() == newShipName && x.use == InventoryShip.USED_IN.none)).use = InventoryShip.USED_IN.combat;




        B_FleetButton();
    }

    /**************************************************************************************************************************
        TRADE UI 
    **************************************************************************************************************************/

//Trade Overview
    public void updateTradeResources() {
        string[] resourceNames = { "Coconut", "Ore", "Rum" };
        Transform resourceHeader = tradeResources.transform;

        for(int i = 0; i < resourceNames.Length; i++) {
            Resource r = Inventory.instance.resources.Find(x => x.GetName() == resourceNames[i]);
            resourceHeader.GetChild(i).GetChild(1).GetComponent<TMP_Text>().text = r.GetAmount().ToString("n0");
        }
    }

    public void addNewTrade() {
        GameObject obj = Instantiate(prefabActiveTradeShips);
        ActiveTradeShip script = obj.GetComponent<ActiveTradeShip>();

        obj.transform.SetParent(activeShipScrollWheel.transform);
        //obj.GetComponent<RectTransform>().sizeDelta = new Vector2(-143.5365f, 0);
        tradeShipList.Add(script);
    }

    public void updateAvailableTradeShips() {
    //See if there available ships
        List<InventoryShip> ships = Inventory.instance.ships.FindAll(x => x.use == InventoryShip.USED_IN.none); 

        if(ships.Count != 0) {
            buySellButtons.transform.GetChild(0).gameObject.SetActive(false);
            buySellButtons.transform.GetChild(1).gameObject.SetActive(true);
            buySellButtons.transform.GetChild(2).gameObject.SetActive(true);
        }
        else {
            buySellButtons.transform.GetChild(0).gameObject.SetActive(true);
            buySellButtons.transform.GetChild(1).gameObject.SetActive(false);
            buySellButtons.transform.GetChild(2).gameObject.SetActive(false);
        }
    }

    public void B_Close_TradeMenu() {
        baseUI.SetActive(true);
        tradeOverview.SetActive(false);
    }

//Buying and Selling
    public void updateResourceBarUI() {
        string[] resourceNames = { "Gold", "Coconut", "Ore", "Rum" };

        for(int i = 0; i < resourceNames.Length; i++) {
            Resource r = Inventory.instance.resources.Find(x => x.GetName() == resourceNames[i]);
            resourceBarUI.transform.GetChild(i).GetChild(1).GetComponent<TMP_Text>().text = r.GetAmount().ToString("n0");
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

    public void B_Buy() {
        tradeOverview.SetActive(false);
        resourceBarUI.SetActive(true);


        populateBuyMenu();

        foreach(CityButtonScript city in FindObjectsOfType<CityButtonScript>()) {
            GameObject tradePopUp = city.gameObject.transform.GetChild(0).gameObject;
            city.gameObject.transform.GetChild(0).gameObject.SetActive(true); //Show Pop up
            tradePopUp.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() => { B_AvailableTradeShip(); });
        }
    }

    public void B_Sell() {

    }

    //Available Trade Ships

    public void B_AvailableTradeShip() {
        resourceBarUI.SetActive(false);
        availableTradeShip.SetActive(true);
    }

    /**************************************************************************************************************************
        OTHER UI 
    **************************************************************************************************************************/
    void Update() {
        updateResources();
        updateTradeResources();
        updateResourceBarUI();
        updateAvailableTradeShips();
    }
}
