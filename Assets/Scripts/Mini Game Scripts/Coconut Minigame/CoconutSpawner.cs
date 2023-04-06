using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoconutSpawner : MonoBehaviour
{
    [SerializeField]
    private Transform spawnPosition;

    //[SerializeField]
    //private float spawnSetSec = 2;
    //[SerializeField]
    //private float minSpawnSec = 2, maxSpawnSec = 4;
    [SerializeField]
    private float minWarningSec = 1, maxWarningSec = 2;

    [SerializeField]
    private GameObject coconutPrefab;

    [SerializeField] 
    private GameObject warningSprite;

    [SerializeField]
    private Transform startPoint, endPoint;

    private float maxXPosition, minXPosition;

    //[SerializeField]
    //private float spawnEverySeconds;
    [SerializeField]
    private float warningEverySeconds;


    [SerializeField]
    private float spawnEverySetSeconds;

    //For checking how many seconds have passed.
    [SerializeField]
    float secPassed = 0;
    [SerializeField]
    float secWarningPassed = 0;



    // Start is called before the first frame update
    private void Start()
    {
        spawnPosition = this.transform;

        startPoint = GameObject.FindWithTag("StartPoint").transform;

        endPoint = GameObject.FindWithTag("EndPoint").transform;


        RandomizeSpawnSeconds();
        RandomizeSpawnPosition();




        minXPosition = startPoint.position.x;
        maxXPosition = endPoint.position.x;

    }

    // Update is called once per frame
    void Update()
    {
        if (secPassed <= 0)
        {
            secWarningPassed += Time.deltaTime;
        }

        if (secWarningPassed > warningEverySeconds)
        {
            //secWarningPassed = 0;

            warningSprite.SetActive(true);

            secPassed += Time.deltaTime;

            if (secPassed > spawnEverySetSeconds)
            {
                warningSprite.SetActive(false);
                secWarningPassed = 0;
                secPassed = 0;

                //Spawns object at a random x
                GameObject temp =
                    Instantiate(coconutPrefab,
                    spawnPosition.position,
                    coconutPrefab.transform.rotation);


                RandomizeSpawnPosition();

                RandomizeSpawnSeconds();

            }
        }
    }



    void RandomizeSpawnSeconds()
    {
        warningEverySeconds = Random.Range(minWarningSec, maxWarningSec);
        //spawnEverySeconds = Random.Range(minSpawnSec, maxSpawnSec);

        //spawnEverySetSeconds = spawnSetSec;


    }
    void RandomizeSpawnPosition()
    {
        //Sets spawn at random x based on the two points
        spawnPosition.position = new Vector3(
            Random.Range(minXPosition, maxXPosition),
            spawnPosition.position.y, spawnPosition.position.z);
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Spawner"))
        {
            RandomizeSpawnPosition();
        }
    }



}
