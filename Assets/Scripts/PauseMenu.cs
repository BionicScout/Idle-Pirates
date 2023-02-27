using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {
    //The key needed to press and open pause menu
    [SerializeField]
    private string keyName;

    //Game Object in canvas with menu
    [SerializeField]
    private GameObject pauseMenuObject;

    [SerializeField]
    private GameObject extrasWindow;

    [SerializeField]
    private string Menu;

    [SerializeField]
    private float clickTimer = 0.5f;

    //keeps track of whether game is paused
    bool isPaused = false;


    //[Header("Music")]
    //public MusicManager musicManager;

    private void Update() {
        if(Input.GetButtonDown(keyName)) {


            if(isPaused) {
                UnPause();
            }
            else {
                Pause();

            }

        }
    }


    public void UnPause() {
        pauseMenuObject.SetActive(false);
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
        isPaused = false;
        Time.timeScale = 1f;
        AudioManager.instance.Play("Menu Sound");

        //pointScript.enabled = true;
        //musicManager.UnPause();
    }

    public void Pause() {
        pauseMenuObject.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        isPaused = true;
        Time.timeScale = 0f;

        AudioManager.instance.Play("Menu Sound");

        //pointScript.enabled = false;
        //musicManager.Pause();
    }

    public void MenuPressed() {

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        AudioManager.instance.Play("Menu Sound");
        StartCoroutine(Wait(clickTimer));

    }

    public void ExtrasWindowPressed() {
        //Set window to true
        AudioManager.instance.Play("Menu Sound");
        UnPause();
        pauseMenuObject.SetActive(false);
        extrasWindow.SetActive(true);
    }

    IEnumerator Wait(float duration) {

        yield return new WaitForSecondsRealtime(duration);   //Wait
        Time.timeScale = 1f;
        SceneManager.LoadScene(Menu);

    }
}