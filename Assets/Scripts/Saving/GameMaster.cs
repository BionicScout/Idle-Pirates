using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour {
    GameData saveData = new GameData();

    public void OnSaveButtonClick() {
        SaveStateManager.instance.SaveGame(saveData);

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
