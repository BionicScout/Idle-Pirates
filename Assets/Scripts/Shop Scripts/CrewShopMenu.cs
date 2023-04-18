using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class CrewShopMenu : MonoBehaviour
{
    //Dummy Crew Member Object
    [SerializeField]
    private MainCrewMembers dummyCrew;

    [SerializeField]
    private ShopManager shopManager;

    [SerializeField]
    private List<TextMeshProUGUI> itemTitles = new List<TextMeshProUGUI>();

    [SerializeField]
    private List<TextMeshProUGUI> itemCostNumbertexts = new List<TextMeshProUGUI>();

    [SerializeField]
    private List<int> itemCostNumbers = new List<int>();

    [SerializeField]
    private int goldResourceIndex;

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


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GetRidOfDupeCrew()
    {
        for(int x = 0; x < Inventory.instance.crew.Count; x++) 
        {
            InventoryCrew tempCrew = Inventory.instance.crew[x];

            for (int y = 0; y < Inventory.instance.crewTemplates.Count; y++)
            {
                if (Inventory.instance.crewTemplates[y].crewName.Contains(tempCrew.crewName))
                {
                    Inventory.instance.crewTemplates.RemoveAt(y);

                }
            }

            //if there are less than 3 crew templates in inventory,
            //get rid of other buttons
        }
    }

    public List<InventoryCrew> GenerateCrew()
    {
        List<InventoryCrew> listOfCrew = new List<InventoryCrew>();


        //Important for saving inventory 
        List<MainCrewMembers> crewInventory = Inventory.instance.crewTemplates.ToList();


        //Maybe if there are less than 3 crew members in inventory,
        //create dummy objects in list
        while (crewInventory.Count < 3)
        {
            crewInventory.Add(dummyCrew);
            //InventoryCrew invCrew
               //= new InventoryCrew(dummyCrew);
            //crewInventory.Add(invCrew);
        }

        for (int z = 0; z < shopItemLimit; z++)
        {
            int randomPosition
                = Random.Range(0, crewInventory.Count - 1);

            InventoryCrew invCrew 
                = new InventoryCrew(crewInventory[randomPosition]);

            /*
            //while (listOfCrew.Contains(invCrew) == true)
            //{
            //    randomPosition
            //    = Random.Range(0, Inventory.instance.shipTemplates.Count);

            //    invCrew
            //        = new InventoryCrew(Inventory.instance.crewTemplates[randomPosition]);

            //}

            //if (listOfCrew.Contains(invCrew) == false)
            //{
            //    listOfCrew.Add(invCrew);
            //}
            */

            listOfCrew.Add(invCrew);
            crewInventory.Remove(crewInventory[randomPosition]);
        }

        //Maybe if there are less than 3 crew members in inventory,
        //create dummy objects in list

        //while (listOfCrew.Count < 3)
        //{

        //    InventoryCrew invCrew
        //        = new InventoryCrew(dummyCrew);
        //    listOfCrew.Add(invCrew);
        //}

        //if(listOfCrew.Count < 3)
        //{
            

        //}

        return listOfCrew;
    }


    public void SetValues(ShopInventory shopStock)
    {
        //Check if they have the crew member
        //Make function that goes through inventory and gets rid of template
        //that corresponds to inventory

        GetRidOfDupeCrew();

        shopStock.crewStock = GenerateCrew();


        for (int i = 0; i < itemTitles.Count; i++)
        {
            
            itemTitles[i].text = shopStock.crewStock[i].crewName;
        }

        for (int i = 0; i < itemCostNumbers.Count; i++)
        {
            itemCostNumbers[i] = shopStock.crewStock[i].GetCost();
        }

        for (int i = 0; i < itemCostNumbertexts.Count; i++)
        {
            itemCostNumbertexts[i].text = itemCostNumbers[i].ToString();
        }

    }


    public void BuyCrewItemOne()
    {
        itemReference = 1;

        if (itemTitles[itemReference - 1].text == "Scam")
        {
            AudioManager.instance.Play("Error");
        }

        else
        {

            if (Inventory.instance.resources[0].GetAmount() >= itemCostNumbers[itemReference - 1])
            {
                AudioManager.instance.Play("Purchase Sound");
                item1Button.gameObject.SetActive(false);
                Pay(itemReference);
                shopManager.BuyCrew(itemReference);


            }
            else
            {
                AudioManager.instance.Play("Error");
            }
        }
    }


    public void BuyCrewItemTwo()
    {
        itemReference = 2;

        if (itemTitles[itemReference - 1].text == "Scam")
        {
            AudioManager.instance.Play("Error");
        }

        else
        {

            if (Inventory.instance.resources[0].GetAmount() >= itemCostNumbers[itemReference - 1])
            {
                AudioManager.instance.Play("Purchase Sound");
                item2Button.gameObject.SetActive(false);
                Pay(itemReference);
                shopManager.BuyCrew(itemReference);
            }
            else
            {
                AudioManager.instance.Play("Error");
            }
        }
    }


    public void BuyCrewItemThree()
    {
        itemReference = 3;

        if (itemTitles[itemReference - 1].text == "Scam")
        {
            AudioManager.instance.Play("Error");
        }

        else
        {

            if (Inventory.instance.resources[0].GetAmount() >= itemCostNumbers[itemReference - 1])
            {
                AudioManager.instance.Play("Purchase Sound");
                item3Button.gameObject.SetActive(false);
                Pay(itemReference);
                shopManager.BuyCrew(itemReference);
            }
            else
            {
                AudioManager.instance.Play("Error");
            }
        }
    }

    public void Pay(int index)
    {
        Inventory.instance.resources[0].SubtractAmount(itemCostNumbers[index - 1]);

    }

}
