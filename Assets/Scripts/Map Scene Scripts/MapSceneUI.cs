using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MapSceneUI : MonoBehaviour {
    [Header("Base UI")]
    public GameObject baseUI;

    [Header("Fleet UI")]
    public GameObject fleetBaseMenu;
    public GameObject availableCombatShips;
    public GameObject crewMenu;

    public Sprite blankAssest; 
    public GameObject prefabCombatShip;
    public GameObject prefabCrew;
    List<GameObject> combatShips = new List<GameObject>();
    List<GameObject> availableCombatShipList = new List<GameObject>();
    List<GameObject> crewList = new List<GameObject>();


    [Header("Trade UI")]
    public GameObject tradeOverview;
    public GameObject tradeResources;
    public GameObject activeShipScrollWheel;
    public GameObject buySellButtons;

    public GameObject resourceBarUI;
    public float refeshTradeTime = 5;
    float timeSinceTradeTime = 0;

    public GameObject availableTradeShip;
    public GameObject availableTradeShipScrollWheel;

    public GameObject prefabActiveTradeShips;
    public GameObject prefabAvailableTradeShip;
    List<GameObject> tradeShipList = new List<GameObject>();
    List<GameObject> availableTradeShipList = new List<GameObject>();

    int maxCargo;
    bool isBuy;

    List<TimeQuery> path = new List<TimeQuery>();
    TradeDeal potentialDeal = null;
    CityButtonScript selectedCity = null;

    void Start() {
        populateBuyMenu();
    }

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
        crewMenu.SetActive(false);

        foreach(CityButtonScript city in FindObjectsOfType<CityButtonScript>()) {
            city.gameObject.GetComponent<Button>().enabled = false;
        }

        refreshCombatShips();
        refreshActiveCrew();
    }

    public void B_TradingButton() {
        baseUI.SetActive(false);
        tradeOverview.SetActive(true);
        availableTradeShip.SetActive(false);

        foreach(CityButtonScript city in FindObjectsOfType<CityButtonScript>()) {
            city.gameObject.GetComponent<Button>().enabled = false;
        }
    }

    public void B_ExitButton() {
        baseUI.SetActive(true);
        tradeOverview.SetActive(false);
        fleetBaseMenu.SetActive(false);
        availableCombatShips.SetActive(false);

        foreach(CityButtonScript city in FindObjectsOfType<CityButtonScript>()) {
            //if(!city.gameObject.GetComponent<Node>().start)
                city.gameObject.GetComponent<Button>().enabled = true;
        }
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

        short currentCombatShips = 0;
        foreach(InventoryShip ship in Inventory.instance.ships) {
            if(ship.use == InventoryShip.USED_IN.combat) {
                GameObject obj = Instantiate(prefabCombatShip);
                obj.transform.GetChild(0).GetComponent<Image>().sprite = ship.shipImage;
                string shipInfo = ship.GetShipName() + "\n" + ship.combatType + "\nSpeed: " + ship.speed;
                obj.transform.GetChild(1).GetComponent<TMP_Text>().text = shipInfo;

                obj.GetComponent<Button>().onClick.AddListener(() => { B_AvailableCombatShips(ship.shipImage, shipInfo, ship.GetShipName()); });

                obj.transform.SetParent(fleetBaseMenu.transform.GetChild(2).GetChild(0));
                combatShips.Add(obj);

                currentCombatShips++;
            }
        }

        for(int i = currentCombatShips; i < 3; i++) {
            GameObject obj = Instantiate(prefabCombatShip);
            obj.transform.GetChild(0).GetComponent<Image>().sprite = blankAssest;
            obj.transform.GetChild(1).GetComponent<TMP_Text>().text = "No Ship Selected\nClick to Select a Ship";

            obj.GetComponent<Button>().onClick.AddListener(() => { B_AvailableCombatShips(blankAssest, null, null); });

            obj.transform.SetParent(fleetBaseMenu.transform.GetChild(2).GetChild(0));
            combatShips.Add(obj);

            currentCombatShips++;
        }
    }

    public void refreshActiveCrew() {
        Transform obj = fleetBaseMenu.transform.GetChild(5);
        InventoryCrew crew = Inventory.instance.crew.Find(x => x.active == true);

        if(crew == null) {
            obj.GetChild(0).gameObject.SetActive(false);
            obj.GetChild(1).gameObject.SetActive(true);
        }
        else {
            obj.GetChild(0).gameObject.SetActive(true);
            obj.GetChild(1).gameObject.SetActive(false);

            obj.GetChild(0).GetChild(0).GetComponent<Image>().sprite = crew.crewImage;

            //Contains a problem because the template is getting removed
            obj.GetChild(0).GetChild(1).GetComponent<TMP_Text>().text =
                crew.crewName + "\n" +
                Inventory.instance.crewTemplates.Find
                (x => x.crewName == crew.crewName).effectDescription;

            //obj.GetChild(0).GetChild(1).GetComponent<TMP_Text>().text =
            //    crew.crewName + "\n" +
            //    Inventory.instance.crew.Find
            //    (x => x.crewName == crew.crewName).effectText;
        }
    }

//Available Combat Ships
    public void B_AvailableCombatShips(Sprite shipImage, string shipInfo, string oldShipName) {
        fleetBaseMenu.SetActive(false);
        availableCombatShips.SetActive(true);

        Transform currentShip = availableCombatShips.transform.GetChild(2).GetChild(1);

        currentShip.GetChild(0).GetComponent<Image>().sprite = shipImage;
        currentShip.GetChild(1).GetComponent<TMP_Text>().text = shipInfo;

        if(oldShipName == null)
            availableCombatShips.transform.GetChild(2).GetChild(2).gameObject.SetActive(true);
        else
            availableCombatShips.transform.GetChild(2).GetChild(2).gameObject.SetActive(false);

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
        if(oldShipName != null)
            Inventory.instance.ships.Find(x => (x.GetShipName() == oldShipName && x.use == InventoryShip.USED_IN.combat)).use = InventoryShip.USED_IN.none;


        Inventory.instance.ships.Find(x => (x.GetShipName() == newShipName && x.use == InventoryShip.USED_IN.none)).use = InventoryShip.USED_IN.combat;




        B_FleetButton();
    }

//Crew
    public void B_CrewMenu(bool oneActive) {
        fleetBaseMenu.SetActive(false);
        crewMenu.SetActive(true);

        for(int i = crewList.Count - 1; i >= 0; i--) {
            GameObject obj = crewList[i];
            crewList.Remove(obj);
            Destroy(obj);
        }

        foreach(InventoryCrew crew in Inventory.instance.crew) {
            GameObject obj = Instantiate(prefabCrew);
            obj.transform.GetChild(0).GetComponent<Image>().sprite = crew.crewImage;
            obj.transform.GetChild(1).GetComponent<TMP_Text>().text =
                crew.crewName + "\n" + Inventory.instance.crewTemplates.Find(x => x.crewName == crew.crewName).effectDescription;

            obj.GetComponent<Button>().onClick.AddListener(() => { B_SelectCrew(crew.crewName, oneActive); });

            obj.transform.SetParent(crewMenu.transform.GetChild(2).GetChild(0));
            crewList.Add(obj);
        }
    }

    public void B_SelectCrew(string crewMemberName, bool oneActive) {
        if(oneActive)
            Inventory.instance.crew.Find(x => x.active == true).active = false;

        Inventory.instance.crew.Find(x => x.crewName == crewMemberName).active = true;

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

    public void updateActiveTradeShips() {
        for(int i = tradeShipList.Count - 1; i >= 0; i--) {
            GameObject obj = tradeShipList[i];
            tradeShipList.Remove(obj);
            Destroy(obj);
        }




        Transform parent = tradeOverview.transform.GetChild(4).GetChild(1).GetChild(0);
        List<TradeDeal> deals = TimedActivityManager.instance.tradeDeals;


        for(int i = 0; i < deals.Count; i++) {
            GameObject obj = Instantiate(prefabActiveTradeShips);
            obj.transform.GetChild(0).GetComponent<Image>().sprite = deals[i].shipInUse.shipImage;
            obj.transform.GetChild(1).GetComponent<TMP_Text>().text =
                "Time: " + Mathf.CeilToInt(deals[i].totalTime - deals[i].passedTime) + "" +
                "\nGain: " + deals[i].gainedResource.GetAmount() + " " + deals[i].gainedResource.GetName();

            obj.transform.SetParent(parent);
            tradeShipList.Add(obj);
        }


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
            //if(city.transform.GetComponent<Node>().start)
            //    continue;

            Image resourceIcon = city.gameObject.transform.GetChild(0).transform.GetChild(3).GetComponent<Image>();
            TMP_Text numberText = city.gameObject.transform.GetChild(0).transform.GetChild(4).GetComponent<TMP_Text>();

            int randomResoureID = Mathf.Clamp(UnityEngine.Random.Range(0, Inventory.instance.tradeResourceTemplates.Count), 0, 2);
            MainResources resource = Inventory.instance.tradeResourceTemplates[randomResoureID];

            int maxAdjust = Mathf.CeilToInt(resource.buyValue * 0.10f);
            int adjust = UnityEngine.Random.Range(-maxAdjust, maxAdjust);
            int buyValue = resource.buyValue + adjust;

            resourceIcon.sprite = resource.sprite;
            numberText.text = buyValue.ToString();

            if(adjust > maxAdjust / 2)
                numberText.color = Color.red;
            else if(adjust < -maxAdjust / 2)
                numberText.color = Color.green;
            else
                numberText.color = Color.yellow;

            GameObject tradePopUp = city.gameObject.transform.GetChild(0).gameObject;
            tradePopUp.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() => { 
                B_AvailableTradeShip(city, resource.resourceName, buyValue); 
            });
        }
    }

    public void B_BackToOverview() {
        tradeOverview.SetActive(true);
        resourceBarUI.SetActive(false);

        foreach(CityButtonScript city in FindObjectsOfType<CityButtonScript>()) {
            city.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    public void B_Buy() {
        tradeOverview.SetActive(false);
        resourceBarUI.SetActive(true);

        isBuy = true;

        foreach(CityButtonScript city in FindObjectsOfType<CityButtonScript>()) {
            city.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    public void B_Sell() {
        tradeOverview.SetActive(false);
        resourceBarUI.SetActive(true);

        isBuy = false;

        foreach(CityButtonScript city in FindObjectsOfType<CityButtonScript>()) {
            city.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    //Available Trade Ships
    public void refreshAvailableTradeShips() {
        int totalTime = 0;
        foreach(TimeQuery query in path) {
            totalTime += query.seconds;
        }


        for(int i = availableTradeShipList.Count - 1; i >= 0; i--) {
            GameObject obj = availableTradeShipList[i];
            availableTradeShipList.Remove(obj);
            Destroy(obj);
        }

        if(isBuy) {
            int tradeAmount = int.Parse(availableTradeShip.transform.GetChild(5).GetChild(1).GetComponent<TMP_InputField>().text);
            foreach(InventoryShip ship in Inventory.instance.ships) {
                if(ship.use == InventoryShip.USED_IN.none) {
                    int cost = potentialDeal.lostResource.GetAmount() * Mathf.Min(ship.maxCargo, tradeAmount);

                    GameObject obj = Instantiate(prefabAvailableTradeShip);
                    obj.transform.GetChild(0).GetComponent<Image>().sprite = ship.shipImage;
                    obj.transform.GetChild(1).GetComponent<TMP_Text>().text =
                        "Time: " + totalTime + "\nGain: " + Mathf.Min(ship.maxCargo, tradeAmount) + " " + potentialDeal.gainedResource.GetName() +
                        "\nCost: " + cost + " " + potentialDeal.lostResource.GetName();

                    obj.GetComponent<Button>().onClick.AddListener(() => { B_CreateDeal(ship, selectedCity); });

                    obj.transform.SetParent(availableTradeShip.transform.GetChild(2).GetChild(0));
                    availableTradeShipList.Add(obj);
                }
            }
        }
        else { // is Sell
            int tradeAmount = int.Parse(availableTradeShip.transform.GetChild(5).GetChild(1).GetComponent<TMP_InputField>().text);
            foreach(InventoryShip ship in Inventory.instance.ships) {
                if(ship.use == InventoryShip.USED_IN.none) {
                    int gain = potentialDeal.gainedResource.GetAmount() * Mathf.Min(ship.maxCargo, tradeAmount);

                    GameObject obj = Instantiate(prefabAvailableTradeShip);
                    obj.transform.GetChild(0).GetComponent<Image>().sprite = ship.shipImage;
                    obj.transform.GetChild(1).GetComponent<TMP_Text>().text =
                        "Time: " + totalTime + "\nGain: " + gain + " " + potentialDeal.gainedResource.GetName() +
                        "\nCost: " + -Mathf.Min(ship.maxCargo, tradeAmount) + " " + potentialDeal.lostResource.GetName();

                    obj.GetComponent<Button>().onClick.AddListener(() => { B_CreateDeal(ship, selectedCity); });

                    obj.transform.SetParent(availableTradeShip.transform.GetChild(2).GetChild(0));
                    availableTradeShipList.Add(obj);
                }
            }
        }
    }

    public void B_AvailableTradeShip(CityButtonScript city, string resourceName, int buyValue) {
        resourceBarUI.SetActive(false);
        availableTradeShip.SetActive(true);

        Resource tradeResource = Inventory.instance.resources.Find(x => x.GetName() == resourceName);
        tradeResource = new Resource(tradeResource.type, tradeResource.GetName(), 1, tradeResource.GetCost());
        Resource goldTemplate = Inventory.instance.resources.Find(x => x.GetName() == "Gold");
        goldTemplate = new Resource(goldTemplate.type, goldTemplate.GetName(), buyValue, goldTemplate.GetCost());

        if(isBuy) {
            potentialDeal = new TradeDeal(tradeResource, goldTemplate);
        }
        else { //is sell
            potentialDeal = new TradeDeal(goldTemplate, tradeResource);
        }

        selectedCity = city;
        path = Pathfinding.nodes.Find(x => x.start).getTradePath(city);

        I_ClampText();
    }

    public void B_CreateDeal(InventoryShip ship, CityButtonScript city) {
        int tradeAmount = Mathf.Min(ship.maxCargo, int.Parse(availableTradeShip.transform.GetChild(5).GetChild(1).GetComponent<TMP_InputField>().text));

        Resource temp = potentialDeal.gainedResource;
        Resource gained = new Resource(temp.type, temp.GetName(), temp.GetAmount() * tradeAmount, temp.GetCost());

        temp = potentialDeal.lostResource;
        Resource lost = new Resource(temp.type, temp.GetName(), -temp.GetAmount() * tradeAmount, temp.GetCost());

        ship.use = InventoryShip.USED_IN.trading;

        TradeDeal deal = new TradeDeal(gained, lost, ship, Pathfinding.nodes.Find(x => x.start).getTradePath(city));

        deal.activate();
        TimedActivityManager.instance.tradeDeals.Add(deal);

        B_TradingButton();
        foreach(CityButtonScript c in FindObjectsOfType<CityButtonScript>()) {
            c.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    public void B_AvailableShipExit() {
        B_TradingButton();
        foreach(CityButtonScript city in FindObjectsOfType<CityButtonScript>()) {
            city.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    public void I_ClampText() {
        maxCargo = 0;

        foreach(InventoryShip ship in Inventory.instance.ships) {
            if(ship.maxCargo > maxCargo && ship.use == InventoryShip.USED_IN.none)
                maxCargo = ship.maxCargo;
        }

        int amount = int.Parse(availableTradeShip.transform.GetChild(5).GetChild(1).GetComponent<TMP_InputField>().text);
        availableTradeShip.transform.GetChild(5).GetChild(1).GetComponent<TMP_InputField>().text = Mathf.Clamp(amount, 0, maxCargo).ToString();

        availableTradeShip.transform.GetChild(5).GetChild(2).GetComponent<TMP_Text>().text = 
            "Max Cargo of one of these ships is: " + maxCargo.ToString() + ".\nHow much do you want to Trade";

        refreshAvailableTradeShips();
    }

    /**************************************************************************************************************************
        OTHER UI 
    **************************************************************************************************************************/
    void Update() {
        updateResources();
        updateTradeResources();
        updateResourceBarUI();
        updateAvailableTradeShips();
        updateActiveTradeShips();

        timeSinceTradeTime += Time.deltaTime;
        if(timeSinceTradeTime >= refeshTradeTime && !resourceBarUI.activeInHierarchy) {
            populateBuyMenu();
            timeSinceTradeTime = 0;
        }
    }
}
