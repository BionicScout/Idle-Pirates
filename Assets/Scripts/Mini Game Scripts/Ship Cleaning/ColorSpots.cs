using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorSpots : MonoBehaviour
{
    public RenderTexture image;
    public GameObject spotPrefab;
    public int numberOfSpots = 10;
    public float spotSize = 10f;
    public Vector2 minPosition = Vector2.zero;
    public Vector2 maxPosition = new Vector2(1, 1);
    public Color color;
    public Vector2 tiling = new Vector2(1, 1);
    public Vector2 offset = Vector2.zero;

    private Image imageComponent;

    void Start()
    {
        // Calculate the number of color spots to generate
        int numSpots = Mathf.Max(1, numberOfSpots);

        // Instantiate the color spot GameObjects
        for (int i = 0; i < numSpots; i++)
        {
            GameObject spot = Instantiate(spotPrefab, transform);

            // Set the position and color of the color spot
            float x = Random.Range(minPosition.x, maxPosition.x);
            float y = Random.Range(minPosition.y, maxPosition.y);
            spot.transform.localPosition = new Vector3(x, y, 0);
            spot.GetComponent<MeshRenderer>().material.color = color;

            // Create a new Material instance for the color spot
            Material newMaterial = new Material(Shader.Find("UI/Default"));

            // Set the color and tiling of the Material
            newMaterial.color = color;
            newMaterial.SetTextureScale("_MainTex", tiling);

            // Assign the Material to the color spot
            spot.GetComponent<MeshRenderer>().material = newMaterial;
        }

        // Get the Image component of the GameObject with this script
        imageComponent = GetComponent<Image>();

        // Set the "image" variable to the Image component
        image = imageComponent.mainTexture as RenderTexture;

        // Set the "material" property of the Image component to the Material of the first color spot
        if (transform.childCount > 0)
        {
            Material firstMaterial = transform.GetChild(0).GetComponent<MeshRenderer>().material;
            imageComponent.material = firstMaterial;
        }

        // Set the tiling and offset of the Material
        imageComponent.material.SetTextureScale("_MainTex", tiling);
        imageComponent.material.SetTextureOffset("_MainTex", offset);
    }
}
