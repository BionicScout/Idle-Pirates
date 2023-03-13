using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpotSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject objectToSpawn;

    [SerializeField]
    private PolygonCollider2D polyCollider;

    [SerializeField]
    private int numberOfSpots = 10;




    void Start()
    {
        polyCollider = GetComponent<PolygonCollider2D>();
        SpawnObjects();
        
    }

    void SpawnObjects()
    {
        Bounds bounds = polyCollider.bounds;

        for (int i = 0; i < numberOfSpots; i++)
        {
            Vector2 randomPoint = new Vector2(
                Random.Range(bounds.min.x, bounds.max.x),
                Random.Range(bounds.min.y, bounds.max.y));

            //Debug.Log(bounds);
            //Debug.Log(randomPoint);


            if (polyCollider.OverlapPoint(randomPoint))
            {
                Instantiate(objectToSpawn, randomPoint, transform.rotation);
            }

        }
    }


   
}
