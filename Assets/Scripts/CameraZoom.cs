using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour {

    public Camera cam;
    float currentZoomPos, zoomTo;
    float zoomFrom = 50f; // a starting positin
    Vector3 dragOrigin;
    // Move Camera
    float speed = 2.0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        // attaches float y to scrollwhell up and down
        float y = Input.mouseScrollDelta.y;
        // if goes up, decrement 10 from zoomto
        if (y >=1)
        {
            zoomTo -=4f;
        }
        else if(y <= -1)
        {
            zoomTo += 4f;
        }

        // create value to raise and lower camera field of view
        currentZoomPos = zoomFrom + zoomTo;
        currentZoomPos = Mathf.Clamp(currentZoomPos, -40f, 75f);

        /*

        // Math actual change to Field of View
        Camera.main.fieldOfView = currentZoomPos;

        // drag mouse
        if (Input.GetMouseButton(1))
        {
            dragOrigin = Input.mousePosition;
            Debug.Log("Move ");

            return;
        }
        if (!Input.GetMouseButton(1)) {
            dragOrigin = Vector3.zero;
                };
        Vector3 pos = cam.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
        Vector3 move = new Vector3(pos.x * speed, 0, pos.y * speed);
        //Debug.Log("Move " + move);

        transform.Translate(move, Space.World);
        */
    }
}
