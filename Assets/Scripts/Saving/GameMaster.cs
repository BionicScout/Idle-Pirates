using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour {
    GameData saveData = new GameData();


    private void Start() {
        if(SceneSwitcher.instance.GetCurrentScene() == "Map Scene") {
            Debug.Log("Saved from Menu");
            SaveStateManager.instance.SaveGame(saveData);
        }
    }

    public void OnSaveButtonClick() {
        SaveStateManager.instance.SaveGame(saveData);

        //save when going to map

        //_gameStateManager?.SaveGame();
    }

    public void OnLoadButtonClick() {
        saveData = SaveStateManager.instance.LoadGame();

        //_gameStateManager?.SaveGame();
    }

    public void OnStartButtonClicktoDelete() {
        SaveStateManager.instance.DeleteData();
    }
}
