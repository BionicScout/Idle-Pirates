using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExplosionScript : MonoBehaviour
{
    [SerializeField]
    private float timerNumber = 3f;

    [SerializeField]
    private int timeInt = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timerNumber -= Time.deltaTime;

        timeInt = (int)timerNumber;


        if (timerNumber <= 0)
        {
            Destroy(gameObject);


        }
    }
}
