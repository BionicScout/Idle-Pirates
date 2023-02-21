using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CoconutSceneManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI gatheredText;

    [SerializeField]
    private TextMeshProUGUI neededText;

    [SerializeField]
    private TextMeshProUGUI timerText;

    [SerializeField]
    private int gatheredNumber = 0;

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



   // Start is called before the first frame update
   void Start()
    {
        gatheredText.text = gatheredNumber.ToString();
        neededText.text = neededNumber.ToString();
        timerText.text = timerNumber.ToString();
        timeInt = (int)timerNumber;


    }

    // Update is called once per frame
    void Update()
    {
        timerNumber -= Time.deltaTime;

        timeInt = (int)timerNumber;

        timerText.text = timeInt.ToString();


        if(timerNumber <= 0) 
        {
            if(gatheredNumber >= neededNumber)
            {
                SceneManager.LoadScene(winScene);

                //When the game is over, calculate the number of cocnuts gathered
                //and subtract that by the number of coconuts you needed to get
                //so you can get materials from it.
            }
            else
            {
                SceneManager.LoadScene(loseScene);
            }
        
        }
    }


    public void CoconutGatheredUpdate()
    {
        gatheredNumber++;
        gatheredText.text = gatheredNumber.ToString();
    }
}
