using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceShopMenu : MonoBehaviour
{

    public List<TextMeshProUGUI> itemTitles = new List<TextMeshProUGUI>();
    public List<TextMeshProUGUI> itemAmountNumbertexts = new List<TextMeshProUGUI>();
    public List<int> itemAmountNumbers = new List<int>();
    public List<TextMeshProUGUI> itemCostNumbertexts = new List<TextMeshProUGUI>();
    public List<int> itemCostNumbers = new List<int>();




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
            itemTitles[i].text = shopStock.resources[i].Name();
        }

        for (int i = 0; i < itemAmountNumbers.Count; i++)
        {
            itemAmountNumbers[i] = shopStock.resources[i].GetAmount();
        }

        for (int i = 0; i < itemAmountNumbertexts.Count; i++)
        {
            itemAmountNumbertexts[i].text = itemAmountNumbers[i].ToString();
        }

        for (int i = 0; i < itemCostNumbers.Count; i++)
        {
            itemCostNumbers[i] = shopStock.resources[i].GetCost();
        }

        for (int i = 0; i < itemCostNumbertexts.Count; i++)
        {
            itemCostNumbertexts[i].text = itemCostNumbers[i].ToString();
        }

    }


    public void BuyOneItem()
    {

    }

    public void BuyAllItems()
    {



    }



}
