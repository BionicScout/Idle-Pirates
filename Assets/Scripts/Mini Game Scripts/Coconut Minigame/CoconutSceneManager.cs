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
    private GameObject spawnerPrefab;

    [SerializeField]
    private Transform spawnerTransformRef;

    [SerializeField]
    private int gatheredNumber = 0;

    [SerializeField]
    private int neededNumber = 0;

    [SerializeField]
    private float timerNumber = 30f;

    [SerializeField]
    private float currentTime = 30f;

    [SerializeField]
    private int timeInt = 0;

    [SerializeField]
    private int checkpointInt = 0;

    //[SerializeField]
    //private int checkpointInt2 = 0;

    [SerializeField]
    private string winScene;

    [SerializeField]
    private string loseScene;

    [SerializeField]
    [Range(0, 1)]
    private float timeCheckpoint = 0.5f;

    //[SerializeField]
    //private float timeCheckpoint2 = 0.3f;

    [SerializeField]
    private bool checkpointHit = false;

    //[SerializeField]
    //private bool checkpoint2Hit = false;





    // Start is called before the first frame update
    void Start()
    {
        currentTime = timerNumber;

        gatheredText.text = gatheredNumber.ToString();
        neededText.text = neededNumber.ToString();
        timerText.text = currentTime.ToString();
        timeInt = (int)currentTime;

        checkpointInt = (int)(timerNumber * timeCheckpoint);
        //checkpointInt2 = (int)(timerNumber * timeCheckpoint2);



    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= Time.deltaTime;

        timeInt = (int)currentTime;

        timerText.text = timeInt.ToString();

        //boolean

        if ((int)currentTime == checkpointInt && checkpointHit == false)
        {

            GameObject another = Instantiate(spawnerPrefab, 
                spawnerTransformRef.position, 
                spawnerTransformRef.rotation);

            
            //Add another Spawner

            checkpointHit = true;

        }


        //if (timeInt == checkpointInt2 && checkpoint2Hit == false)
        //{
        //    //Decrease spawn time
        //    checkpoint2Hit = true;
        //}


        if (currentTime <= 0) 
        {
            if(gatheredNumber >= neededNumber)
            {
                int coconutsGained = gatheredNumber - neededNumber;

                if (Inventory.instance.crew.Find(x => x.active).crewName == "Pete")
                {
                    float resourceBoostPercentage = .15f;
                    float resourceBoost = coconutsGained * resourceBoostPercentage;
                    int totalResourceGained = coconutsGained + (int)resourceBoost;

                    coconutsGained = totalResourceGained;
                }

                Resource coconuts = 
                    new Resource(Resource.Type.Trade, "Coconut", coconutsGained, 0);
                Inventory.instance.AddResource(coconuts);

                SceneManager.LoadScene(winScene);
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
