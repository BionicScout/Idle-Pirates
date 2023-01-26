using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlManager : MonoBehaviour 
{
    public TMP_Text text; //Temp Object

    [SerializeField]
    private int totalTerritories = 5;
    public static int controlledTerritories = 0;

    [SerializeField]
    private List<GameObject> territoryList;

    [SerializeField]
    private string winningSceneName;

    //public static ControlManager cm;


    void Start()
    {
        //if (cm == null)
        //    cm = this;

        totalTerritories = territoryList.Count;
    }

    void Update() 
    {
        if (controlledTerritories == totalTerritories) {
            Win();
        }
        UpdateText();
    }

    void Win() {
        Debug.Log("WIN");
        SceneManager.LoadScene(winningSceneName);
    }

    void UpdateText() { //TEMP METHOD
        float percent = (int)((controlledTerritories * 100f)/ totalTerritories);
        //Debug.Log(controlledTerritories);
        Debug.Log(percent);
        text.text = percent.ToString() + "%";
    }
}