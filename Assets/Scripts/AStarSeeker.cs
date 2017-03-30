using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarSeeker : VehicleMovement {

    private Vector3 seekTarget = Vector3.zero;
    private List<NavNode> path;
    private int currentIndex = 0;

    void Start()
    {
        seekTarget = this.transform.position;
    }

    protected override void CalculateSteeringForces()
    {
        // If you're close enough to the node you're seeking
        float distSqr = Vector3.SqrMagnitude(this.transform.position - seekTarget);
        if (distSqr < 5.0f)
        {
            // Seek the next node
            ++currentIndex;
            if (currentIndex < path.Count) // Stay within bounds
                seekTarget = path[currentIndex].gameObject.transform.position;
        }

        force = Vector3.zero;
        force += Seek(seekTarget);
        force = Vector3.ClampMagnitude(force, maxForce);
        //force.y = 0;
        ApplyForce(force);
    }

    public void AcquirePath(List<NavNode> _path)
    {
        path = _path;
        currentIndex = 0;
        seekTarget = path[0].transform.position;
    }
}
