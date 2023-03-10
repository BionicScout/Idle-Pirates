using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    //public List<ResourceShopMenu> shopMenus;
    //public List<InventoryShip> shipsToBuy;
    //public List<InventoryCrew> crewToBuy;

    public ResourceShopMenu shopMenu;

    //Maybe have a class for each shop?


    [SerializeField]
    private GameObject item1Button;
    [SerializeField] 
    private GameObject item2Button;
    [SerializeField] 
    private GameObject item3Button;

    [SerializeField]
    private Inventory shopInventory;


    [SerializeField]
    private int shipBuyResourceTypes;
    [SerializeField]
    private int shipTypes;
    [SerializeField]
    private int crewTypes;

    [SerializeField]
    private GameObject shipShopParentObject;

    [SerializeField]
    private GameObject resourceShopParentObject;
    
    [SerializeField]
    private GameObject crewShopParentObject;


    // Start is called before the first frame update
    void Start()
    {
        shopMenu.SetValues(shopInventory);


        //Set each shop to the stock
        //for(int i = 0; i< shopMenus.Count; i++) 
        //{
        //    shopMenus[i].SetValues(shopInventory.GetComponent<Inventory>());
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BuyOneShipResourceItemOne()
    {



    }


    public void BuyOneShipResourceItemTwo()
    {



    }


    public void BuyOneShipResourceItemThree()
    {



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
    public void BuyCrewItemOne()
    {



    }


    public void BuyCrewItemTwo()
    {



    }


    public void BuyCrewItemThree()
    {



    }


    public void ShipMinimizeButtonPressed()
    {
        //Set window to false
        AudioManager.instance.Play("Menu Sound");
        

        shipShopParentObject.SetActive(false);
    }


    public void ResourceMinimizeButtonPressed()
    {
        //Set window to false
        AudioManager.instance.Play("Menu Sound");


        resourceShopParentObject.SetActive(false);
    }


    public void CrewMinimizeButtonPressed()
    {
        //Set window to false
        AudioManager.instance.Play("Menu Sound");


        crewShopParentObject.SetActive(false);
    }


    public void OpenResourceShopMenu()
    {
        AudioManager.instance.Play("Menu Sound");

        resourceShopParentObject.SetActive(true);

    }

    public void OpenShipShopMenu()
    {
        AudioManager.instance.Play("Menu Sound");

        shipShopParentObject.SetActive(true);

    }

    public void OpenCrewShopMenu()
    {
        AudioManager.instance.Play("Menu Sound");

        crewShopParentObject.SetActive(true);

    }

}
