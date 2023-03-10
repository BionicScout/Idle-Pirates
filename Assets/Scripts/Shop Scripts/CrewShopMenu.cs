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
            item3Button.gameObject.SetActive(false);
            Pay(itemReference);
            shopManager.BuyCrew(itemReference);
        }

    }

    public void Pay(int index)
    {
        Inventory.instance.resources[0].SubtractToPay(itemCostNumbers[index - 1]);

    }

}
