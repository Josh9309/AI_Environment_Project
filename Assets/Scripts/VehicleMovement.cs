using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Vehicle movement class - 
/// Provides basic steering force movement for other classes to expand upon
/// </summary>
public abstract class VehicleMovement : MonoBehaviour {

    // Access to GameManager scriot
    protected GameManager gm;
    // Properties
    public Vector3 Velocity
    {
        get { return velocity; }
    }

    // Private fields
    protected Vector3 velocity;
    protected Vector3 acceleration;
    protected Vector3 direction;
    protected Vector3 force;
    protected Vector3 desired;
    protected Vector3 previousPosition;
    protected GameObject[] flock;
    protected Vector3 queueFuturePoint;
    [SerializeField] protected const float MAX_QUEUE_AHEAD_DIST = 8f;
    [SerializeField] protected const float MAX_QUEUE_RADIUS = 2.40f;
    // Public fields
    [Tooltip("Maximum speed of the character")]
    public float maxSpeed = 6.0f;
    [Tooltip("Max force of the character (related to acceleration)")]
    public float maxForce = 12.0f;
    [Tooltip("Mass of the character based on an arbitrary scale")]
    public float mass = 1.0f;
    [Tooltip("Radius of the character, used with terrain snapping among other things")]
    public float radius = 1.0f;
    [Tooltip("How far to turn away from obstacles")]
    public float avoidObstacleNormalLength = 7.0f;
    [Tooltip("Distance for separation in flockers")]
    public float tooCloseDist = 4000.0f;

    #region Unity Defaults
    virtual public void Start () {
        flock = GameObject.FindGameObjectsWithTag("Flocker");
        acceleration = Vector3.zero;
        velocity = transform.forward;
        gm = GameObject.Find("GameManagerGO").GetComponent<GameManager>();
    }
	
	void Update () {
        // Calculate steering forces - varies depending on implementation
        CalculateSteeringForces();
        // Add acceleration to velocity
        velocity += acceleration * Time.deltaTime;
        // Limit velocity based on max speed
        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
        // Set forward to direction - note: throws a warning when velocity is zero
        transform.forward = velocity.normalized;
        // Update previous position
        previousPosition = this.transform.position;
        // Move character
        //this.transform.position += velocity * Time.deltaTime; // old way of doing things
        this.GetComponent<Rigidbody>().MovePosition(this.transform.position += velocity * Time.deltaTime);
        //this.GetComponent<Rigidbody>().velocity = this.velocity * Time.deltaTime; //THIS DOES NOT WORK! 'velocity' is a unit vector
        this.GetComponent<Rigidbody>().AddForce(this.transform.up * -9.81f);
        // Reset acceleration
        acceleration = Vector3.zero;
    }

    void OnCollisionEnter(Collision collision)
    {
        // Apply a force to push you out of walls
        this.Seek(this.transform.position - collision.transform.position);
    }
    #endregion

    #region Vehicle Methods
    /// <summary>
    /// Implement this in classes that use the Vehicle movement pattern
    /// </summary>
    abstract protected void CalculateSteeringForces();

    /// <summary>
    /// Simple method to apply forces
    /// </summary>
    protected void ApplyForce(Vector3 force)
    {
        acceleration += force / mass;
    }

    /// <summary>
    /// Most basic steering method - seek towards a point
    /// </summary>
    protected Vector3 Seek(Vector3 targetLoc)
    {
        desired = targetLoc - this.transform.position;
        desired = desired.normalized * maxSpeed;
        desired -= velocity;
        return desired;
    }

    //protected Vector3 AvoidObstacle(float safe)
    //{
    //    RaycastHit hitInfo;
    //    if (Physics.Raycast(this.transform.position, this.transform.forward, out hitInfo, safe))
    //        return Seek(hitInfo.transform.position + (hitInfo.normal * avoidObstacleNormalLength));
    //    return Vector3.zero;
    //}
    protected Vector3 AvoidObstacle(GameObject ob, float safe)
    {
        // reset desired 
        desired = Vector3.zero;
        // get radius from obstacle
        float obsR = ob.GetComponent<ObstacleScript>().Radius;
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
    #endregion

    #region Flocking Methods
    // Alignment method
    protected Vector3 Alignment(Vector3 flockDirection)
    {
        desired = flockDirection - this.transform.forward;
        desired = desired.normalized * maxSpeed;
        desired -= velocity;
        return desired;
    }
     // Cohesion method
    protected Vector3 Cohesion(Vector3 centroidLocation)
    {
        desired = centroidLocation - this.transform.position;
        desired = desired.normalized * maxSpeed;
        desired -= velocity;
        return desired;
    }
    // Separation method    
    protected Vector3 Separation()
    {
        Vector3 sumVel = Vector3.zero;
        float tempDist;
        foreach (GameObject g in flock)
        {
            if (g != this.gameObject)
            {
                tempDist = Vector3.Distance(this.gameObject.transform.position, g.transform.position);

                //Debug.Log("TooCose Dis: "+ tooCloseDist + " Distance Now: " + tempDist);
                if (tempDist < tooCloseDist)
                {
                    sumVel += ((g.transform.position - transform.position) * -1) * maxSpeed * (1 / tempDist);
                }
            }
        }
        sumVel = sumVel.normalized * maxSpeed;
        sumVel = sumVel - velocity;
        return sumVel;
    }
    //Arrivial Method
    protected Vector3 Arrive(Vector3 target)
    {
        desired = target - transform.position;
        float d = desired.magnitude;

        if (d < 5)
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
    //Queueing
    protected Vector3 Queue()
    {
        Vector3 QueueAhead = velocity.normalized * MAX_QUEUE_AHEAD_DIST;
        queueFuturePoint = QueueAhead + transform.position;

        Debug.DrawLine(transform.position, queueFuturePoint, Color.red);
        GameObject flockerAhead = null;

        for (int i = 0; i < flock.Length; i++)
        {
            GameObject flocker = flock[i];
            float dist = (flocker.transform.position - queueFuturePoint).magnitude;

            if(flocker != this && dist <= MAX_QUEUE_RADIUS)
            {
                flockerAhead = flocker;
                break;
            }
        }
        
        if(flockerAhead != null)
        {
            //take action because their is a flocker ahead of you
            velocity.Scale(new Vector3(0.3f, 0.3f, 0.3f));
            Debug.Log("Queue applied");
        }

        return Vector3.zero;
    }
    
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(queueFuturePoint, new Vector3(MAX_QUEUE_RADIUS, MAX_QUEUE_RADIUS, MAX_QUEUE_RADIUS));
    }
    #endregion
}