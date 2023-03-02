using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShipMoveSceneManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI gatheredDistanceText;

    [SerializeField]
    private TextMeshProUGUI neededDistanceText;

    [SerializeField]
    private TextMeshProUGUI timerText;


    //[SerializeField]
    //private int scoreMultipler = 100;

    [SerializeField]
    private float currentDistance = 0;

    [SerializeField]
    private int neededDistance = 0;

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
        gatheredDistanceText.text = currentDistance.ToString();
        neededDistanceText.text = neededDistance.ToString();
        timerText.text = timerNumber.ToString();
        timeInt = (int)timerNumber;
    }

    // Update is called once per frame
    void Update()
    {
        //currentDistance = (currentDistance + 1) * scoreMultipler;
        if (MiniGameShipMovement.gotHit == false)
        {
            DistanceUpdate();

        }
        timerNumber -= Time.deltaTime;

        timeInt = (int)timerNumber;

        timerText.text = timeInt.ToString();


        if (timerNumber <= 0)
        {
            if (currentDistance >= neededDistance)
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

    public void DistanceUpdate()
    {
        currentDistance = (currentDistance + 1);
        gatheredDistanceText.text = currentDistance.ToString();
    }


}
