using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EraserCursor : MonoBehaviour
{
    public Image eraserImage;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(eraserImage.sprite.texture, Vector2.zero, CursorMode.Auto);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
