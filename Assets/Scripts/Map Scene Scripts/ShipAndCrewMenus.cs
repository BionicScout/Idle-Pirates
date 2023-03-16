using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShipAndCrewMenus : MonoBehaviour {
    [Header("Button")]
    public GameObject shipCrewButton;

    [Header("Ship Menus")]
    public GameObject combatShipsMenu;
    public List<GameObject> combatShip_UIList;

    public GameObject availableShipMenu;
    public GameObject availableShipInfoPrefab;
    List<GameObject> listOfAvaliableShips = new List<GameObject>();
    int selectedCombatShipIndex;

    [Header("Crew Menus")]
    public GameObject activeCrewMenu;
    public GameObject activeCrew_UI;

    public GameObject availableCrewMenu;
    public GameObject availableCrewInfoPrefab;
    List<GameObject> listOfAvaliableCrew = new List<GameObject>();

    public void toNoMenu() {
        shipCrewButton.SetActive(true);
        combatShipsMenu.SetActive(false);
    }

    public void toCombatShipMenu() {
        shipCrewButton.SetActive(false);
        combatShipsMenu.SetActive(true);
        availableShipMenu.SetActive(false);
        activeCrewMenu.SetActive(false);

        //Update Ship
        int uiListIndex = 0;
        foreach(InventoryShip ship in Inventory.instance.ships) {
            if(ship.use == InventoryShip.USED_IN.combat) {
                MainShips info = Inventory.instance.shipTemplates.Find(x => x.name == ship.GetShipName());

                GameObject shipInfoUI = combatShip_UIList[uiListIndex];
                shipInfoUI.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = info.shipImage;

                shipInfoUI.transform.GetChild(1).GetComponent<TMP_Text>().text = 
                    info.name + "\nHealth: " + info.health + "\nSpeed: " + info.speed + "\nAttack: " + info.attack;

                uiListIndex++;
            }
        }
    }

    public void toAvailableShips(GameObject shipInfoUI) {
        combatShipsMenu.SetActive(false);
        availableShipMenu.SetActive(true);

    //Get INdex
        for(int i = 0; i < combatShip_UIList.Count; i++) {
            if(combatShip_UIList[i] == shipInfoUI) {
                selectedCombatShipIndex = i;
                break;
            }
        }

    //Delete Old ships
        foreach(GameObject obj in listOfAvaliableShips)
            Destroy(obj);

        //Add Currently Selected Ship
        GameObject scrollObject = availableShipMenu.transform.GetChild(1).GetChild(0).gameObject;

        Sprite selectedShipSprite = shipInfoUI.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite;

        if(shipInfoUI.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite != null) {
            GameObject infoBlock = Instantiate(availableShipInfoPrefab);
            MainShips info = Inventory.instance.shipTemplates.Find(x => x.shipImage == selectedShipSprite);

            infoBlock.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = info.shipImage;
            infoBlock.transform.GetChild(0).GetChild(0).GetComponent<Button>().onClick.AddListener(() => { selectNewCombatShip(infoBlock); });
            infoBlock.transform.GetChild(1).GetComponent<TMP_Text>().text =
                    info.name + "\nHealth: " + info.health + "\nSpeed: " + info.speed + "\nAttack: " + info.attack;

            infoBlock.transform.SetParent(scrollObject.transform);
            listOfAvaliableShips.Add(infoBlock);
        }

        //Add LAll Avaliable Ships
        foreach(InventoryShip ship in Inventory.instance.ships) {
            if(ship.use != InventoryShip.USED_IN.none)
                continue;

            GameObject infoBlock = Instantiate(availableShipInfoPrefab);
            MainShips info = Inventory.instance.shipTemplates.Find(x => x.name == ship.GetShipName());

            infoBlock.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = info.shipImage;
            infoBlock.transform.GetChild(0).GetChild(0).GetComponent<Button>().onClick.AddListener(() => { selectNewCombatShip(infoBlock); });
            infoBlock.transform.GetChild(1).GetComponent<TMP_Text>().text =
                    info.name + "\nHealth: " + info.health + "\nSpeed: " + info.speed + "\nAttack: " + info.attack;

            infoBlock.transform.SetParent(scrollObject.transform);
            listOfAvaliableShips.Add(infoBlock);
        }
    }

    public void selectNewCombatShip(GameObject shipInfoUI) {
    //Remove Selected Combat SHip
        int uiListIndex = 0;
        foreach(InventoryShip ship in Inventory.instance.ships) {
            if(ship.use == InventoryShip.USED_IN.combat) {
                if(selectedCombatShipIndex == uiListIndex) {
                    ship.use = InventoryShip.USED_IN.none;
                }
                uiListIndex++;
            }
        }

    //Add New Ship
        Sprite selectedShipSprite = shipInfoUI.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite;
        string name = Inventory.instance.shipTemplates.Find(x => x.shipImage == selectedShipSprite).shipName;

        foreach(InventoryShip ship in Inventory.instance.ships) {
            if(ship.use == InventoryShip.USED_IN.none && ship.GetShipName() == name) {
                ship.use = InventoryShip.USED_IN.combat;
                break;
            }
        }

        toCombatShipMenu();
    }

    public void toActiveCrew() {
        combatShipsMenu.SetActive(false);
        activeCrewMenu.SetActive(true);
        availableCrewMenu.SetActive(false);

        MainCrewMembers info = null;
        foreach(InventoryCrew crew in Inventory.instance.crew) {
            if(crew.active) {
                info = Inventory.instance.crewTemplates.Find(x => x.crewName == crew.crewName);
                break;
            }
        }

        //activeCrew_UI.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = info.shipImage;
        activeCrew_UI.transform.GetChild(1).GetComponent<TMP_Text>().text = info.crewName + "\nEffect: " + info.effectDescription;
    }

    public void toAvaliableCrew() {
        activeCrewMenu.SetActive(false);
        availableCrewMenu.SetActive(true);

        //Destroy List
        foreach(GameObject obj in listOfAvaliableCrew)
            Destroy(obj);

        //Create New List
        GameObject scrollObject = availableCrewMenu.transform.GetChild(1).GetChild(0).gameObject;
        foreach(InventoryCrew crew in Inventory.instance.crew) {
            GameObject infoBlock = Instantiate(availableCrewInfoPrefab);
            MainCrewMembers info = Inventory.instance.crewTemplates.Find(x => x.crewName == crew.crewName);

            //infoBlock.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = info.crewImage;
            infoBlock.transform.GetChild(0).GetChild(0).GetComponent<Button>().onClick.AddListener(() => { selectNewCrew(infoBlock); });
            infoBlock.transform.GetChild(1).GetComponent<TMP_Text>().text = info.crewName + "\nEffect: " + info.effectDescription;

            infoBlock.transform.SetParent(scrollObject.transform);
            listOfAvaliableCrew.Add(infoBlock);
        }

    }

    public void selectNewCrew(GameObject crewInfoUI) {
        foreach(InventoryCrew crew in Inventory.instance.crew) {
            if(crew.active)
                crew.active = false;
        }

        string text = crewInfoUI.transform.GetChild(1).GetComponent<TMP_Text>().text;
        text = text.Substring(0, text.IndexOf("\n"));
        Inventory.instance.crew.Find(x => x.crewName == text).active = true;

        toActiveCrew();
    }
}
