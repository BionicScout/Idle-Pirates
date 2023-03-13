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


    public List<string> itemMaterialTexts = new List<string>();

    //[SerializeField]
    //private List<string> item2MaterialTexts = new List<string>();

    //[SerializeField]
    //private List<string> item3MaterialTexts = new List<string>();

    [SerializeField]
    private GameObject materialListParent;

    [SerializeField]
    private bool materialListOn = false;

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



   // Start is called before the first frame update
   void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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

            itemMaterialListText.text += itemMaterialTexts[itemReference - 1];
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
            itemMaterialListText.text += itemMaterialTexts[itemReference - 1];
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
            itemMaterialListText.text += itemMaterialTexts[itemReference - 1];
        }

    }


    public void SetValues(Inventory shopStock)
    {
        for (int i = 0; i < itemTitles.Count; i++)
        {
            itemTitles[i].text = shopStock.ships[i].GetShipName();
        }

        for (int i = 0; i < itemMaterialTexts.Count; i++) 
        {
            for (int j = 0; j < shopStock.ships[i].resourcesNeeded.Count; j++)
            {
                //itemMaterialTexts.Add(shopStock.ships[i].resourcesNeeded[j].Name()
                //    + ": " + shopStock.ships[i].resourcesNeeded[j].GetAmount().ToString()
                //    + "\n");
                itemMaterialTexts[i] +=
                    shopStock.ships[i].resourcesNeeded[j].Name()
                    + ": " + shopStock.ships[i].resourcesNeeded[j].GetAmount().ToString() 
                    + "\n";
            }
        }


    }

}
