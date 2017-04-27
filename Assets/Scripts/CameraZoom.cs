using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour {

    public Camera cam;
    float currentZoomPos, zoomTo;
    float zoomFrom = 50f; // a starting positin

    // Move Camera
    float speed = 5.0f;
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

        zoomTo = Mathf.Clamp(zoomTo, -45f, 80f);

        // create value to raise and lower camera field of view
        currentZoomPos = zoomFrom + zoomTo;
        currentZoomPos = Mathf.Clamp(currentZoomPos, -40f, 75f);

        

        // Math actual change to Field of View
        cam.fieldOfView = currentZoomPos;
        
        // move Camera

        if(Input.GetKey(KeyCode.W))
        {
            transform.Translate(new Vector3(speed, 0,0), Space.World);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(new Vector3(-speed, 0, 0), Space.World);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(new Vector3(0, 0, -speed), Space.World);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(new Vector3(0, 0, speed), Space.World);
        }

        return;

        
    }
}
