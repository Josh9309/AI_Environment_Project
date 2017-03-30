using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarManager : MonoBehaviour {

    // This Graph is pretty much going to be doing all the work
    private Graph AStarGraph;
    // The character who will be using the AStar pathfinding
    public GameObject AStarCharacter;
    // The path for the character to take
    private List<NavNode> AStarPath;
    // Path for the flockers to take
    public GameObject[] flockingPath;
    // Reference to target object
    public GameObject AStarTargetObj;

    #region Properties
    public List<GameObject> FlockingPathList
    {
        get
        {
            List<GameObject> list = new List<GameObject>();
            for (int i = 0; i < flockingPath.Length; ++i)
                list.Add(flockingPath[i]);
            return list;
        }
    }
    #endregion

    void Start () {
        // Prepare AStar stuff
        AStarGraph = new Graph();
        AStarGraph.SetUpGraph();
        // Run the algorithm
        DoAStarStuffForCharacter();
    }

    void Update()
    {
        // Check to see if the character is close enough to the target object
        float sqrDist = Vector3.SqrMagnitude(AStarCharacter.transform.position - AStarTargetObj.transform.position);
        if (sqrDist < 52.0f) // 52 is kinda arbitrary but I felt it was a good radius
        {
            // Move the object to a random point
            AStarTargetObj.transform.position = AStarGraph.Nodes[Random.Range(0, AStarGraph.Nodes.Length - 1)].gameObject.transform.position;
            // Move up a bit to look nicer
            AStarTargetObj.transform.position += new Vector3(0f, 3f, 0f);
            // Run the algorithm again
            DoAStarStuffForCharacter();
        }
    }

    void DoAStarStuffForCharacter()
    {
        // Set up nodes
        NavNode characterNode = AStarGraph.FindNearestNode(AStarCharacter.transform.position);
        NavNode objNode = AStarGraph.FindNearestNode(AStarTargetObj.transform.position);
        AStarPath = AStarGraph.AStar(characterNode, objNode); // Run the algorithm
        AStarPath.RemoveAt(AStarPath.Count - 1); // Remove last node because it's the one we're already at
        AStarPath.Reverse(); // Reverse the path because it comes out backwards
        AStarPath.Add(objNode); // Add the final node to the path (because the function itself doesn't)
        // Give path to the character
        AStarCharacter.GetComponent<AStarSeeker>().AcquirePath(AStarPath);
    }
}
