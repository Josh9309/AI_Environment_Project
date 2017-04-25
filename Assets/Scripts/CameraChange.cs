using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChange : MonoBehaviour {

    [Tooltip("Add cameras to this list to be cycled through.")]
    public GameObject[] cameras;

    private int mainCamNum = 0;
	
    public int MainCamNum
    {
        get { return mainCamNum; }
    }
	void Update () {
        //Find which camera should be used
        if (Input.GetKeyDown(KeyCode.Alpha1))
            mainCamNum = 0;
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            mainCamNum = 1;
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            mainCamNum = 2;
        else if (Input.GetKeyDown(KeyCode.Alpha4))
            mainCamNum = 3;
        else if (Input.GetKeyDown(KeyCode.Alpha5))
            mainCamNum = 4;
        else if (Input.GetKeyDown(KeyCode.Alpha6))
            mainCamNum = 5;
        else if (Input.GetKeyDown(KeyCode.Alpha7))
            mainCamNum = 6;
        else if (Input.GetKeyDown(KeyCode.Alpha8))
            mainCamNum = 7;

        //Disable all cameras
        for (int i = 0; i < cameras.Length; ++i)
        {
            cameras[i].GetComponent<Camera>().enabled = false;
            cameras[i].GetComponent<Camera>().tag = "DisabledCamera";
        }
        //Enable selected camera
        cameras[mainCamNum].GetComponent<Camera>().enabled = true;
        cameras[mainCamNum].GetComponent<Camera>().tag = "MainCamera";
    }
}
