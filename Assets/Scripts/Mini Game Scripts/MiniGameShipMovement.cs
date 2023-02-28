using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameShipMovement : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rigbody;

    [SerializeField]
    [Range(0, 50)]
    private float speed;

    [SerializeField]
    public static bool gotHit = false;

    [SerializeField]
    [Range(0, 5)]
    private float timer;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        float horizontalInput = Input.GetAxisRaw("Horizontal");

        if (gotHit == false)
        {
            rigbody.velocity = new Vector2(horizontalInput * speed,
                rigbody.velocity.y);
        }

        //Make score system

    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Obstacles"))
        {
            gotHit = true;
            rigbody.velocity = new Vector2(0, 0);
            Destroy(other.gameObject);
            StartCoroutine(HitTimer(timer));
        }
    }

    


    IEnumerator HitTimer(float duration)
    {
        yield return new WaitForSeconds(duration);
        gotHit = false;
    }

    //When player collides with rock, lock movement

}
