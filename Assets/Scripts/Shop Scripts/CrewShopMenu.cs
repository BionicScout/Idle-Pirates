using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CrewShopMenu : MonoBehaviour
{
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

    public List<InventoryCrew> GenerateCrew()
    {
        List<InventoryCrew> listOfCrew = new List<InventoryCrew>();

        for (int z = 0; z < shopItemLimit; z++)
        {
            InventoryCrew invCrew = new InventoryCrew(Inventory.instance.crewTemplates[z]);


            listOfCrew.Add(invCrew);
        }

        return listOfCrew;
    }


    public void SetValues(Inventory shopStock)
    {
        shopStock.crew = GenerateCrew();


        for (int i = 0; i < itemTitles.Count; i++)
        {
            
            itemTitles[i].text = shopStock.crew[i].crewName;
        }

        for (int i = 0; i < itemCostNumbers.Count; i++)
        {
            itemCostNumbers[i] = shopStock.crew[i].GetCost();
        }

        for (int i = 0; i < itemCostNumbertexts.Count; i++)
        {
            itemCostNumbertexts[i].text = itemCostNumbers[i].ToString();
        }

    }


    public void BuyCrewItemOne()
    {
        itemReference = 1;

       

        if (Inventory.instance.resources[0].GetAmount() >= itemCostNumbers[itemReference - 1])
        {
            AudioManager.instance.Play("Menu Sound");
            item1Button.gameObject.SetActive(false);
            Pay(itemReference);
            shopManager.BuyCrew(itemReference);

            
        }


    }


    public void BuyCrewItemTwo()
    {
        itemReference = 2;

        

        if (Inventory.instance.resources[0].GetAmount() >= itemCostNumbers[itemReference - 1])
        {
            AudioManager.instance.Play("Menu Sound");
            item2Button.gameObject.SetActive(false);
            Pay(itemReference);
            shopManager.BuyCrew(itemReference);
        }

    }


    public void BuyCrewItemThree()
    {
        itemReference = 3;

        

        if (Inventory.instance.resources[0].GetAmount() >= itemCostNumbers[itemReference - 1])
        {
            AudioManager.instance.Play("Menu Sound");
            item3Button.gameObject.SetActive(false);
            Pay(itemReference);
            shopManager.BuyCrew(itemReference);
        }

    }

    public void Pay(int index)
    {
        Inventory.instance.resources[0].SubtractAmount(itemCostNumbers[index - 1]);

    }

}
