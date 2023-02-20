using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    //The key needed to press and open pause menu
    public string keyName;
    //Game Object in canvas with menu
    public GameObject pauseMenuObject;
    public string Menu;
    public float clickTimer = 0.5f;

    //keeps track of whether game is paused
    bool isPaused = false;


    //[Header("Music")]
    //public MusicManager musicManager;

    [Header("SFX")]
    //audio source to play UI sound
    public AudioSource soundSource;

    //sounds that will play when button Pressed
    public AudioClip pauseSound;
    public AudioClip unpauseSound;
    public AudioClip menuClick;

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

    IEnumerator Wait(float duration)
    {
        
        yield return new WaitForSecondsRealtime(duration);   //Wait
        Time.timeScale = 1f;
        SceneManager.LoadScene(Menu);

    }
}
