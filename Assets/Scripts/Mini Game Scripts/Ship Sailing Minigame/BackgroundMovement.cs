using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    // Start is called before the first frame update

    //Try to make a speed booster to 
    //have the backgrounds move faster
    //Also add way to get rid of backgrounds without 
    //timer and with collision.
    //Find way to have score increase based on speed of background


    //[SerializeField]
    //private BoxCollider2D boxCollider;

    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    private float speed = -3f;

    //[SerializeField]
    //private float length;

    [SerializeField]
    private float disappearTime;

    [SerializeField]
    public int disappearMultiplier = 3;

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
        //boxCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();

        //length = boxCollider.size.y;

        //rb.velocity = new Vector2(0, speed);

        

        disappearTime = -(speed) * disappearMultiplier;


        maxXPosition = maxXPositionObj.transform.position.x;
        minXPosition = minXPositionObj.transform.position.x;
        maxYPosition = maxYPositionObj.transform.position.y;
        minYPosition = minYPositionObj.transform.position.y;


        //when background is spawned, randomize obstacle position and 
        //obstacles spawned

        //Goes through each item in List and randomize position
        for (int j = 0; j < obstaclePrefabList.Count; j++)
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
            int objectSpawnNumber = Random.Range(0, obstaclePrefabList.Count);

            //transform.position = new Vector3(Random.Range(minXPosition, maxXPosition),
            //    Random.Range(minYPosition, maxYPosition), 0);


            GameObject child = Instantiate(obstaclePrefabList[objectSpawnNumber],
                obstaclePrefabList[objectSpawnNumber].transform.position,
                transform.rotation) as GameObject;

            child.transform.parent = this.transform;

        }


    }

    // Update is called once per frame
    void Update()
    {
        if (MiniGameShipMovement.gotHit == false)
        {
            if (timer < disappearTime)
            {
                timer += Time.deltaTime;

            }
            else
            {
                Destroy(this.gameObject);

            }
        }


        if (MiniGameShipMovement.gotHit == true)
        {
            rb.velocity = new Vector2(0, 0);
        }
        else
        {
            rb.velocity = new Vector2(0, speed);
        }

        //if(transform.position.y < -length) 
        //{
        //    Reposition();

        //}



    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if(collision.gameObject.CompareTag("Obstacles"))
    //    {
    //        Destroy(boxCollider); 

    //    }
    //}





}
