using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MapSceneUI : MonoBehaviour {
    [Header("Base UI")]
    public GameObject baseUI;



    [Header("Trade UI")]
    public GameObject tradeOverview;
    public GameObject tradeResources;

    /**************************************************************************************************************************
        BASE UI 
    **************************************************************************************************************************/

    //This method is called every update
    public void updateResources() {
        string[] resourceNames = { "Gold", "Cloth", "Metal", "Wood" };
        Transform resourceHeader = baseUI.transform.GetChild(0);

        for(int i = 0; i < resourceNames.Length; i++) {
            Resource r = Inventory.instance.resources.Find(x => x.GetName() == resourceNames[i]);
            resourceHeader.GetChild(i).GetChild(1).GetComponent<TMP_Text>().text = r.GetAmount().ToString("n0");
        }
    }

    public void B_FleetButton() {
        setActive_BaseUI(false);
    }

    public void B_TradingButton() {
        setActive_BaseUI(false);
        tradeOverview.SetActive(true);
    }

    public void setActive_BaseUI(bool isActive) {
        baseUI.SetActive(isActive);
    }


    /**************************************************************************************************************************
        FLEET UI 
    **************************************************************************************************************************/


    /**************************************************************************************************************************
        TRADE UI 
    **************************************************************************************************************************/

    public void updateTradeResources() {
        string[] resourceNames = { "Coconut", "Ore", "Rum" };
        Transform resourceHeader = tradeResources.transform;

        for(int i = 0; i < resourceNames.Length; i++) {
            Resource r = Inventory.instance.resources.Find(x => x.GetName() == resourceNames[i]);
            resourceHeader.GetChild(i).GetChild(1).GetComponent<TMP_Text>().text = r.GetAmount().ToString("n0");
        }
    }

    /**************************************************************************************************************************
        OTHER UI 
    **************************************************************************************************************************/
    void Update() {
        updateResources();   
    }
}
