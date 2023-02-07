using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoconutScript : MonoBehaviour
{
    [SerializeField]
    private int timeTillDespawn = 10;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ItemTimer(timeTillDespawn));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            Destroy(this.gameObject);

        }

        Debug.Log("Triggered");
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            Destroy(this.gameObject);
            
        }

        Debug.Log("Collided");
    }


    IEnumerator ItemTimer(int duration)
    {
        yield return new WaitForSeconds(duration);
        //this.gameObject.SetActive(false);
        Destroy(this.gameObject);
    }

}
