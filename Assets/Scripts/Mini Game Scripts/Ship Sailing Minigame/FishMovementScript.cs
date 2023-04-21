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

    [SerializeField]
    private SpriteRenderer sprite;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //rb.velocity = new Vector2(speed, -wallSpeed);

        this.transform.localPosition += 
            new Vector3(speed, 0, 0) * Time.deltaTime;


        //for flipping fish
        //if(this.transform.localPosition.x < 0)
        //{
        //    sprite.flipX = true;
        //}
        //else if(this.transform.localPosition.x > 0)
        //{
        //    sprite.flipX = false;
        //}

    }


    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    //localScale.x *= -1;


    //    speed *= -1;

    //    //transform.localScale = new Vector2(localScale.x, 1);

    //    //if (collision.gameObject.CompareTag("Background")) 
    //    //{
    //    //    //transform.localScale.x *= -1;
    //    //    localScale.x *= -1;

    //    //    transform.localScale = new Vector2(localScale.x, 1);


    //    //}
    //}


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Collider") 
            || collision.gameObject.CompareTag("Obstacles"))
        {
            speed *= -1;
        }
        //localScale.x *= -1;

        //transform.localScale = new Vector2(localScale.x, 1);

    }


}
