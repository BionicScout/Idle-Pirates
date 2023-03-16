using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryUI : MonoBehaviour {
    public GameObject menuButton;
    public GameObject inventoryMenu;

    bool update = false;

   public void toInventory() {
        updateUI();

        menuButton.SetActive(false);
        inventoryMenu.SetActive(true);
        update = true;
   }

   public void outOfInventory() {
        menuButton.SetActive(true);
        inventoryMenu.SetActive(false);
        update = false;
    }

    public void updateUI() {
        //Gold
        Resource gold = Inventory.instance.resources.Find(x => x.GetName() == "Gold");
        inventoryMenu.transform.GetChild(1).GetChild(1).GetComponent<TMP_Text>().text = gold.GetName() + ": " + gold.GetAmount();

        //Ship Buy
        List<Resource> shipBuy = Inventory.instance.resources.FindAll(x => x.type == Resource.Type.Ship_Build);
        inventoryMenu.transform.GetChild(2).GetChild(3).GetComponent<TMP_Text>().text =
            shipBuy[0].GetName() + ": " + shipBuy[0].GetAmount() + "\n" +
            shipBuy[1].GetName() + ": " + shipBuy[1].GetAmount() + "\n" +
            shipBuy[2].GetName() + ": " + shipBuy[2].GetAmount();

        //Trade
        List<Resource> trade = Inventory.instance.resources.FindAll(x => x.type == Resource.Type.Trade);
        inventoryMenu.transform.GetChild(3).GetChild(3).GetComponent<TMP_Text>().text =
            trade[0].GetName() + ": " + trade[0].GetAmount() + "\n" +
            trade[1].GetName() + ": " + trade[1].GetAmount() + "\n" +
            trade[2].GetName() + ": " + trade[2].GetAmount();
    }

    private void Update() {
        //while(update)
        //    updateUI();
    }
}
