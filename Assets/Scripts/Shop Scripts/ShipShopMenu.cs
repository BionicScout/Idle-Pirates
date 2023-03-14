using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShipShopMenu : MonoBehaviour
{
    [SerializeField]
    private ShopManager shopManager;


    [SerializeField]
    private List<TextMeshProUGUI> itemTitles = new List<TextMeshProUGUI>();


    public List<string> allItemMaterialTexts = new List<string>();

    [SerializeField]
    private List<string> item1MaterialTexts = new List<string>();

    [SerializeField]
    private List<string> item2MaterialTexts = new List<string>();

    [SerializeField]
    private List<string> item3MaterialTexts = new List<string>();

    [SerializeField]
    private List<int> item1MaterialAmounts = new List<int>();

    [SerializeField]
    private List<int> item2MaterialAmounts = new List<int>();

    [SerializeField]
    private List<int> item3MaterialAmounts = new List<int>();

    [SerializeField]
    private GameObject materialListParent;

    [SerializeField]
    private bool materialListOn = false;



    [SerializeField]
    private GameObject item1Button;
    [SerializeField]
    private GameObject item2Button;
    [SerializeField]
    private GameObject item3Button;



    //[SerializeField]
    //private GameObject item1MaterialListButton;

    //[SerializeField]
    //private GameObject item2MaterialListButton;

    //[SerializeField]
    //private GameObject item3MaterialListButton;

    [SerializeField]
    private int itemReference;

    [SerializeField]
    private TextMeshProUGUI itemMaterialListText;

    //[SerializeField]
    //private Inventory shopInventory;



    [SerializeField]
    private List<int> shipMatRequirementChecks = new List<int>();

    [SerializeField]
    private List<int> shipAmountRequirementChecks = new List<int>();

    [SerializeField]
    private bool payCheck;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetValues(Inventory shopStock)
    {
        for (int i = 0; i < itemTitles.Count; i++)
        {
            itemTitles[i].text = shopStock.ships[i].GetShipName();
        }

        for (int i = 0; i < allItemMaterialTexts.Count; i++)
        {
            for (int j = 0; j < shopStock.ships[i].resourcesNeeded.Count; j++)
            {
                //string mats = shopStock.ships[i].resourcesNeeded[j].Name()
                //    + ": " + shopStock.ships[i].resourcesNeeded[j].GetAmount().ToString()
                //    + "\n";


                //allItemMaterialTexts.Add(mats);


                //Each item has a sentence of all of the materials
                allItemMaterialTexts[i] +=
                    shopStock.ships[i].resourcesNeeded[j].Name()
                    + ": " + shopStock.ships[i].resourcesNeeded[j].GetAmount().ToString()
                    + "\n";
            }
        }



        //for seperate ships
        int k = 0;
        for (int l = 0; l < shopStock.ships[k].resourcesNeeded.Count; l++)
        {
            item1MaterialTexts.Add(shopStock.ships[k].resourcesNeeded[l].Name());
            item1MaterialAmounts.Add(shopStock.ships[k].resourcesNeeded[l].GetAmount());

        }
        k++;
        for (int l = 0; l < shopStock.ships[k].resourcesNeeded.Count; l++)
        {
            item2MaterialTexts.Add(shopStock.ships[k].resourcesNeeded[l].Name());
            item2MaterialAmounts.Add(shopStock.ships[k].resourcesNeeded[l].GetAmount());


        }
        k++;
        for (int l = 0; l < shopStock.ships[k].resourcesNeeded.Count; l++)
        {
            item3MaterialTexts.Add(shopStock.ships[k].resourcesNeeded[l].Name());
            item3MaterialAmounts.Add(shopStock.ships[k].resourcesNeeded[l].GetAmount());


        }

    }


    public void MaterialListMinimizeButtonPressed()
    {
        //Set window to false
        AudioManager.instance.Play("Menu Sound");
        itemMaterialListText.text = "";
        materialListOn = false;
        materialListParent.SetActive(false);
    }

    public void BuyShipItemOne()
    {
        itemReference = 1;


        for (int y = 0; y < item1MaterialTexts.Count; y++)
        {
            for (int x = 0; x < Inventory.instance.resources.Count; x++)
            {
                if (Inventory.instance.
                    resources[x].Name().Contains(item1MaterialTexts[y]) == true)
                {
                    shipMatRequirementChecks.Add(Convert.ToInt32(Inventory.instance.
                        resources[x].Name().Contains(item1MaterialTexts[y])));

                    if (Inventory.instance.resources[x].GetAmount() >= item1MaterialAmounts[y])
                    {
                        //shipAmountRequirementChecks.Add(
                        //    Inventory.instance.resources[x].GetAmount() - item1MaterialAmounts[y]);

                        shipAmountRequirementChecks.Add(1);
                    }
                }
            }

        }


        if (shipMatRequirementChecks.Count == item1MaterialTexts.Count &&
            shipAmountRequirementChecks.Count == item1MaterialAmounts.Count)
        {
            AudioManager.instance.Play("Menu Sound");
            item1Button.gameObject.SetActive(false);
            Pay(itemReference);
        }


        //if shipMatRequirementChecks have 2 trues, check of they have the right amount




        //shipMatRequirementChecks.Clear();

        //Make a list of bools and if they are all true, then pay for the ship

        //Make a list to check all of the materials needed to make ship in Inventory


        //Search the inventory if they have the materials for this item
        //through the specific list 



    }


    public void BuyShipItemTwo()
    {



    }


    public void BuyShipItemThree()
    {



    }


    public void Item1MaterialListButtonPressed()
    {
        if (materialListOn == false)
        {
            itemReference = 1;

            AudioManager.instance.Play("Menu Sound");
            materialListParent.SetActive(true);
            materialListOn = true;

            itemMaterialListText.text += allItemMaterialTexts[itemReference - 1];
        }


    }

    public void Item2MaterialListButtonPressed()
    {

        if (materialListOn == false)
        {
            itemReference = 2;
            AudioManager.instance.Play("Menu Sound");
            materialListParent.SetActive(true);
            materialListOn = true;
            itemMaterialListText.text += allItemMaterialTexts[itemReference - 1];
        }
    }

    public void Item3MaterialListButtonPressed()
    {
        if (materialListOn == false)
        {
            itemReference = 3;
            AudioManager.instance.Play("Menu Sound");
            materialListParent.SetActive(true);
            materialListOn = true;
            itemMaterialListText.text += allItemMaterialTexts[itemReference - 1];
        }

    }



    public void Pay(int index)
    {


    }



}
