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
            int resourceNumber = 
                Random.Range(0, Inventory.instance.shipBuildResourceTemplates.Count - 1);

            int resourceGained =
                Random.Range(0, neededNumber);


            //if Pete is active
            if (Inventory.instance.crew.Find(x => x.active).crewName == "Pete")
            {
                float resourceBoostPercentage = .15f;
                float resourceBoost = resourceGained * resourceBoostPercentage;
                int totalResourceGained = resourceGained + (int)resourceBoost;

                resourceGained = totalResourceGained;
            }


            if (resourceNumber == 0)
            {
                Resource metal =
                    new Resource(Resource.Type.Ship_Build, "Metal", resourceGained, 0);
                Inventory.instance.AddResource(metal);
            }

            if (resourceNumber == 1)
            {
                Resource wood =
                     new Resource(Resource.Type.Ship_Build, "Wood", resourceGained, 0);
                Inventory.instance.AddResource(wood);
            }

            if (resourceNumber == 2)
            {
                Resource cloth =
                    new Resource(Resource.Type.Ship_Build, "Cloth", resourceGained, 0);
                Inventory.instance.AddResource(cloth);
            }

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
