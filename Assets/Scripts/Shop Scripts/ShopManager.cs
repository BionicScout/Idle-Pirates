using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Buffers.Text;

public class ShopManager : MonoBehaviour
{
    //public List<ResourceShopMenu> shopMenus;
    //public List<InventoryShip> shipsToBuy;
    //public List<InventoryCrew> crewToBuy;

    [SerializeField]
    private ResourceShopMenu resourceShopMenu;
    [SerializeField]
    private ShipShopMenu shipShopMenu;
    [SerializeField]
    private CrewShopMenu crewShopMenu;

    //Maybe have a class for each shop?


    //[SerializeField]
    //private GameObject item1Button;
    //[SerializeField] 
    //private GameObject item2Button;
    //[SerializeField] 
    //private GameObject item3Button;

    [SerializeField]
    private ShopInventory shopInventory;


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

    [SerializeField]
    private List<TextMeshProUGUI> resourceNameTexts;

    [SerializeField]
    private List<TextMeshProUGUI> resourceAmountTexts;


    // Start is called before the first frame update
    void Start()
    {
        crewShopMenu.SetValues(shopInventory);
        resourceShopMenu.SetValues(shopInventory);
        shipShopMenu.SetValues(shopInventory);
        


        //Set each shop to the stock
        //for(int i = 0; i< shopMenus.Count; i++) 
        //{
        //    shopMenus[i].SetValues(shopInventory.GetComponent<Inventory>());
        //}
    }

    // Update is called once per frame
    void Update()
    {
        //Make a function for showing and updating the respurces you have
        //Base that on the MapSceneUI Function UpdateResources
        UpdateInventoryUIForShop();

    }

    
    private void UpdateInventoryUIForShop()
    {
        string[] resourceNames = new string[resourceNameTexts.Count];
        //Transform resourceHeader = baseUI.transform.GetChild(0);

        for (int i = 0; i < resourceNameTexts.Count; i++)
        {
            resourceNames[i] = resourceNameTexts[i].text.ToString();
        }

        for (int i = 0; i < resourceNames.Length; i++)
        {
            Resource r = Inventory.instance.resources.Find(x => x.GetName() == resourceNames[i]);
            //resourceHeader.GetChild(i).GetChild(1).GetComponent<TMP_Text>().text
            //= r.GetAmount().ToString("n0");
            resourceAmountTexts[i].text = r.GetAmount().ToString();
        }


        //for (int i = 0; i < resourceNames.Length; i++)
        //{ 
        //    if()
        //}
     }


    public void BuyCrew(int index)
    {
        Inventory.instance.AddCrew(shopInventory.crewStock[index - 1]);
        //shopInventory.crewStock[index - 1].ReduceAmount(1);

    }


    public void BuyShip(int index)
    {
        shopInventory.shipStock[index - 1].use = InventoryShip.USED_IN.none;
        Inventory.instance.AddShip(shopInventory.shipStock[index - 1]);


    }

    public void BuyResource(int index)
    {
        Inventory.instance.AddResource(shopInventory.resourceStock[index - 1]);
        //shopInventory.resourceStock[index - 1].SubtractAmount(1);


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
