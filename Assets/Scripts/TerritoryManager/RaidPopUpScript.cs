using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaidPopUpScript : MonoBehaviour
{
    public static bool tookCity = false;

    //try to change color of city when raided

    public void YesButtonPressed()
    {
        //raidPopUp.SetActive(true);


        tookCity = true;
        this.gameObject.SetActive(false); 



    }

    public void NoButtonPressed()
    {
        this.gameObject.SetActive(false);
    }

}
