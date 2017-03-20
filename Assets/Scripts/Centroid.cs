using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Centroid : MonoBehaviour {

    private GameManager gm;
	// Use this for initialization
	void Start () {
        gm = GameObject.Find("GameManagerGO").GetComponent<GameManager>();
        transform.position = gm.Centroid;
        transform.forward = gm.FlockDirection;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = gm.Centroid;
        transform.forward = gm.FlockDirection;
    }
}
