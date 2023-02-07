using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rigbody;

    [SerializeField]
    [Range(0, 50)]
    private float speed;
    private float movement;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //movement = Input.GetAxis("Horizontal") * speed;

        float horizontalInput = Input.GetAxis("Horizontal");

        rigbody.velocity = new Vector2(horizontalInput * speed, rigbody.velocity.y);
    }
}
