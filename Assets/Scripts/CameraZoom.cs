using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour {
    // Movement based Scroll Wheel Zoom.
    [SerializeField]
    private Transform map;
    Vector3 minScale;

    //Zoom Level
    [SerializeField]
    private float zoomLevel = 1;

    [SerializeField]
    private float sensitivity = 1;

    [SerializeField]
    private float maxZoom = 30;

    //Move Map
    [SerializeField]
    private Camera cam;

    [SerializeField]
    private float grabSpeed = 10;

    Vector2 startPos;
    Vector2 lastMosueFrame;
    Vector2 currentMouseFrame;

    Vector2 pixelRatio = new Vector2(1920, 1080);

    void Start() {
        minScale = map.localScale;
        startPos = map.position;
    }

    void Update() {
    //Control Zoom Level
        zoomLevel += Input.mouseScrollDelta.y * sensitivity;
        zoomLevel = Mathf.Clamp(zoomLevel, 1, maxZoom);

        map.localScale = minScale * zoomLevel;

    //Move Map
        if(Input.GetKeyDown(KeyCode.Mouse0)) { //Update to Input Manger
            currentMouseFrame = cam.ScreenToWorldPoint(Input.mousePosition);
        }
        else if(Input.GetKey(KeyCode.Mouse0)) { //Update to Input Manger
            lastMosueFrame = currentMouseFrame;
            currentMouseFrame = cam.ScreenToWorldPoint(Input.mousePosition);
            Vector3 dirrection = lastMosueFrame - currentMouseFrame;

            map.position += grabSpeed * -1 * dirrection;
        }

        ClampMap();
    }

    void ClampMap() {
        Vector2 maxMove = ((zoomLevel - 1) / 2f) * pixelRatio;

        float clamped_X = Mathf.Clamp(map.position.x, (pixelRatio.x / 2f) - maxMove.x, (pixelRatio.x / 2f) + maxMove.x);
        float clamped_Y = Mathf.Clamp(map.position.y, (pixelRatio.y / 2f) - maxMove.y, (pixelRatio.y / 2f) + maxMove.y);

        map.position = new Vector3(clamped_X, clamped_Y, map.position.z);
    }
}