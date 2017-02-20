using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : VehicleMovement {

    void Start () {
        base.Start();
        force = Vector3.zero;
    }

    protected override void CalculateSteeringForces()
    {
        force = Vector3.zero;

        // Up/down movement
        if (Input.GetKey(KeyCode.W))
        {
            //Seek point ahead on the Z axis
            force += Seek(this.transform.position + new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z) * 10);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            //Seek point behind on the Z axis
            force += Seek(this.transform.position + new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z) * -10);
        }
        // Left/right movement
        if (Input.GetKey(KeyCode.A))
        {
            //Seek point left on the X axis
            force += Seek(this.transform.position + new Vector3(Camera.main.transform.right.x, 0, Camera.main.transform.right.z) * -10);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            //Seek point right on the X axis
            force += Seek(this.transform.position + new Vector3(Camera.main.transform.right.x, 0, Camera.main.transform.right.z) * 10);
        }

        // Friction
        this.velocity *= 0.92f;

        // Clamp force
        force = Vector3.ClampMagnitude(force, maxForce);

        // Clamp position to terrain
        //SnapToTerrain();  //Don't do this anymore! We have bridges now

        // Apply force to acceleration
        ApplyForce(force);
    }
}
