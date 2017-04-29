using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reset : MonoBehaviour {

    GameObject[] units;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space))
        {
            resetUnit();
            resetGrid();
        }
	}


    public void resetUnit()
    {
        units = GameObject.FindGameObjectsWithTag("unit");

        for(var i = 0; i < units.Length; i ++)
        {
            Destroy(units[i]);
        }
    }

    public void resetGrid()
    {
        // Not sure what to do yet
    }
}
