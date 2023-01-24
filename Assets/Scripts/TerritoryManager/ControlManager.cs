using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlManager : MonoBehaviour 
{
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
        
    }

    void Win() {
        Debug.Log("WIN");
        SceneManager.LoadScene(winningSceneName);
    }
}