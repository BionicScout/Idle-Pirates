using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoconutSpawner : MonoBehaviour
{
    [SerializeField]
    private Transform spawnPosition;

    [SerializeField]
    private float minSpawnSec = 5, maxSpawnSec = 30;

    [SerializeField]
    private GameObject coconutPrefab;

    [SerializeField]
    private Transform startPoint, endPoint;

    private float maxXPosition, minXPosition;

    [SerializeField]
    private float spawnEverySeconds;

    //For checking how many seconds have passed.
    float secPassed = 0;



    // Start is called before the first frame update
    private void Start()
    {

        spawnEverySeconds = Random.Range(minSpawnSec, maxSpawnSec);

        minXPosition = startPoint.position.x;
        maxXPosition = endPoint.position.x;

    }

    // Update is called once per frame
    void Update()
    {
        secPassed += Time.deltaTime;

        if (secPassed > spawnEverySeconds)
        {
            secPassed = 0;

            //Sets spawn at random x based on the two points
            spawnPosition.position = new Vector3(
                Random.Range(minXPosition, maxXPosition),
                spawnPosition.position.y,
                spawnPosition.position.z);

           

            //Spawns object at a random x
            GameObject temp = 
                Instantiate(coconutPrefab,
                spawnPosition.position, 
                coconutPrefab.transform.rotation);

            spawnEverySeconds = Random.Range(minSpawnSec, maxSpawnSec);
        }
    }
}
