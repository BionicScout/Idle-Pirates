using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using Unity.VisualScripting;
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
    private List<int> shipMatAmountRequirementChecks = new List<int>();

    [SerializeField]
    private List<int> shipMatIndex = new List<int>();

    [SerializeField]
    private bool payCheck;

    [SerializeField]
    private int shopItemLimit = 3;



    public List<InventoryShip> GenerateShips() 
    {
       List<InventoryShip> listOfShips = new List<InventoryShip>();

        for (int z = 0; z < shopItemLimit; z++)
        {
            /*
            //Pick Random ship for Inventory.instance.shipTemplates
            //Turn template into InventoryShip
            */

            /*
            //Debug.Log(z + " + " + Inventory.instance.shipTemplates[z]);

            //for (int x = 0; x < Inventory.instance.shipTemplates[z].resourcesNeeded.Count; x++)
            //{
            //    Debug.Log(z + " + " + 
            //        Inventory.instance.shipTemplates[z].resourcesNeeded[x].GetName());
            //    Debug.Log(z + " + " + 
            //        Inventory.instance.shipTemplates[z].resourcesNeeded[x].GetAmount());
            //    Debug.Log(z + " + " + 
            //        Inventory.instance.shipTemplates[z].resourcesNeeded[x].GetCost());
            //    Debug.Log(z + " + " + 
            //        Inventory.instance.shipTemplates[z].resourcesNeeded[x].GetResourceType());
            //}

            

            //MainShips templateShip = Inventory.instance.shipTemplates[z];

            //Debug.Log(templateShip.name);

            //Debug.Log(templateShip.resourcesNeeded[z].GetName()
            //       + " + " + templateShip.resourcesNeeded[z].GetAmount());

            
            //Debug.Log(templateShip.name);



            */


            int randomPosition 
                = UnityEngine.Random.Range(0, Inventory.instance.shipTemplates.Count);


            


            InventoryShip invShip 
                = new InventoryShip(Inventory.instance.shipTemplates[randomPosition]);


            //Check to see if ship is already inside of the shopList
            //for(int y = 0; y < listOfShips.Count; y++)
            //{
            //    //Check the name of ship to see if the name is in there

            //    listOfShips.Find(invShip);


            //}


            while (listOfShips.Contains(invShip) == true)
            {
                randomPosition
                = UnityEngine.Random.Range(0, Inventory.instance.shipTemplates.Count);

                invShip
                    = new InventoryShip(Inventory.instance.shipTemplates[randomPosition]);

            }

            if(listOfShips.Contains(invShip) == false)
            {
                listOfShips.Add(invShip);
            }


            
        }

        return listOfShips;
    }

    public void SetValues(ShopInventory shopStock)
    {
       

        shopStock.shipStock = GenerateShips();


        for (int i = 0; i < itemTitles.Count; i++)
        {
            itemTitles[i].text = shopStock.shipStock[i].GetShipName();
        }

        for (int i = 0; i < allItemMaterialTexts.Count; i++)
        {
            for (int j = 0; j < shopStock.shipStock[i].resourcesNeeded.Count; j++)
            {
                //string mats = shopStock.shipStock[i].resourcesNeeded[j].GetName()
                //    + ": " + shopStock.shipStock[i].resourcesNeeded[j].GetAmount().ToString()
                //    + "\n";


                //allItemMaterialTexts.Add(mats);


                //Each item has a sentence of all of the materials
                allItemMaterialTexts[i] +=
                    shopStock.shipStock[i].resourcesNeeded[j].GetName()
                    + ": " + shopStock.shipStock[i].resourcesNeeded[j].GetAmount().ToString()
                    + "\n";
            }
        }



        //for seperate shipStock
        int k = 0;
        for (int l = 0; l < shopStock.shipStock[k].resourcesNeeded.Count; l++)
        {
            item1MaterialTexts.Add(shopStock.shipStock[k].resourcesNeeded[l].GetName());
            item1MaterialAmounts.Add(shopStock.shipStock[k].resourcesNeeded[l].GetAmount());

        }
        k++;
        for (int l = 0; l < shopStock.shipStock[k].resourcesNeeded.Count; l++)
        {
            item2MaterialTexts.Add(shopStock.shipStock[k].resourcesNeeded[l].GetName());
            item2MaterialAmounts.Add(shopStock.shipStock[k].resourcesNeeded[l].GetAmount());


        }
        k++;
        for (int l = 0; l < shopStock.shipStock[k].resourcesNeeded.Count; l++)
        {
            item3MaterialTexts.Add(shopStock.shipStock[k].resourcesNeeded[l].GetName());
            item3MaterialAmounts.Add(shopStock.shipStock[k].resourcesNeeded[l].GetAmount());


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
        if (materialListOn == false)
        {
            shipMatAmountRequirementChecks.Clear();
            shipMatRequirementChecks.Clear();
            shipMatIndex.Clear();

            itemReference = 1;

            //Get index of resourceStock in inventory for payment progress
            //Make list for that


            for (int y = 0; y < item1MaterialTexts.Count; y++)
            {
                for (int x = 0; x < Inventory.instance.resources.Count; x++)
                {
                    if (Inventory.instance.
                        resources[x].GetName().Contains(item1MaterialTexts[y]) == true)
                    {
                        shipMatRequirementChecks.Add(Convert.ToInt32(Inventory.instance.
                            resources[x].GetName().Contains(item1MaterialTexts[y])));

                        shipMatIndex.Add(x);


                        if (Inventory.instance.resources[x].GetAmount() >= item1MaterialAmounts[y])
                        {
                            //shipAmountRequirementChecks.Add(
                            //    Inventory.instance.resourceStock[x].GetAmount() - item1MaterialAmounts[y]);

                            shipMatAmountRequirementChecks.Add(1);
                        }
                    }
                }

            }


            if (shipMatRequirementChecks.Count == item1MaterialTexts.Count &&
                shipMatAmountRequirementChecks.Count == item1MaterialAmounts.Count)
            {
                AudioManager.instance.Play("Menu Sound");
                item1Button.gameObject.SetActive(false);

                for (int i = 0; i < item1MaterialAmounts.Count; i++)
                {
                    //Maybe make a list of integers containing the indexes 
                    //of each material needed

                    Inventory.instance.resources[shipMatIndex[i]]
                        .SubtractAmount(item1MaterialAmounts[i]);
                }

                shopManager.BuyShip(itemReference);

            }

            /*
            //if shipMatRequirementChecks have 2 trues, check of they have the right amount






            //Make a list of bools and if they are all true, then pay for the ship

            //Make a list to check all of the materials needed to make ship in Inventory


            //Search the inventory if they have the materials for this item
            //through the specific list 

            */
        }
    }


    public void BuyShipItemTwo()
    {
        if (materialListOn == false)
        {
            shipMatRequirementChecks.Clear();
            shipMatAmountRequirementChecks.Clear();
            shipMatIndex.Clear();

            itemReference = 2;

            for (int y = 0; y < item2MaterialTexts.Count; y++)
            {
                for (int x = 0; x < Inventory.instance.resources.Count; x++)
                {
                    if (Inventory.instance.
                        resources[x].GetName().Contains(item2MaterialTexts[y]) == true)
                    {
                        shipMatRequirementChecks.Add(Convert.ToInt32(Inventory.instance.
                            resources[x].GetName().Contains(item2MaterialTexts[y])));

                        shipMatIndex.Add(x);

                        if (Inventory.instance.resources[x].GetAmount() >= item2MaterialAmounts[y])
                        {
                            //shipAmountRequirementChecks.Add(
                            //    Inventory.instance.resourceStock[x].GetAmount() - item1MaterialAmounts[y]);

                            shipMatAmountRequirementChecks.Add(1);
                        }
                    }
                }

            }


            if (shipMatRequirementChecks.Count == item2MaterialTexts.Count &&
                shipMatAmountRequirementChecks.Count == item2MaterialAmounts.Count)
            {
                AudioManager.instance.Play("Menu Sound");
                item2Button.gameObject.SetActive(false);

                for (int i = 0; i < item2MaterialAmounts.Count; i++)
                {
                    Inventory.instance.resources[shipMatIndex[i]]
                        .SubtractAmount(item2MaterialAmounts[i]);
                }

                shopManager.BuyShip(itemReference);

            }
        }
    }


    public void BuyShipItemThree()
    {
        if (materialListOn == false)
        {
            shipMatRequirementChecks.Clear();
            shipMatAmountRequirementChecks.Clear();
            shipMatIndex.Clear();

            itemReference = 3;

            for (int y = 0; y < item3MaterialTexts.Count; y++)
            {
                for (int x = 0; x < Inventory.instance.resources.Count; x++)
                {
                    if (Inventory.instance.
                        resources[x].GetName().Contains(item3MaterialTexts[y]) == true)
                    {
                        shipMatRequirementChecks.Add(Convert.ToInt32(Inventory.instance.
                            resources[x].GetName().Contains(item3MaterialTexts[y])));

                        shipMatIndex.Add(x);

                        if (Inventory.instance.resources[x].GetAmount() >= item3MaterialAmounts[y])
                        {
                            //shipAmountRequirementChecks.Add(
                            //    Inventory.instance.resourceStock[x].GetAmount() - item1MaterialAmounts[y]);

                            shipMatAmountRequirementChecks.Add(1);
                        }
                    }
                }

            }


            if (shipMatRequirementChecks.Count == item3MaterialTexts.Count &&
                shipMatAmountRequirementChecks.Count == item3MaterialAmounts.Count)
            {
                AudioManager.instance.Play("Menu Sound");
                item3Button.gameObject.SetActive(false);

                for (int i = 0; i < item3MaterialAmounts.Count; i++)
                {
                    Inventory.instance.resources[shipMatIndex[i]]
                        .SubtractAmount(item3MaterialAmounts[i]);
                }

                shopManager.BuyShip(itemReference);

            }

        }
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




}
