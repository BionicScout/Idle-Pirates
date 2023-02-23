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

    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();

        length = boxCollider.size.y;
        rb.velocity = new Vector2(0, speed);
    }

    // Update is called once per frame
    void Update()
    {
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
