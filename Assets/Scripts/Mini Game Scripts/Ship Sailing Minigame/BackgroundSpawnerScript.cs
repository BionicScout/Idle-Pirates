using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSpawnerScript : MonoBehaviour
{

    [SerializeField]
    private GameObject backgroundPrefab;

    /*
    //Have a speed variable for the backgrounds to reference

    //make function for background to increase and decrease speed 
    //when player hits a speed boost panel

    //[SerializeField]
    //private float spawnTime = 10f;

    //[SerializeField]
    //private float timer = 0;

    //[SerializeField]
    //private float spawnCountOffset = 1.43f;
    */


    // Start is called before the first frame update
    void Start()
    {
        /*
        //spawnTime = 
            //-(backgroundPrefab.GetComponent<BackgroundMovement>().speed) * spawnCountOffset; 

        //Spawn();
        */
    }

    // Update is called once per frame
    void Update()
    {
        /*
        //if (MiniGameShipMovement.gotHit == false)
        //{
        //    if (timer < spawnTime)
        //    {
        //        timer += Time.deltaTime;

        //    }
        //    else
        //    {
        //        Spawn();
        //        timer = 0;

        //    }
        //}
        */
    }

    private void Spawn()
    {
        Instantiate(backgroundPrefab, transform.position, transform.rotation);
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Background"))
        {
            Destroy(collision.gameObject.GetComponent<BoxCollider2D>());
            Spawn();

        }
    }


}
