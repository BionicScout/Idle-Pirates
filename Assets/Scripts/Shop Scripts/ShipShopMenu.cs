using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShipShopMenu : MonoBehaviour
{
    [SerializeField]
    private List<TextMeshProUGUI> itemTitles = new List<TextMeshProUGUI>();


    public List<string> itemMaterialTexts = new List<string>();

    //[SerializeField]
    //private List<string> item2MaterialTexts = new List<string>();

    //[SerializeField]
    //private List<string> item3MaterialTexts = new List<string>();

    [SerializeField]
    private GameObject materialListParent;

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
        itemReference = 1;

        AudioManager.instance.Play("Menu Sound");
        materialListParent.SetActive(true);


        itemMaterialListText.text += itemMaterialTexts[itemReference - 1];
        


    }

    public void Item2MaterialListButtonPressed()
    {
        itemReference = 2;
        AudioManager.instance.Play("Menu Sound");
        materialListParent.SetActive(true);

        itemMaterialListText.text += itemMaterialTexts[itemReference - 1];
    }

    public void Item3MaterialListButtonPressed()
    {
        itemReference = 3;
        AudioManager.instance.Play("Menu Sound");
        materialListParent.SetActive(true);

        itemMaterialListText.text += itemMaterialTexts[itemReference - 1];

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
                itemMaterialTexts[i] +=
                    shopStock.ships[i].resourcesNeeded[j].Name()
                    + ": " + shopStock.ships[i].resourcesNeeded[j].GetAmount().ToString() 
                    + "\n";
            }
        }

        //for (int i = 0; i < item2MaterialTexts.Count; i++)
        //{
        //    for (int j = 0; j < shopStock.ships[i].resourcesNeeded.Count; j++)
        //    {
        //        item2MaterialTexts[i] =
        //            shopStock.ships[i].resourcesNeeded[j].Name()
        //            + ": " + shopStock.ships[i].resourcesNeeded[j].GetAmount().ToString();
        //    }
        //}

        //for (int i = 0; i < item3MaterialTexts.Count; i++)
        //{

        //    for (int j = 0; j < shopStock.ships[i].resourcesNeeded.Count; j++)
        //    {
        //        item3MaterialTexts[i] =
        //            shopStock.ships[i].resourcesNeeded[j].Name()
        //            + ": " + shopStock.ships[i].resourcesNeeded[j].GetAmount().ToString();
        //    }
        //}

    }

}
