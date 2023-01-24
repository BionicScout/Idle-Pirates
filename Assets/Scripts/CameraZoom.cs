using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour {
//Map Info
    [SerializeField]
    private Transform map;
    Vector3 minScale;
    Vector2 pixelRatio = new Vector2(1920, 1080);

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

    Vector2 lastMosueFrame;
    Vector2 currentMouseFrame;

    void Start() {
        minScale = map.localScale;
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
        //Get Direction to Move map
            lastMosueFrame = currentMouseFrame;
            currentMouseFrame = cam.ScreenToWorldPoint(Input.mousePosition);
            Vector3 dirrection = lastMosueFrame - currentMouseFrame;

        //Move the Map Pos
            map.position += grabSpeed * -1 * dirrection;
        }

        ClampMap();
    }

    /*
        ClampMap garantees that the map always on the screen and that the player can't look past the map edge. The area the map
        is clamped to is determied by zoomLevel and where the camera is. 
    */
    void ClampMap() {
        Vector2 maxMove = ((zoomLevel - 1) / 2f) * pixelRatio;

        float clamped_X = Mathf.Clamp(map.position.x, (pixelRatio.x / 2f) - maxMove.x, (pixelRatio.x / 2f) + maxMove.x);
        float clamped_Y = Mathf.Clamp(map.position.y, (pixelRatio.y / 2f) - maxMove.y, (pixelRatio.y / 2f) + maxMove.y);

        map.position = new Vector3(clamped_X, clamped_Y, map.position.z);
    }
}