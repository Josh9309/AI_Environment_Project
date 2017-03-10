using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Leader : VehicleMovement {


    // attribute
    // Seeker's steering force
   // private Vector3 force;
    private int pathCount;
    
    private List<GameObject> path;
    private bool inward = true;


    // WEIGHT
    public float seekWeight = 75.0f;
    public float safeDistance = 100.0f;
    public float avoidWeight = 240.0f;
    public float seperateSafeDis = 5.0f; // not sure if i need this or not;
    // Use this for initialization
    override public void Start()
    {
        // call parent's start
        base.Start();

        // initialize
        force = Vector3.zero;
        pathCount = 0;
        path = GameObject.Find("PathManager").GetComponent<AStarManager>().FlockingPathList;
    }

    // Class Method
    protected override void CalculateSteeringForces()
    {

        // reset value to zero
        force = Vector3.zero;

        // FORCE

        // Path following

        //Debug.Log("path following");
        force += Seek(path[pathCount].transform.position) * seekWeight;
        float dis = (transform.position - path[pathCount].transform.position).magnitude;
        // What to do with way point
        if (dis < 5)
        {
            //if (inward == true)
            //{
            //    pathCount++;
            //    if (pathCount >= path.Count - 1)
            //    {
            //        inward = false;
            //    }
            //}
            //else if (inward == false)
            //{
            //    pathCount--;
            //    if (pathCount <= 0)
            //    {
            //        inward = true;
            //    }
            //}
                pathCount++;
                if (pathCount >= path.Count - 1)
                {
                    pathCount = 0;
                }
        }
        // got a seeking force
        //force += Seek(Vector3.zero) * seekWeight;


        // add the obstacle force
        // add the obstacle force to it
        for (int i = 0; i < gm.Obstacles.Length; i++)
        {
            force += AvoidObstacle(gm.Obstacles[i],safeDistance) * avoidWeight;
        }
        // call the flock method

        // limited the seeker's steering force
        force = Vector3.ClampMagnitude(force, maxForce);

        // applied the steering Force to this Vehicle's acceleration(ApplyForce);
        ApplyForce(force);

    }

}
