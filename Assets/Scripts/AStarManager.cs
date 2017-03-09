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
	}

    void Update()
    {
        // Move the character
        // NEED STEERING FORCES!
    }
}
