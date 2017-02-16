using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Vehicle movement class - 
/// Provides basic steering force movement for other classes to expand upon
/// </summary>
public abstract class VehicleMovement : MonoBehaviour {

    // Private fields
    protected Vector3 velocity;
    protected Vector3 acceleration;
    protected Vector3 direction;
    protected Vector3 force;
    protected Vector3 desired;
    // Public fields
    [Tooltip("Maximum speed of the character")]
    public float maxSpeed = 6.0f;
    [Tooltip("Max force of the character (related to acceleration)")]
    public float maxForce = 12.0f;
    [Tooltip("Mass of the character based on an arbitrary scale")]
    public float mass = 1.0f;
    [Tooltip("Radius of the character, used with terrain snapping among other things")]
    public float radius = 1.0f;

    #region Unity Defaults
    virtual public void Start () {
        acceleration = Vector3.zero;
        velocity = transform.forward;
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
        // Move character - note: uses simple addition to position, perhaps not the best way of doing this
        this.transform.position += velocity * Time.deltaTime;
        // Reset acceleration
        acceleration = Vector3.zero;
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

    protected void SnapToTerrain()
    {
        this.transform.position = new Vector3(this.transform.position.x, Terrain.activeTerrain.SampleHeight(this.transform.position) + this.radius, this.transform.position.z);
    }
    #endregion

    #region Flocking Methods
    /* // Alignment method
    protected Vector3 Alignment(Vector3 flockDirection)
    {
        desired = flockDirection - this.transform.forward;
        desired = desired.normalized * maxSpeed;
        desired -= velocity;
        return desired;
    }*/
    /* // Cohesion method
    protected Vector3 Cohesion(Vector3 centroidLocation)
    {
        desired = centroidLocation - this.transform.position;
        desired = desired.normalized * maxSpeed;
        desired -= velocity;
        return desired;
    }*/
    /*// Separation method    
    protected Vector3 Separation()
    {
        Vector3 sumVel = Vector3.zero;
        float tempDist;
        foreach (GameObject g in Flock)
        {
            if (g != this.gameObject)
            {
                tempDist = Vector3.Distance(this.gameObject.transform.position, g.transform.position);
                if (tempDist < tooCloseDist)
                {
                    sumVel += ((g.transform.position - transform.position) * -1) * maxSpeed * (1 / tempDist);
                }
            }
        }
        sumVel = sumVel.normalized * maxSpeed;
        sumVel = sumVel - velocity;
        return sumVel;
    }*/
    #endregion
}