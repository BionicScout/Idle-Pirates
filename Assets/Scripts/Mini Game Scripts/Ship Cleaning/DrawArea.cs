using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawArea : MonoBehaviour
{
    public RectTransform areaRect;  // Reference to the RectTransform component of the area
    public Image areaImage;         // Reference to the Image component of the area

    void Start()
    {
        // Set the size and position of the area
        areaRect.sizeDelta = new Vector2(200f, 100f);
        areaRect.anchoredPosition = new Vector2(0f, 0f);

        // Set the color and thickness of the border
        areaImage.color = Color.blue;
        areaImage.type = Image.Type.Sliced;
        areaImage.fillCenter = false;
        areaImage.fillAmount = 1f;
        areaImage.fillOrigin = 0;
        areaImage.fillClockwise = true;
    }
}
