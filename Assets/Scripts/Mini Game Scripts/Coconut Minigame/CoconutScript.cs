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
        AudioManager.instance.Play("Coconut Fall");
        StartCoroutine(ItemTimer(timeTillDespawn));
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    IEnumerator ItemTimer(int duration)
    {
        yield return new WaitForSeconds(duration);
        //this.gameObject.SetActive(false);
        Destroy(gameObject);
    }

}
