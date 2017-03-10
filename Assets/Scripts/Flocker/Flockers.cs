using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Flockers : VehicleMovement
{


    // attribute
    public GameObject leaderFlocker; // use later
    private Vector3 tv;
    private int leaderBehindDist = 1;
    private Vector3 behind;
    // Seeker's steering force
    //private Vector3 force;



    // WEIGHT
    //public float wanderWeight = 75.0f;
    public float safeDistance = 100.0f;
    public float seekWeight = 90.0f;
    public float avoidWeight = 240.0f;
    public float stayInWeight = 200.0f;
    public float seperateWeight = 40.0f;
    public float alignmentWeight = 20.0f;
    public float cohesionWeight = 20.0f;

    // Use this for initialization
    override public void Start()
    {
        // call parent's start
        base.Start();

        // initialize
        force = Vector3.zero;
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
        float leaderDis = (leaderFlocker.GetComponent<Leader>().transform.position - transform.position).magnitude;
        if (leaderDis < leaderFlocker.GetComponent<Leader>().radius + radius)
        {
            if (Vector3.Dot(transform.position, leaderFlocker.GetComponent<Leader>().transform.position) > 0)
            {
                // at right, steer left
                force += Seek(-transform.right);
            }
            else
            {
                // at left, steer right
                force += Seek(transform.right);
            }
        }
        force += LeaderFollow()* seekWeight;

        // avoid obstacles
        for (int i = 0; i < gm.Obstacles.Length; i++)
        {
            force += AvoidObstacle(gm.Obstacles[i], safeDistance) * avoidWeight;
        }

        // add the obstacle force to it

        //force += AvoidObstacle(safeDistance) * avoidWeight;

        // call the flock method

        // limited the seeker's steering force

        // call the flock methods
        for (int i = 0; i < gm.Flock.Count; i++)
        {
            force += Separation()* seperateWeight;
        }
        force += Alignment(gm.FlockDirection)* alignmentWeight;
        force += Cohesion(gm.transform.position)* cohesionWeight;

        force = Vector3.ClampMagnitude(force, maxForce);

        // applied the steering Force to this Vehicle's acceleration(ApplyForce);
        ApplyForce(force);

    }
    private Vector3 LeaderFollow()
    {
        tv = leaderFlocker.GetComponent<Leader>().Velocity * (-1);
        tv = tv.normalized * leaderBehindDist;
        behind = leaderFlocker.transform.position + tv;
        Debug.Log(behind);
        return Arrive(behind);

    }

}
