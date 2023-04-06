using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ControlManager : MonoBehaviour {
    public TMP_Text percentageText;

    public int controlledTerritories = 0;

    [SerializeField]
    private List<GameObject> territoryList;

    public int percentage = 0;

    void Update() {
        if(controlledTerritories == territoryList.Count) {
            Win();
        }
        UpdateText();
    }

    void Win() {
        Debug.Log("WIN");
        SceneSwitcher.instance.A_LoadScene("You Win");
    }

    void UpdateText() {   
        percentage = (int)((controlledTerritories * 100f) / territoryList.Count);
        percentageText.text = percentage.ToString() + "%";
    }

    public void AddControlledTerritories() {
        controlledTerritories += 1;
    }
}