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
    public static bool boostedUp = false;

    [SerializeField]
    [Range(0, 5)]
    private float stopTimer;

    [SerializeField]
    [Range(0, 5)]
    private float boostTimer;


    // Start is called before the first frame update
    void Start()
    {
        gotHit = false;
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
            StartCoroutine(HitTime(stopTimer));
        }

        if (other.gameObject.CompareTag("Booster") && boostedUp == false)
        {
            boostedUp = true;
            //rigbody.velocity = new Vector2(0, 0);
            Destroy(other.gameObject);

            StartCoroutine(BoostTime(stopTimer));
        }

        if (other.gameObject.CompareTag("Booster") && boostedUp == true)
        {
            
            //rigbody.velocity = new Vector2(0, 0);
            Destroy(other.gameObject);
            //StopCoroutine(BoostTime(stopTimer));
            StopAllCoroutines();
            StartCoroutine(BoostTime(stopTimer));
        }
        //StopCoroutine
    }

    


    IEnumerator HitTime(float duration)
    {
        yield return new WaitForSeconds(duration);
        gotHit = false;
    }

    IEnumerator BoostTime(float duration)
    {
        yield return new WaitForSeconds(duration);
        boostedUp = false;
    }



    //When player collides with rock, lock movement

}
