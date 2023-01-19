using UnityEditor;
using UnityEngine;

public class ControlManager : MonoBehaviour {
    public int totalTerritories = 5;
    public static int currentTerritories = 0;
    //public static ControlManager cm;


    //void Start() {
    //    if(cm == null)
    //        cm = this;
    //}

    void Update() {
        //if(Input.GetKeyDown(KeyCode.Equals) && currentTerritories < totalTerritories) {
        //    currentTerritories++;
        //}
        //if(Input.GetKeyDown(KeyCode.Minus) && currentTerritories > 0) {
        //    currentTerritories--;
        //}

        if(currentTerritories == totalTerritories) {
            Win();
        }
        else {
            Debug.Log(currentTerritories);
        }
    }

    void Win() {
        Debug.Log("WIN");
    }
}