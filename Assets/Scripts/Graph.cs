using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph {

    #region Fields
    private NavNode[] nodes;
    private PriorityQueue pq = new PriorityQueue();
    #endregion

    #region Properties
    public NavNode[] Nodes
    {
        get { return nodes; }
    }
    public PriorityQueue PQ
    {
        get { return pq; }
    }
    #endregion

    #region Methods
    public void SetUpGraph()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("AStarNode");
        nodes = new NavNode[objs.Length];
        for (int i = 0; i < nodes.Length; ++i)
            nodes[i] = objs[i].GetComponent<NavNode>();
    }

    public List<NavNode> AStar(NavNode start, NavNode end)
    {
        // Reset data (if any exists)
        ResetGraph();
        // Closed node list
        List<NavNode> closed = new List<NavNode>();
        // Actual path list
        List<NavNode> path = new List<NavNode>();
        // Current node
        NavNode current = start;
        // Add the current node to start
        pq.Enqueue(current);
        // Begin the big loop... Go until you hit the end
        while (pq.Peek() != end)
        {
            // If a path could not be found...
            if (pq.Peek() == null)
                return null; // Give up!
            // Store and remove top node
            current = pq.Dequeue();
            // Add it to the closed list
            closed.Add(current);
            // Heuristic variable
            float diagonal = 0;
            // Queue up neightbors
            foreach (NavNode n in current.NeighborNavNodes) 
            {
                // Here, "current.DistToNode(n)" is the distance (squared) to neighbor 'n'
                float cost = current.DistFromStart + current.DistToNode(n);
                // Remove far away vertices
                if (cost < n.DistFromStart)
                {
                    if (pq.Contains(n))
                        pq.Remove(n);
                    if (closed.Contains(n))
                        closed.Remove(n);
                }
                // Adding to the priority queue
                if (!closed.Contains(n) &&
                    !pq.Contains(n) &&
                    n.Walkable)
                {
                    n.DistFromStart = cost;
                    diagonal = Vector3.SqrMagnitude(end.transform.position - n.transform.position);
                    n.Priority = n.DistFromStart + diagonal;
                    n.Parent = current;
                    pq.Enqueue(n);
                }
            }
        }
        // Return the actual path
        current = end;
        while (current != start) 
        {
            current = current.Parent;
            path.Add(current);
        }
        return path;
    }
    /// <summary>
    /// Resets the data in the priority queue. 
    /// Should be called when you want a new path.
    /// But the AStar method does that anyway so don't worry about it.
    /// </summary>
    public void ResetGraph()
    {
        foreach (NavNode n in nodes)
            n.Walkable = true;
        // Empty priority queue
        while (pq.Dequeue() != null) ;
    }

    /// <summary>
    /// Returns the NavNode nearest the location passed in.
    /// Uses a brute force method so it's not overly pretty, but it gets the job done.
    /// </summary>
    public NavNode FindNearestNode(Vector3 loc)
    {
        float minDistSqr = float.MaxValue;
        float tempDistSqr = 0f;
        int index = -1;
        for (int i = 0; i < nodes.Length; ++i)
        {
            tempDistSqr = Vector3.SqrMagnitude(loc - nodes[i].gameObject.transform.position);
            if (tempDistSqr < minDistSqr)
            {
                index = i;
                minDistSqr = tempDistSqr;
            }
        }
        return nodes[index];
    }
    #endregion
}
