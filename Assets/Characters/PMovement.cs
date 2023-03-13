using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PMovement : MonoBehaviour
{
   
    
        public float moveSpeed = 5f;
    public Rigidbody2D reggin;
    public Animator animator;

    Vector2 movement;

    

    // Update is called once per frame
    void Update()
    {
  
    movement.x = Input.GetAxisRaw("Hor");
        movement.y = Input.GetAxisRaw("Vert");

        animator.SetFloat("Hor", movement.x);
        animator.SetFloat("Vert", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }
    //Input

    void FixedUpdate()
{

        reggin.MovePosition(reggin.position + movement * moveSpeed * Time.fixedDeltaTime);

}
}
