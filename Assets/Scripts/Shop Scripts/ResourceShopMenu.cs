using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceShopMenu : MonoBehaviour
{
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


    public void BuyOneShipResourceItemOne()
    {
        itemReference = 1;



        if (Inventory.instance.resources[0].GetAmount() >= itemCostNumbers[itemReference - 1])
        {
            AudioManager.instance.Play("Menu Sound");
            //item1Button.gameObject.SetActive(false);
            Pay(itemReference);
            shopManager.BuyResource(itemReference);
            itemAmountNumbers[itemReference - 1] -= 1;

        }


    }


    public void BuyOneShipResourceItemTwo()
    {
        itemReference = 2;



        if (Inventory.instance.resources[0].GetAmount() >= itemCostNumbers[itemReference - 1])
        {

        }


    }


    public void BuyOneShipResourceItemThree()
    {
        itemReference = 3;



        if (Inventory.instance.resources[0].GetAmount() >= itemCostNumbers[itemReference - 1])
        {

        }


    }

    public void Pay(int index)
    {
        Inventory.instance.resources[0].SubtractAmount(itemCostNumbers[index - 1]);

    }

}
