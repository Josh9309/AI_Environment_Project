using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Flockers : VehicleMovement
{


    // attribute
    public GameObject leaderFlocker; // use later
    private Vector3 tv;
    private int leaderBehindDist = 20;
    private Vector3 behind;
    // Seeker's steering force
    //private Vector3 force;

    private int pathCount;

    private List<GameObject> path;

    private Vector3 seekTarget;

    // WEIGHT
    //public float wanderWeight = 75.0f;
    private float safeDistance = 30.0f;
    private float seekWeight = 100.0f;
    private float avoidWeight = 240.0f;
    //private float stayInWeight = 200.0f;
    private float seperateWeight = 70.0f;
    private float alignmentWeight = 30.0f;
    private float cohesionWeight = 30.0f;
    private float queueingWeight = 40.0f;

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
        //Vector3 wonderF = base.Wander();
        //Debug.Log("wonderF: " + wonderF);
        // leader code
        //float leaderDis = (leaderFlocker.GetComponent<Leader>().transform.position - transform.position).magnitude;
        //if (leaderDis < leaderFlocker.GetComponent<Leader>().radius + radius)
        //{
        //    if (Vector3.Dot(transform.position, leaderFlocker.GetComponent<Leader>().transform.position) > 0)
        //    {
        //        // at right, steer left
        //        force += Seek(-transform.right);
        //    }
        //    else
        //    {
        //        // at left, steer right
        //        force += Seek(transform.right);
        //    }
        //}
        //force += LeaderFollow()* seekWeight;

        //pathfollowing
        //force += Seek(path[pathCount].transform.position) * seekWeight;
        //Debug.DrawLine(transform.position, path[pathCount].transform.position, Color.green);
        //float dis = (transform.position - path[pathCount].transform.position).magnitude;
        //// What to do with way point
        //if (dis < 5)
        //{
        //    //if (inward == true)
        //    //{
        //    //    pathCount++;
        //    //    if (pathCount >= path.Count - 1)
        //    //    {
        //    //        inward = false;
        //    //    }
        //    //}
        //    //else if (inward == false)
        //    //{
        //    //    pathCount--;
        //    //    if (pathCount <= 0)
        //    //    {
        //    //        inward = true;
        //    //    }
        //    //}
        //    pathCount++;
        //    if (pathCount >= path.Count - 1)
        //    {
        //        pathCount = 0;
        //    }
        //}

        force += Seek(gm.FlockSeekTarget) * seekWeight;
        // avoid obstacles
        for (int i = 0; i < gm.Obstacles.Length; i++)
        {
            force += AvoidObstacle(gm.Obstacles[i], safeDistance) * avoidWeight;
        }



        // call the flock method

        // limited the seeker's steering force

        // call the flock methods
        for (int i = 0; i < gm.Flock.Count; i++)
        {
            force += Separation()* seperateWeight;
        }
        force += Alignment(gm.FlockDirection)* alignmentWeight;
        force += Cohesion(gm.transform.position)* cohesionWeight;

        //queue along the way
        force += Queue() * queueingWeight;

        force = Vector3.ClampMagnitude(force, maxForce);
        force.y = 0;
        // applied the steering Force to this Vehicle's acceleration(ApplyForce);
        ApplyForce(force);

    }
    private Vector3 LeaderFollow()
    {
        tv = leaderFlocker.GetComponent<Leader>().Velocity * (-1);
        tv = tv.normalized * leaderBehindDist;
        behind = leaderFlocker.transform.position + tv;
        //Debug.Log(behind);
        return Arrive(behind);

    }

}
