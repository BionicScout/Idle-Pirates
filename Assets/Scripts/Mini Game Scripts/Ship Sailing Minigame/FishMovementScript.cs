using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMovementScript : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    private float speed = 3f;

    [SerializeField]
    private float wallSpeed = 4f;

    private Vector2 localScale;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(speed, -wallSpeed);



    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        localScale.x *= -1;

        transform.localScale = new Vector2(localScale.x, 1);

        //if (collision.gameObject.CompareTag("Background")) 
        //{
        //    //transform.localScale.x *= -1;
        //    localScale.x *= -1;

        //    transform.localScale = new Vector2(localScale.x, 1);


        //}
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        localScale.x *= -1;

        transform.localScale = new Vector2(localScale.x, 1);
        
    }


}
