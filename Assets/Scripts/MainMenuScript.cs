using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
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
    private string miniGameSceneName;

    [Header("Sound Sources")]
    [SerializeField]
    private AudioSource soundSource;

    [SerializeField]
    private AudioClip menuClick;

    [SerializeField]
    private float clickTimer = 0.5f;


    [Header("Window Variables")]
    [SerializeField]
    private GameObject extrasWindow;
    //[SerializeField]
    //private GameObject pauseWindow;



    public void StartGameButtonPressed()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        soundSource.PlayOneShot(menuClick);
        StartCoroutine(WaitforStartGameButton(clickTimer));

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

    public void MapButtonPressed()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        soundSource.PlayOneShot(menuClick);
        StartCoroutine(WaitforMapButton(clickTimer));

    }

    public void CombatButtonPressed()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        soundSource.PlayOneShot(menuClick);
        StartCoroutine(WaitforCombatButton(clickTimer));

    }

    public void MiniGameButtonPressed()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        soundSource.PlayOneShot(menuClick);
        StartCoroutine(WaitforMiniGameButton(clickTimer));

    }


    public void MinimizeButtonPressed()
    {
        //Set window to false
        soundSource.PlayOneShot(menuClick);
        extrasWindow.SetActive(false);
    }
    


    public void CloseGame()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        soundSource.PlayOneShot(menuClick);
        StartCoroutine(WaitforCloseButton(clickTimer));

    }


    IEnumerator WaitforStartGameButton(float duration)
    {

        yield return new WaitForSeconds(duration);   //Wait

        //reset save data
        //Make sure this is set to only button on title screen
        //CityInbetweenManagementScript.staticCityList.Clear();
        //CityInbetweenManagementScript.currentStaticCityNumber = 0;

        SaveStateManager.instance.DeleteData();

        SceneManager.LoadScene(startGameSceneName);
    }

    IEnumerator WaitforStartButton(float duration)
    {

        yield return new WaitForSeconds(duration);   //Wait

        SceneManager.LoadScene(startSceneName);
    }


    IEnumerator WaitforTitleMenuButton(float duration)
    {

        yield return new WaitForSeconds(duration);   //Wait
        SceneManager.LoadScene(titleMenuSceneName);
    }

    IEnumerator WaitforCreditsButton(float duration)
    {

        yield return new WaitForSeconds(duration);   //Wait
        SceneManager.LoadScene(creditsSceneName);
    }

    IEnumerator WaitforCloseButton(float duration)
    {

        yield return new WaitForSeconds(duration);   //Wait
        Application.Quit();

    }

    IEnumerator WaitforMapButton(float duration)
    {

        yield return new WaitForSeconds(duration);   //Wait

        //reset save data

        SceneManager.LoadScene(mapSceneName);
    }


    IEnumerator WaitforCombatButton(float duration)
    {

        yield return new WaitForSeconds(duration);   //Wait

        //reset save data

        SceneManager.LoadScene(combatSceneName);
    }


    IEnumerator WaitforMiniGameButton(float duration)
    {

        yield return new WaitForSeconds(duration);   //Wait

        //reset save data

        SceneManager.LoadScene(miniGameSceneName);
    }

}
