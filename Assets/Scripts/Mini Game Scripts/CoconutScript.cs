using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoconutScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Box"))
    //    {
    //        Destroy(this.gameObject);
    //    }

    //    if (collision.gameObject.CompareTag("Player"))
    //    {

    //    }
    //}


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Box"))
        {
            Destroy(this.gameObject);
        }

        if (collision.gameObject.CompareTag("Player"))
        {

        }
    }

}