using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstBackgroundMovement : MonoBehaviour
{

    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    public float speed = -3f;


    [SerializeField]
    private float disappearTime;

    [SerializeField]
    public int disappearMultiplier = 3;


    [SerializeField]
    private float timer = 0;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        disappearTime = -(speed) * disappearMultiplier;

    }

    // Update is called once per frame
    void Update()
    {
        if (MiniGameShipMovement.gotHit == false)
        {
            if (timer < disappearTime)
            {
                timer += Time.deltaTime;

            }
            else
            {
                Destroy(this.gameObject);

            }
        }


        if (MiniGameShipMovement.gotHit == true)
        {
            rb.velocity = new Vector2(0, 0);
        }
        else
        {
            rb.velocity = new Vector2(0, speed);
        }
    }
}
