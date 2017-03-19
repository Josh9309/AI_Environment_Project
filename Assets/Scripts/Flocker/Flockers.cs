using UnityEngine;
using UnityEngine.UI;
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
    private float safeDistance = 20.0f;
    private float seekWeight = 100.0f;
    private float avoidWeight = 240.0f;
    //private float stayInWeight = 200.0f;
    private float seperateWeight = 60.0f;
    private float alignmentWeight = 30.0f;
    private float cohesionWeight = 20.0f;
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
        if (gm.applySeek.isOn)
        {
            force += Seek(gm.FlockSeekTarget) * seekWeight;
        }

        // avoid obstacles
        if (gm.applyAvoid.isOn)
        {
            for (int i = 0; i < gm.Obstacles.Length; i++)
            {
                force += AvoidObstacle(gm.Obstacles[i], safeDistance) * avoidWeight;
            }
        }


        // call the flock method

        // limited the seeker's steering force

        // call the flock methods
        if (gm.applySeperation.isOn)
        {
            for (int i = 0; i < gm.Flock.Count; i++)
            {
                force += Separation() * seperateWeight;
            }
        }
        if (gm.applyAlignment.isOn)
        {
            force += Alignment(gm.FlockDirection) * alignmentWeight;
        }
        if (gm.applyCohesion.isOn)
        {
            force += Cohesion(gm.transform.position) * cohesionWeight;
        }


        //queue along the way
        if (gm.applyQueue.isOn)
        {
            force += Queue() * queueingWeight;
        }

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
