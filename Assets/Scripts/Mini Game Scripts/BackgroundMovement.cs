using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private BoxCollider2D boxCollider;

    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    private float speed = -3f;

    [SerializeField]
    private float length;

    private float disappearTime = 9f;

    [SerializeField]
    private float timer = 0;

    [SerializeField]
    private GameObject maxXPositionObj;

    [SerializeField]
    private GameObject minXPositionObj;

    [SerializeField]
    private GameObject maxYPositionObj;

    [SerializeField]
    private GameObject minYPositionObj;

    [SerializeField]
    private float maxXPosition;

    [SerializeField]
    private float minXPosition;

    [SerializeField]
    private float maxYPosition;

    [SerializeField]
    private float minYPosition;


    [SerializeField]
    private List<GameObject> obstaclePrefabList = new List<GameObject>();

    [SerializeField]
    private int maxSpawnItems = 3;

    [SerializeField]
    private int numOfSpawnItems;


    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();

        length = boxCollider.size.y;
        rb.velocity = new Vector2(0, speed);

        disappearTime = -(speed) * 3;


        maxXPosition = maxXPositionObj.transform.position.x;
        minXPosition = minXPositionObj.transform.position.x;
        maxYPosition = maxYPositionObj.transform.position.y;
        minYPosition = minYPositionObj.transform.position.y;


        //when background is spawned, randomize obstacle position and 
        //obstacles spawned

        //Goes through each item in List and randomize position
        for(int j = 0; j < obstaclePrefabList.Count;j++) 
        {
            obstaclePrefabList[j].transform.position = 
                new Vector3(Random.Range(minXPosition, maxXPosition), 
                Random.Range(minYPosition, maxYPosition), 0);
        
        }


        //randomize number of objects that will spawn
        numOfSpawnItems = Random.Range(1, maxSpawnItems);


        //spawn random objects up to the number of items that can be spawned
        for(int i = 0; i < numOfSpawnItems; i++) 
        {
            int objectSpawnNumber = Random.Range(0, obstaclePrefabList.Count - 1);

            Instantiate(obstaclePrefabList[objectSpawnNumber], 
                transform.position, transform.rotation);

        }


    }

    // Update is called once per frame
    void Update()
    { 

        if (timer < disappearTime)
        {
            timer += Time.deltaTime;

        }
        else 
        { 
            Destroy(this.gameObject);
           
        }


        if(transform.position.y < -length) 
        {
            Reposition();
        
        }



    }

    private void Reposition()
    {
        Vector2 vector = new Vector2(0, length * 2f);
        transform.position = (Vector2)transform.position + vector;
    }





}
