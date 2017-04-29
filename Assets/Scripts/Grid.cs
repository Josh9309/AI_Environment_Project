using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {

    protected GameObject[,] grid;
    public GameObject unit;
    public Material[] matColors; 
    // 0 = red
    // 1 = gray
    // 2 = green

    public GameObject[,] Grid_
    {
        get { return grid; }
    }
	
	void Start () {
        grid = new GameObject[100, 100];
        GameObject temp;
        for(int i=0; i<100; ++i)
        {
            for(int j=0; j<100; ++j)
            {
                temp = (GameObject)Object.Instantiate(unit, new Vector3((i * 5) - 250, 0, (j * 5) - 250), Quaternion.identity);//create object at correct transform
                temp.AddComponent<GridEntry>(); //give it a GridEntry class
                grid[i, j] = temp;//store gameobj
                temp.GetComponent<MeshRenderer>().material = matColors[1];//reset to gray
            }
        }
	}

    /// <summary>
    /// Updates influece values
    /// </summary>
    /// <param name="transf">Transform of unit object</param>
    /// <param name="red">True if red team, false if green</param>
    /// <param name="influence">Influece value of the unit</param>
    public void UpdateInfluence(Vector3 transf, bool red, int influence)
    {
        Vector2 xz = new Vector2((transf.x + 250) / 5, (transf.z + 250) / 5);
        if (red)
            grid[(int)xz.x, (int)xz.y].GetComponent<GridEntry>().RedInfluence += influence;
        else
            grid[(int)xz.x, (int)xz.y].GetComponent<GridEntry>().GreenInfluence += influence;
    }
}