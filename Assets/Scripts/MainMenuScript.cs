using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    [Header("Scene Names")]
    [SerializeField]
    private string StartSceneName;
    [SerializeField]
    private string CreditsSceneName;
    [SerializeField]
    private string TitleMenuSceneName;

    [Header("Sound Sources")]
    [SerializeField]
    private AudioSource soundSource;
    [SerializeField]
    private AudioClip menuClick;
    [SerializeField]
    private float clickTimer = 0.5f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void StartButtonPressed()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        soundSource.PlayOneShot(menuClick);
        StartCoroutine(WaitforStartButton(clickTimer));

    }

    public void CreditsButtonPressed()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        soundSource.PlayOneShot(menuClick);
        StartCoroutine(WaitforCreditsButton(clickTimer));

    }

    public void TitleMenuButtonPressed()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        soundSource.PlayOneShot(menuClick);
        StartCoroutine(WaitforTitleMenuButton(clickTimer));

    }


    public void CloseGame()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        soundSource.PlayOneShot(menuClick);
        StartCoroutine(WaitforCloseButton(clickTimer));

    }


    IEnumerator WaitforStartButton(float duration)
    {

        yield return new WaitForSeconds(duration);   //Wait

        //reset save data

        SceneManager.LoadScene(StartSceneName);
    }
    IEnumerator WaitforTitleMenuButton(float duration)
    {

        yield return new WaitForSeconds(duration);   //Wait
        SceneManager.LoadScene(TitleMenuSceneName);
    }

    IEnumerator WaitforCreditsButton(float duration)
    {

        yield return new WaitForSeconds(duration);   //Wait
        SceneManager.LoadScene(CreditsSceneName);
    }

    IEnumerator WaitforCloseButton(float duration)
    {

        yield return new WaitForSeconds(duration);   //Wait
        Application.Quit();

    }
}
