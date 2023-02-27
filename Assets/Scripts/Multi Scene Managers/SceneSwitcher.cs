using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour {
    public static SceneSwitcher instance;

    public static string currentScene;

    void Awake() {
        if (instance == null)
            instance = this;
    }

    public void A_ExitButton() {
        Application.Quit();
    }

    public void A_LoadScene(string sceneName) {
        currentScene = sceneName;
        SceneManager.LoadScene(sceneName);
    }

    private void Update() {

    }
}
