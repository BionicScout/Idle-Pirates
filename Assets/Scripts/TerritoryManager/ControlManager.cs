using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlManager : MonoBehaviour {
    public int totalTerritories = 5;
    public static int controlledTerritories = 0;

    public List<GameObject> territoryList;

    public string winningSceneName;

    //public static ControlManager cm;


    void Start()
    {
        //if (cm == null)
        //    cm = this;

        totalTerritories = territoryList.Count;
    }

    void Update() 
    {

        //if(Input.GetKeyDown(KeyCode.Equals) && controlledTerritories < totalTerritories) {
        //    controlledTerritories++;
        //}
        //if(Input.GetKeyDown(KeyCode.Minus) && controlledTerritories > 0) {
        //    controlledTerritories--;
        //}

        //if (territoryList[controlledTerritories].GetComponent<Territory>().controlled == true)
        //{
        //    controlledTerritories++;
        //}


        if (controlledTerritories == totalTerritories) {
            Win();
        }
        

    }

    void Win() {
        Debug.Log("WIN");
        SceneManager.LoadScene(winningSceneName);
    }
}