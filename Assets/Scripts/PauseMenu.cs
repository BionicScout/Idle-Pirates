using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
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

    [Header("SFX")]
    //audio source to play UI sound
    [SerializeField]
    private AudioSource soundSource;

    //sounds that will play when button Pressed
    [SerializeField]
    private AudioClip pauseSound;

    [SerializeField]
    private AudioClip unpauseSound;

    [SerializeField]
    private AudioClip menuClick;

    private void Update()
    {
        if (Input.GetButtonDown(keyName))
        {
       

            if(isPaused)
            {
                UnPause();
            }
            else
            {
                Pause();

            }

        }
    }


    public void UnPause()
    {
        pauseMenuObject.SetActive(false);
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
        isPaused = false;
        Time.timeScale = 1f;
        soundSource.PlayOneShot(unpauseSound);

        //pointScript.enabled = true;
        //musicManager.UnPause();
    }

    public void Pause()
    {
        pauseMenuObject.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        isPaused = true;
        Time.timeScale = 0f;

        soundSource.PlayOneShot(pauseSound);

        //pointScript.enabled = false;
        //musicManager.Pause();
    }

    public void MenuPressed()
    {

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        soundSource.PlayOneShot(menuClick);
        StartCoroutine(Wait(clickTimer));

    }

    public void ExtrasWindowPressed()
    {
        //Set window to true
        soundSource.PlayOneShot(menuClick);
        UnPause();
        pauseMenuObject.SetActive(false);
        extrasWindow.SetActive(true);
    }

    IEnumerator Wait(float duration)
    {
        
        yield return new WaitForSecondsRealtime(duration);   //Wait
        Time.timeScale = 1f;
        SceneManager.LoadScene(Menu);

    }
}
