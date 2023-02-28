using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSpawnerScript : MonoBehaviour
{

    [SerializeField]
    private GameObject backgroundPrefab;

    [SerializeField]
    private float spawnTime = 10f;

    [SerializeField]
    private float timer = 0;


    // Start is called before the first frame update
    void Start()
    {
        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        if (MiniGameShipMovement.gotHit == false)
        {
            if (timer < spawnTime)
            {
                timer += Time.deltaTime;

            }
            else
            {
                Spawn();
                timer = 0;

            }
        }
    }

    private void Spawn()
    {
        Instantiate(backgroundPrefab, transform.position, transform.rotation);
        
    }


}
