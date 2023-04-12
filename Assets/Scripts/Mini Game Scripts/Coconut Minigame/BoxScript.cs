using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour
{
    [SerializeField]
    private CoconutSceneManager coconutSceneManager;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            AudioManager.instance.Play("Coconut Catch");
            Destroy(collision.gameObject);
            coconutSceneManager.CoconutGatheredUpdate();

        }

    }

}
