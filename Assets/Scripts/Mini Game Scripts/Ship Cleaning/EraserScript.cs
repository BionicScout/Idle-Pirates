using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EraserScript : MonoBehaviour
{
    

    [SerializeField]
    private bool mouseButtonDown = false;

    [SerializeField]
    [Range(0f, 1f)]
    private float alphaReductionRate = 0.01f;

    [SerializeField]
    private float minAlpha = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            mouseButtonDown = true;


        }
        else
        {
            mouseButtonDown = false;
        }


        if(mouseButtonDown == true) 
        {
            

        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Button"))
        {
            if (mouseButtonDown == true)
            {
                Image buttonImage = collision.gameObject.GetComponent<Image>();
                Color imageColor = buttonImage.color;
                imageColor.a -= alphaReductionRate;
                if (imageColor.a < minAlpha)
                {
                    imageColor.a = minAlpha;
                }
                buttonImage.color = imageColor;
            }
        }
    }


}
