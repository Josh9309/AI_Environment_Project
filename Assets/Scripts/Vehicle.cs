﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
abstract public class Vehicle : MonoBehaviour
{
    // Movement attribute
    protected Vector3 acceleration;
    protected Vector3 velocity;
    protected Vector3 desired;

    private Vector3 futurePos;

    // wander attributes
    // wander
    protected Vector3 circleCenter;
    protected float circleDistance;
    protected float circleRadius;
    protected Vector3 displacement;
    protected float wanderAngle;


    // Access to GameManager scriot
    protected GameManager gm;

    // Properties
    public Vector3 Velocity
    {
        get { return velocity; }
    }

    // public for changing in Inspector
    public float maxSpeed = 6.0f;
    public float maxForce = 12.0f;
    public float mass = 1.0f;
    public float radius = 2.0f;

    // access to Character Controller component
    CharacterController charControl;

    abstract protected void CalcSteeringForces();

    //-----------------------------------------------------------------------
    // Start and Update
    //-----------------------------------------------------------------------

    // Use this for initialization
    virtual public void Start()
    {
        acceleration = Vector3.zero;
        velocity = transform.forward;
        charControl = GetComponent<CharacterController>();
        //gm = GameObject.Find("GameManagerGO").GetComponent<GameManager>();
        futurePos = Vector3.zero;

        circleDistance = 60.0f;
        circleRadius = 16.0f;
    }

    // Update is called once per frame
    void Update()
    {
        // calculate all necessary steering forces
        CalcSteeringForces();

        // add accel to vel
        velocity += acceleration * Time.deltaTime;
        velocity.y = 0; // keep on the same plane

        // limit vel to max speed
        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);

        // turn dude toward the forward
        transform.forward = velocity.normalized;
        // move the character based on velocity
        charControl.Move(velocity * Time.deltaTime);

        // replace velocity with rigid body
        //Regidbody.velocity Time.deltaTime;
        // maybe use rigid body


        // rreset acceleration to 0l
        acceleration = Vector3.zero;
        // calculate future pos;
        futurePos = transform.position + transform.forward * 5;

        Debug.DrawLine(transform.position, futurePos, Color.black);
    }

    // Class method
    protected void ApplyForce(Vector3 steeringForce)
    {
        acceleration += steeringForce / mass;
    }

    protected Vector3 Seek(Vector3 targetPos)
    {
        desired = targetPos - transform.position;
        desired = desired.normalized * maxSpeed;
        desired -= velocity;
        desired.y = 0;
        return desired;
    }
    protected Vector3 Arrive(Vector3 target)
    {
        desired = target - transform.position;
        float d = desired.magnitude;

        if (d < 10)
        {
            desired = desired.normalized * maxSpeed * (d / 1); // need to change
        }
        else
        {
            desired = desired.normalized * maxSpeed;

        }
        desired -= velocity;
        desired.y = 0;
        return desired;
    }


    protected Vector3 AvoidObstacle(GameObject ob, float safe)
    {

        // reset desired 
        desired = Vector3.zero;

        // get radius from obstacle
        float obsR = 0;// ob.GetComponent<ObstacleScript>().Radius;

        // get vector from vehicle to obstacle
        Vector3 vecToCenter = ob.transform.position - transform.position;

        // zero-out y component 
        vecToCenter.y = 0;

        // ignore object out of safe zone
        if (vecToCenter.magnitude > safe)
        {
            return Vector3.zero;
        }

        // ignore obj behind me
        if (Vector3.Dot(vecToCenter, transform.forward) < 0)
        {
            return Vector3.zero;
        }

        // ignore obj not in forward path
        if (Mathf.Abs(Vector3.Dot(vecToCenter, transform.right)) > obsR + radius)
        {
            return Vector3.zero;
        }

        // collide with an obstacle
        // obj on left, steer right
        if (Vector3.Dot(vecToCenter, transform.right) < 0)
        {
            desired = transform.right * maxSpeed;
            // debug line 
            Debug.DrawLine(transform.position, ob.transform.position, Color.red);
        }
        else
        {
            desired = transform.right * -maxSpeed;
            // debug line
            Debug.DrawLine(transform.position, ob.transform.position, Color.green);
        }

        return desired;
    }

    protected Vector3 Wander()
    {
        // change wanderAngle 
        wanderAngle += Random.Range(-0.25f, 0.25f); // notsure

        // Circle position
        circleCenter = velocity;
        circleCenter = circleCenter.normalized;
        circleCenter = circleCenter * circleDistance;
        circleCenter = circleCenter + transform.position; // not sure 

        /////////displacement Force
        //displacement = new Vector3(0,0, -1);
        //displacement = circleCenter + transform.position ;

        // randomly change the vector direction
        // displacement = Quaternion.Euler(wanderAngle,0, wanderAngle)*displacement; //// Not sure
        setAngle(displacement, wanderAngle);


        //Debug.Log("wanderAngle: " + wanderAngle);

        //Debug.Log("displacement: " + displacement);

        //displacement = displacement ;
        //displacement = displacement.normalized;

        // calculate and retun the wander force
        Vector3 wanderForce = circleCenter + displacement;
        return wanderForce;
    }

    protected void setAngle(Vector3 dis, float angle)
    {
        dis.x = Mathf.Cos(angle);
        dis.z = Mathf.Sin(angle);
        displacement = dis;
    }

    // Flocking Code

    protected Vector3 Seperation(GameObject nb, float closeDis)
    {
        // reset desired
        desired = Vector3.zero;

        // get vector from this vehicles to the neighbor
        Vector3 vecToCenter = nb.transform.position - transform.position;

        // zero out y component
        vecToCenter.y = 0;

        // ignore object out of safe zone
        if (vecToCenter.magnitude > closeDis)
        {
            return Vector3.zero;
        }
        else
        {
            return -1 * Seek(nb.transform.position);
        }

    }

    protected Vector3 Alignment(List<GameObject> flockers)
    {
        Vector3 forwardSum = Vector3.zero;

        for (int i = 0; i < flockers.Count; i++)
        {
            forwardSum = forwardSum + flockers[i].transform.forward;
        }
        // compute desired Velocity
        Vector3 desiredVelocity = forwardSum.normalized * maxSpeed;

        return desiredVelocity - velocity;
    }

    protected Vector3 Cohesion(List<GameObject> flockers)
    {
        Vector3 positionSum = Vector3.zero;

        for (int i = 0; i < flockers.Count; i++)
        {
            positionSum = positionSum + flockers[i].transform.position;
        }
        Vector3 averagePos = positionSum / flockers.Count;
        return Seek(averagePos);
    }
}
