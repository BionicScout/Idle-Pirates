using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour {
    [Header("Scene Names")]
    [SerializeField]
    private string startGameSceneName;
    [SerializeField]
    private string startSceneName;
    [SerializeField]
    private string creditsSceneName;
    [SerializeField]
    private string titleMenuSceneName;
    [SerializeField]
    private string mapSceneName;
    [SerializeField]
    private string combatSceneName;
    [SerializeField]
    private string coconutMiniGameSceneName;
    [SerializeField]
    private string shipSailingMiniGameSceneName;
    [SerializeField]
    private string shipCleaningMiniGameSceneName;



    [SerializeField]
    private float clickTimer = 0.5f;


    [Header("Window Variables")]
    [SerializeField]
    private GameObject extrasWindow;
    //[SerializeField]
    //private GameObject pauseWindow;


//Buton Press Functions
    public void StartGameButtonPressed() {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        AudioManager.instance.Play("Menu Sound");
        StartCoroutine(WaitforStartGameButton(clickTimer));
    }

    public void StartButtonPressed() {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        AudioManager.instance.Play("Menu Sound");
        StartCoroutine(WaitButton(clickTimer, startSceneName));

    }

    public void CreditsButtonPressed() {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        AudioManager.instance.Play("Menu Sound");
        StartCoroutine(WaitButton(clickTimer, creditsSceneName));
    }

    public void TitleMenuButtonPressed() {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        AudioManager.instance.Play("Menu Sound");
        StartCoroutine(WaitButton(clickTimer, titleMenuSceneName));
    }

    public void MapButtonPressed() {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        AudioManager.instance.Play("Menu Sound");
        StartCoroutine(WaitButton(clickTimer, mapSceneName));

    }

    public void CombatButtonPressed() {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        AudioManager.instance.Play("Menu Sound");
        StartCoroutine(WaitButton(clickTimer, combatSceneName));

    }

    public void CoconutMiniGameButtonPressed() {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        AudioManager.instance.Play("Menu Sound");
        StartCoroutine(WaitButton(clickTimer, coconutMiniGameSceneName));

    }

    public void ShipSailingMiniGameButtonPressed()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        AudioManager.instance.Play("Menu Sound");
        StartCoroutine(WaitButton(clickTimer, shipSailingMiniGameSceneName));

    }

    public void ShipCleaningMiniGameButtonPressed()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        AudioManager.instance.Play("Menu Sound");
        StartCoroutine(WaitButton(clickTimer, shipCleaningMiniGameSceneName));


    }


    public void MinimizeButtonPressed() {
        //Set window to false
        AudioManager.instance.Play("Menu Sound");
        extrasWindow.SetActive(false);
    }

    public void CloseGame() {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        AudioManager.instance.Play("Menu Sound");
        StartCoroutine(WaitforCloseButton(clickTimer));
    }

//Wait Methods
    IEnumerator WaitforStartGameButton(float duration) {
        yield return new WaitForSeconds(duration);   //Wait

        //reset save data
        //Make sure this is set to only button on title screen


        SaveStateManager.instance.DeleteData();


        SceneSwitcher.instance.A_LoadScene("Map Scene");
    }

    IEnumerator WaitforCloseButton(float duration) {
        yield return new WaitForSeconds(duration);   //Wait
        Application.Quit();
    }

    IEnumerator WaitButton(float duration, string sceneName) {
        yield return new WaitForSeconds(duration);   //Wait

        //reset save data

        SceneSwitcher.instance.A_LoadScene(sceneName);
    }
}