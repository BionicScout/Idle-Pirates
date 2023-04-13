using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShipCleaningSceneManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI cleanedText;

    [SerializeField]
    private TextMeshProUGUI neededText;

    [SerializeField]
    private TextMeshProUGUI timerText;

    [SerializeField]
    private int cleanedNumber = 0;

    [SerializeField]
    private int neededNumber = 0;

    [SerializeField]
    private float timerNumber = 30f;

    [SerializeField]
    private int timeInt = 0;

    [SerializeField]
    private string winScene;

    [SerializeField]
    private string loseScene;

    [SerializeField]
    private SpotImageSpawner spotSpawner;

    [SerializeField]
    [Range(0f, 3f)]
    private float timeMultipler = 0.67f;


    // Start is called before the first frame update
    void Start()
    {
        cleanedText.text = cleanedNumber.ToString();

        neededNumber = spotSpawner.numberOfSpots;
     
        neededText.text = neededNumber.ToString();

        timerNumber = neededNumber * timeMultipler;

        timerText.text = timerNumber.ToString();
        timeInt = (int)timerNumber;
    }

    // Update is called once per frame
    void Update()
    {
        timerNumber -= Time.deltaTime;

        timeInt = (int)timerNumber;

        timerText.text = timeInt.ToString();

        if(cleanedNumber >= neededNumber)
        {


            SceneManager.LoadScene(winScene);
        }


        if (timerNumber <= 0)
        {
            if (cleanedNumber < neededNumber)
            {
                
                SceneManager.LoadScene(loseScene);
            }
            

        }
    }


    public void SpotCleanedUpdate()
    {
        cleanedNumber++;
        cleanedText.text = cleanedNumber.ToString();
    }
}
