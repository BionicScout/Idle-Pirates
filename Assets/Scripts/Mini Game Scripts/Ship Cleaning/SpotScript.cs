using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpotScript : MonoBehaviour
{

    [SerializeField]
    [Range(0f, 1f)]
    private float alphaReductionRate = 0.01f;

    [SerializeField]
    private Color32 newAlpha;

    [SerializeField]
    private ShipCleaningSceneManager manager;

    //[SerializeField]
    //private ParticleSystem explosion;

    // Start is called before the first frame update
    void Start()
    {
        newAlpha = this.GetComponent<Image>().color;

        manager = GameObject.FindObjectOfType<ShipCleaningSceneManager>();

    }

    // Update is called once per frame
    void Update()
    {
        if(this.GetComponent<Image>().color.a == 0)
        {
            
            manager.SpotCleanedUpdate();

            //Maybe create a particle to signal to spot is gone
            //Instantiate(explosion, transform.position, Quaternion.identity);

            
            Destroy(this.gameObject);
        }
    }


    public void OnSpotPressed()
    {
        newAlpha = new Color(this.GetComponent<Image>().color.r, 
            this.GetComponent<Image>().color.g,
            this.GetComponent<Image>().color.b, 
            this.GetComponent<Image>().color.a - alphaReductionRate);


        this.GetComponent<Image>().color = newAlpha;

        if(this.GetComponent<Image>().color.a == 0)
        {
            AudioManager.instance.Play("Spot Clean");
        }

    }

}
