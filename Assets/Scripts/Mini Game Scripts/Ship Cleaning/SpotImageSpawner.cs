using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpotImageSpawner : MonoBehaviour
{
    [SerializeField]
    private Canvas canvas;

    [SerializeField]
    private GameObject imageToSpawn;

    [SerializeField]
    private PolygonCollider2D polyCollider;

    [SerializeField]
    private Camera canvasCamera;

    public int numberOfSpots = 10;


    void Start()
    {
        polyCollider = GetComponent<PolygonCollider2D>();
        SpawnImage();
    }

    void SpawnImage()
    {

        for (int i = 0; i < numberOfSpots; i++)
        {
            Vector2 randomPoint = GetRandomPointInCollider();
            if (polyCollider.OverlapPoint(randomPoint))
            {
                GameObject spawnedObject = Instantiate(imageToSpawn, randomPoint, Quaternion.identity);
                RectTransform rectTransform = spawnedObject.GetComponent<RectTransform>();
                Vector3 canvasPosition = canvasCamera.WorldToScreenPoint(randomPoint);
                rectTransform.anchoredPosition = (Vector2)canvasPosition - canvas.GetComponent<RectTransform>().sizeDelta / 2f;
                spawnedObject.transform.SetParent(canvas.transform, false);
            }
        }


        //for (int i = 0; i < 10; i++)
        //{
        //    GameObject imageInstance = Instantiate(imageToSpawn);
        //    Vector2 randomPosition = GetRandomPosition();
        //    while (!polyCollider.bounds.Contains(randomPosition))
        //    {
        //        randomPosition = GetRandomPosition();
        //    }
        //    imageInstance.transform.position = randomPosition;
        //}



        //Old Method
        //Bounds bounds = polyCollider.bounds;

        //for (int i = 0; i < numberOfSpots; i++)
        //{

        //    Vector2 randomPoint = new Vector2(
        //    Random.Range(bounds.min.x, bounds.max.x),
        //    Random.Range(bounds.min.y, bounds.max.y));



        //    //Debug.Log(bounds);
        //    //Debug.Log(randomPoint);


        //    if (polyCollider.OverlapPoint(randomPoint))
        //    {
        //        // Create a new instance of the image and Add it to the canvas
        //        Image newImage = Instantiate(imageToSpawn, canvas.transform);

        //        // Convert the random point from world space to local space within the canvas
        //        Vector2 localPoint;
        //        RectTransformUtility.ScreenPointToLocalPointInRectangle(
        //            canvas.transform as RectTransform,
        //            Camera.main.WorldToScreenPoint(randomPoint),
        //            Camera.main,
        //            out localPoint);

        //        Debug.Log(localPoint);

        //        // Set the position of the image to the converted point
        //        newImage.rectTransform.localPosition = localPoint;
        //    }
        //}
    }



    //Vector2 GetRandomPosition()
    //{
    //    float x = Random.Range(polyCollider.bounds.min.x, polyCollider.bounds.max.x);
    //    float y = Random.Range(polyCollider.bounds.min.y, polyCollider.bounds.max.y);
    //    return new Vector2(x, y);
    //}


    private Vector2 GetRandomPointInCollider()
    {
        Vector2 point;
        Bounds bounds = polyCollider.bounds;
        do
        {
            point = new Vector2(Random.Range(bounds.min.x, bounds.max.x), Random.Range(bounds.min.y, bounds.max.y));
        } while (!polyCollider.OverlapPoint(point));
        return point;
    }
}
