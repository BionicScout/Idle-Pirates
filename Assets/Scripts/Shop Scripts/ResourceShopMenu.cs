using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceShopMenu : MonoBehaviour {
    [SerializeField]
    private ShopManager shopManager;


    [SerializeField]
    private List<TextMeshProUGUI> itemTitles = new List<TextMeshProUGUI>();

    [SerializeField]
    private List<TextMeshProUGUI> itemAmountNumbertexts = new List<TextMeshProUGUI>();

    [SerializeField]
    private List<int> itemAmountNumbers = new List<int>();

    [SerializeField]
    private List<TextMeshProUGUI> itemCostNumbertexts = new List<TextMeshProUGUI>();

    [SerializeField]
    private List<int> itemCostNumbers = new List<int>();

    [SerializeField]
    private int itemReference;

    [SerializeField]
    private GameObject item1Button;
    [SerializeField]
    private GameObject item2Button;
    [SerializeField]
    private GameObject item3Button;


    [SerializeField]
    private int shopItemLimit = 3;

    public List<Resource> GenerateResources() {
        List<Resource> listOfResources = new List<Resource>();

        for(int z = 0; z < shopItemLimit; z++) {
            Resource invResource = new Resource(Inventory.instance.shipBuildResourceTemplates[z]);


            listOfResources.Add(invResource);
        }

        return listOfResources;
    }


    public void SetValues(ShopInventory shopStock) {

        shopStock.resourceStock = GenerateResources();


        for(int i = 0; i < itemTitles.Count; i++) {
            itemTitles[i].text = shopStock.resourceStock[i].GetName();
        }

        for(int i = 0; i < itemAmountNumbers.Count; i++) {
            itemAmountNumbers[i] = shopStock.resourceStock[i].GetAmount();
        }

        for(int i = 0; i < itemAmountNumbertexts.Count; i++) {
            itemAmountNumbertexts[i].text = itemAmountNumbers[i].ToString();
        }

        for(int i = 0; i < itemCostNumbers.Count; i++) {
            itemCostNumbers[i] = shopStock.resourceStock[i].GetCost();

            if (Inventory.instance.crew.Find(x => x.active).crewName == "Joe")
            {
                itemCostNumbers[i] -= Mathf.CeilToInt(shopStock.resourceStock[i].GetCost() * .1f);
            }
        }

        for(int i = 0; i < itemCostNumbertexts.Count; i++) {
            itemCostNumbertexts[i].text = itemCostNumbers[i].ToString();
        }

    }


    public void BuyShipResourceItemOne() {
        itemReference = 1;



        if(Inventory.instance.resources[0].GetAmount() >= itemCostNumbers[itemReference - 1]) 
        {
            AudioManager.instance.Play("Purchase Sound");
            item1Button.gameObject.SetActive(false);
            Pay(itemReference);
            shopManager.BuyResource(itemReference);
            itemAmountNumbers[itemReference - 1] -= 1;

        }
        else
        {
            AudioManager.instance.Play("Error");
        }

    }


    public void BuyShipResourceItemTwo() {
        itemReference = 2;



        if(Inventory.instance.resources[0].GetAmount() >= itemCostNumbers[itemReference - 1]) 
        {
            AudioManager.instance.Play("Purchase Sound");
            item2Button.gameObject.SetActive(false);

            Pay(itemReference);
            shopManager.BuyResource(itemReference);
            itemAmountNumbers[itemReference - 1] -= 1;
        }
        else
        {
            AudioManager.instance.Play("Error");
        }

    }


    public void BuyShipResourceItemThree() {
        itemReference = 3;



        if(Inventory.instance.resources[0].GetAmount() >= itemCostNumbers[itemReference - 1]) 
        {
            AudioManager.instance.Play("Purchase Sound");
            item3Button.gameObject.SetActive(false);
            Pay(itemReference);
            shopManager.BuyResource(itemReference);
            itemAmountNumbers[itemReference - 1] -= 1;
        }
        else
        {
            AudioManager.instance.Play("Error");
        }


    }

    public void Pay(int index) {
        Inventory.instance.resources[0].SubtractAmount(itemCostNumbers[index - 1]);

    }

}