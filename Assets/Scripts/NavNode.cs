using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NavNode : MonoBehaviour
{
    [Tooltip("List of connected nodes. Must be set via inspector with drag and drop.")]
    public List<GameObject> neighbors;

    #region Protected fields
    protected NavNode parent;
    protected float dist;
    protected float priority;
    protected bool walkable;
    #endregion

    #region Properties
    public NavNode Parent
    {
        get { return parent; }
        set { parent = value; }
    }
    public float DistFromStart
    {
        get { return dist; }
        set { dist = value; }
    }
    /// <summary>
    /// Gets NavNode components of all neighbors
    /// </summary>
    public List<NavNode> NeighborNavNodes
    {
        get
        {
            List<NavNode> data = new List<NavNode>();
            foreach (GameObject g in neighbors)
                data.Add(g.GetComponent<NavNode>());
            return data;
        }
    }
    public bool Walkable
    { get { return walkable; } set { walkable = value; } }
    public float Priority
    { get { return priority; } set { priority = value; } }
    #endregion

    #region Methods
    void Start()
    {
        dist = 0;
        priority = 0;
        walkable = true;
    }

    /// <summary>
    /// Returns distance (squared!) from this node's position to n's position
    /// </summary>
    public float DistToNode(NavNode n)
    {
        return Vector3.SqrMagnitude(this.transform.position - n.transform.position);
    }

    /// <summary>
    /// Super duper helpful for setting up AI paths. Comment out if it bothers you.
    /// </summary>
    void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        foreach (GameObject n in neighbors)
            Debug.DrawLine(this.transform.position, n.transform.position);
    }
    #endregion
}
