using UnityEngine;
using System.Collections;

//add using System.Collections.Generic; to use the generic list format
using System.Collections.Generic;
public class GameManager : MonoBehaviour {


    /// <summary>
    ///  Flock
    /// </summary>
    // center of floack
    private Vector3 centroid;

    public Vector3 Centroid
    {
        get { return centroid; }
    }

    // average flock direction
    private Vector3 flockDirection;

    public Vector3 FlockDirection
    {
        get { return flockDirection; }
    }


    // List
    public List<GameObject> flock;
    public List<GameObject> Flock
    {
        get { return flock; }
    }

    private GameObject[] obstacles;
    public GameObject[] Obstacles
    {
        get { return obstacles; }
    }

    // Need flockers

    //----------------------------------------------------
    // start and Update
    //-------------------------------------------------------
    void Start()
    {
        obstacles = GameObject.FindGameObjectsWithTag("Obstacles");


    }

    void Update()
    {   
        // call the centroid and direction method
        CalcCentroid();
        CalcDirection();
    }



    //-----------------------------------------------------------------------
    // Flocking Methods
    //-----------------------------------------------------------------------
    void CalcCentroid()
    {
        // not sure
        for (int i = 0; i < flock.Count; i++)
        {
            centroid = centroid + flock[i].transform.position;
        }
        //Debug.Log("centroid: " + centroid);
        centroid = centroid / flock.Count;

    }
    void CalcDirection()
    {
        // not sure
        for (int i = 0; i < flock.Count; i++)
        {
            flockDirection = flockDirection + flock[i].transform.forward;
        }
        flockDirection = flockDirection / flock.Count;
    }



}
