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



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //movement = Input.GetAxis("Horizontal") * horizontalSpeed;

        float horizontalInput = Input.GetAxisRaw("Horizontal");

        rigbody.velocity = new Vector2(horizontalInput * speed,
            rigbody.velocity.y);
    }



    //When player collides with rock, lock movement

}
