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
        Vector3 totalPosition = Vector3.zero;

        for (int i = 0; i < flock.Count; i++)
        {
            totalPosition += flock[i].transform.position;
        }
        Debug.Log("totalPosition: " + totalPosition);

        Debug.Log("centroid: " + centroid);
        centroid = totalPosition / flock.Count;
    }
    void CalcDirection()
    {
        // not sure
        Vector3 totalPosition = Vector3.zero;
        for (int i = 0; i < flock.Count; i++)
        {
            totalPosition += flock[i].transform.forward;
        }
        flockDirection = totalPosition / flock.Count;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(Centroid, 2f);
        Gizmos.DrawLine(centroid, centroid + flockDirection);
    }


}
