using UnityEngine;
using System.Collections;

/// <summary>
/// Makes the camera orbit around an object in a third person perspective
/// </summary>
public class CameraOrbit : MonoBehaviour {

	#region Fields
	public Transform orbitPoint; //The transform to orbit around
	public float dist = 25f;    //Starting distance from the point
	public float sensitivity = 4f;  //How quickly the camera moves when you move the mouse
	public float minDist = 10f; //The closest you can get to the orbit point
	public float maxDist = 50f; //The furthest you can get away from the orbit point
    public float zoomIncrement = 5f;    //How quickly the scroll wheel zooms in
	private Vector2 mousePos;   //Used to get mouse input
    #endregion

	void LateUpdate()
	{
        //Get input data from the mouse (i.e. movement this frame)
		mousePos = new Vector2 (mousePos.x + Input.GetAxis ("Mouse X") * sensitivity * dist * Time.deltaTime, mousePos.y + Input.GetAxis ("Mouse Y") * dist * sensitivity * Time.deltaTime);
        //Get a rotation based on the mouse movement
		Quaternion rot = Quaternion.Euler (-mousePos.y, mousePos.x, 0);
        //Update the distance from the centroid based on the mouse wheel
		dist = Mathf.Clamp (dist - Input.GetAxis ("Mouse ScrollWheel") * zoomIncrement, minDist, maxDist);
        //Get a vector for the distance to move back the camera
		Vector3 negDist = new Vector3 (0f, 0f, -dist);
        //Use that vector to figure out where the camera's location should be
		Vector3 pos = rot * negDist + orbitPoint.position;
        //Actually update the camera's transform
		this.transform.rotation = rot;
        this.transform.position = pos;
        
        //Make sure the camera isn't inside a wall using raycasting
		RaycastHit hit;
        if (Physics.Raycast(orbitPoint.position, (this.transform.position - orbitPoint.position).normalized, out hit, dist, 8))
        {
            //Snap the camera to the wall if it got stuck in one
			transform.position = hit.point;
		}
	}
}