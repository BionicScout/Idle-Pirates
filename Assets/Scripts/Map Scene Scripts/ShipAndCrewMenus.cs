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

    public void toNoMenu() {
        shipCrewButton.SetActive(true);
        combatShipsMenu.SetActive(false);
    }

    public void toCombatShipMenu() {
        shipCrewButton.SetActive(false);
        combatShipsMenu.SetActive(true);
        availableShipMenu.SetActive(false);

    //Update Ship
        int uiListIndex = 0;
        foreach(InventoryShip ship in Inventory.instance.ships) {
            if(ship.use == InventoryShip.USED_IN.combat) {
                Debug.Log(ship.GetShipName());
                MainShips info = Inventory.instance.shipTemplates.Find(x => x.name == ship.GetShipName());

                GameObject shipInfoUI = combatShip_UIList[uiListIndex];
                shipInfoUI.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = info.shipImage;

                shipInfoUI.transform.GetChild(1).GetComponent<TMP_Text>().text = 
                    info.name + "\nHealth: " + info.health + "\nSpeed: " + info.speed + "\nAttack: " + info.attack;

                uiListIndex++;
            }
        }
    }


    /*
     *      BUG: When selecting lower item on list, does not always switch ships
     */
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
        Inventory.instance.ships.Find(x => x.GetShipName() == name).use = InventoryShip.USED_IN.combat;

        toCombatShipMenu();
    }
}
