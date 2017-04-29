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
    private RaycastHit gridHit;

    public void Start () {
        grid = new GameObject[100, 100];
        GameObject temp;
        for(int i=0; i<100; ++i)
        {
            for(int j=0; j<100; ++j)
            {
                Physics.Raycast(new Ray(new Vector3((i * 5) - 250, 400, (j * 5) - 250), Vector3.down),out gridHit, 1000);
                temp = (GameObject)Object.Instantiate(unit, gridHit.point, Quaternion.identity);//create object at correct transform
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
    /// <param name="influence">Influence value of the unit</param>
    public void UpdateInfluence(Vector3 transf, bool red, int influence)
    {
        Vector2 xz = new Vector2((transf.x + 250) / 5, (transf.z + 250) / 5);
        if (red)
        {
            int xPos = (int)xz.x;
            int yPos = (int)xz.y;
            grid[xPos,yPos].GetComponent<GridEntry>().RedInfluence += influence; //set this equal to the intial influence
            int i = 1;
            while(influence-i > 0) {
                //Fill out grid for influence -i radiating outwards
                //grid space up
                if (yPos - i >= 0) //makesure the point is not off grid
                {
                    grid[xPos, yPos-i].GetComponent<GridEntry>().RedInfluence += influence-i;
                }
                //GridSpace down
                if(yPos+i < 100)
                {
                    grid[xPos, yPos+i].GetComponent<GridEntry>().RedInfluence += influence-i;
                }
                //grid space left
                if(xPos - i >= 0)
                {
                    grid[xPos-i, yPos].GetComponent<GridEntry>().RedInfluence += influence - i;
                }
                //grid space right
                if(xPos + i < 100)
                {
                    grid[xPos+i, yPos].GetComponent<GridEntry>().RedInfluence += influence - i;
                }
                if ((influence - i)-1 <= 0)
                {
                    //gridSpace top right
                    switch (i)
                    {
                        case 3:
                            //3 influence
                            grid[xPos + 1, yPos -1].GetComponent<GridEntry>().RedInfluence += influence - 1;
                            //2 influence
                            grid[xPos + 1, yPos - 2].GetComponent<GridEntry>().RedInfluence += influence - 2;
                            grid[xPos + 2, yPos -2].GetComponent<GridEntry>().RedInfluence += influence - 2;
                            grid[xPos + 2, yPos -1].GetComponent<GridEntry>().RedInfluence += influence - 2;
                            //1 influence
                            grid[xPos + 1, yPos -3].GetComponent<GridEntry>().RedInfluence += influence - 3;
                            grid[xPos + 2, yPos -3].GetComponent<GridEntry>().RedInfluence += influence - 3;
                            grid[xPos + 3, yPos-3].GetComponent<GridEntry>().RedInfluence += influence - 3;
                            grid[xPos + 3, yPos-1].GetComponent<GridEntry>().RedInfluence += influence - 3;
                            grid[xPos + 3, yPos- 2].GetComponent<GridEntry>().RedInfluence += influence - 3;
                            break;

                        case 2:
                            //2 influence
                            grid[xPos + 1, yPos - 1].GetComponent<GridEntry>().RedInfluence += influence - 1;
                            //1 influence
                            grid[xPos + 1, yPos - 2].GetComponent<GridEntry>().RedInfluence += influence - 2;
                            grid[xPos + 2, yPos - 2].GetComponent<GridEntry>().RedInfluence += influence - 2;
                            grid[xPos + 2, yPos - 1].GetComponent<GridEntry>().RedInfluence += influence - 2;
                            break;

                        case 1:
                            //2 influence
                            grid[xPos + 1, yPos - 1].GetComponent<GridEntry>().RedInfluence += influence - 1;
                            break;

                        case 0:
                            //
                            break;
                    }
                    //gridspace bottom right
                    switch (i)
                    {
                        case 3:
                            //3 influence
                            grid[xPos + 1, yPos + 1].GetComponent<GridEntry>().RedInfluence += influence - 1;
                            //2 influence
                            grid[xPos + 1, yPos + 2].GetComponent<GridEntry>().RedInfluence += influence - 2;
                            grid[xPos + 2, yPos + 2].GetComponent<GridEntry>().RedInfluence += influence - 2;
                            grid[xPos + 2, yPos + 1].GetComponent<GridEntry>().RedInfluence += influence - 2;
                            //1 influence
                            grid[xPos + 1, yPos + 3].GetComponent<GridEntry>().RedInfluence += influence - 3;
                            grid[xPos + 2, yPos + 3].GetComponent<GridEntry>().RedInfluence += influence - 3;
                            grid[xPos + 3, yPos + 3].GetComponent<GridEntry>().RedInfluence += influence - 3;
                            grid[xPos + 3, yPos + 1].GetComponent<GridEntry>().RedInfluence += influence - 3;
                            grid[xPos + 3, yPos + 2].GetComponent<GridEntry>().RedInfluence += influence - 3;
                            break;

                        case 2:
                            //2 influence
                            grid[xPos + 1, yPos + 1].GetComponent<GridEntry>().RedInfluence += influence - 1;
                            //1 influence
                            grid[xPos + 1, yPos + 2].GetComponent<GridEntry>().RedInfluence += influence - 2;
                            grid[xPos + 2, yPos + 2].GetComponent<GridEntry>().RedInfluence += influence - 2;
                            grid[xPos + 2, yPos + 1].GetComponent<GridEntry>().RedInfluence += influence - 2;
                            break;

                        case 1:
                            //2 influence
                            grid[xPos + 1, yPos + 1].GetComponent<GridEntry>().RedInfluence += influence - 1;
                            break;

                        case 0:
                            //
                            break;
                    }
                    //Gridspace Top Left
                    switch (i)
                    {
                        case 3:
                            //3 influence
                            grid[xPos - 1, yPos - 1].GetComponent<GridEntry>().RedInfluence += influence - 1;
                            //2 influence
                            grid[xPos - 1, yPos - 2].GetComponent<GridEntry>().RedInfluence += influence - 2;
                            grid[xPos - 2, yPos - 2].GetComponent<GridEntry>().RedInfluence += influence - 2;
                            grid[xPos - 2, yPos - 1].GetComponent<GridEntry>().RedInfluence += influence - 2;
                            //1 influence
                            grid[xPos - 1, yPos - 3].GetComponent<GridEntry>().RedInfluence += influence - 3;
                            grid[xPos - 2, yPos - 3].GetComponent<GridEntry>().RedInfluence += influence - 3;
                            grid[xPos - 3, yPos - 3].GetComponent<GridEntry>().RedInfluence += influence - 3;
                            grid[xPos - 3, yPos - 1].GetComponent<GridEntry>().RedInfluence += influence - 3;
                            grid[xPos - 3, yPos - 2].GetComponent<GridEntry>().RedInfluence += influence - 3;
                            break;

                        case 2:
                            //2 influence
                            grid[xPos - 1, yPos - 1].GetComponent<GridEntry>().RedInfluence += influence - 1;
                            //1 influence
                            grid[xPos - 1, yPos - 2].GetComponent<GridEntry>().RedInfluence += influence - 2;
                            grid[xPos - 2, yPos - 2].GetComponent<GridEntry>().RedInfluence += influence - 2;
                            grid[xPos - 2, yPos - 1].GetComponent<GridEntry>().RedInfluence += influence - 2;
                            break;

                        case 1:
                            //2 influence
                            grid[xPos - 1, yPos - 1].GetComponent<GridEntry>().RedInfluence += influence - 1;
                            break;

                        case 0:
                            //
                            break;
                    }
                    //Top Right
                    switch (i)
                    {
                        case 3:
                            //3 influence
                            grid[xPos - 1, yPos + 1].GetComponent<GridEntry>().RedInfluence += influence - 1;
                            //2 influence
                            grid[xPos - 1, yPos + 2].GetComponent<GridEntry>().RedInfluence += influence - 2;
                            grid[xPos - 2, yPos + 2].GetComponent<GridEntry>().RedInfluence += influence - 2;
                            grid[xPos - 2, yPos + 1].GetComponent<GridEntry>().RedInfluence += influence - 2;
                            //1 influence
                            grid[xPos - 1, yPos + 3].GetComponent<GridEntry>().RedInfluence += influence - 3;
                            grid[xPos - 2, yPos + 3].GetComponent<GridEntry>().RedInfluence += influence - 3;
                            grid[xPos - 3, yPos + 3].GetComponent<GridEntry>().RedInfluence += influence - 3;
                            grid[xPos - 3, yPos + 1].GetComponent<GridEntry>().RedInfluence += influence - 3;
                            grid[xPos - 3, yPos + 2].GetComponent<GridEntry>().RedInfluence += influence - 3;
                            break;

                        case 2:
                            //2 influence
                            grid[xPos - 1, yPos + 1].GetComponent<GridEntry>().RedInfluence += influence - 1;
                            //1 influence
                            grid[xPos - 1, yPos + 2].GetComponent<GridEntry>().RedInfluence += influence - 2;
                            grid[xPos - 2, yPos + 2].GetComponent<GridEntry>().RedInfluence += influence - 2;
                            grid[xPos - 2, yPos + 1].GetComponent<GridEntry>().RedInfluence += influence - 2;
                            break;

                        case 1:
                            //2 influence
                            grid[xPos - 1, yPos + 1].GetComponent<GridEntry>().RedInfluence += influence - 1;
                            break;

                        case 0:
                            //
                            break;
                    }
                }
                i++;
            }

        }
        else
        {
            int xPos = (int)xz.x;
            int yPos = (int)xz.y;
            grid[xPos, yPos].GetComponent<GridEntry>().GreenInfluence += influence;
            int i = 1;
            while (influence - i > 0)
            {
                //Fill out grid for influence -i radiating outwards
                //grid space up
                if (yPos - i >= 0) //makesure the point is not off grid
                {
                    grid[xPos, yPos - i].GetComponent<GridEntry>().GreenInfluence += influence - i;
                }
                //GridSpace down
                if (yPos + i < 100)
                {
                    grid[xPos, yPos + i].GetComponent<GridEntry>().GreenInfluence += influence - i;
                }
                //grid space left
                if (xPos - i >= 0)
                {
                    grid[xPos - i, yPos].GetComponent<GridEntry>().GreenInfluence += influence - i;
                }
                //grid space right
                if (xPos + i < 100)
                {
                    grid[xPos + i, yPos].GetComponent<GridEntry>().GreenInfluence += influence - i;
                }
                if ((influence - i) - 1 <= 0)
                {
                    //gridSpace top right
                    switch (i)
                    {
                        case 3:
                            //3 influence
                            grid[xPos + 1, yPos - 1].GetComponent<GridEntry>().GreenInfluence += influence - 1;
                            //2 influence
                            grid[xPos + 1, yPos - 2].GetComponent<GridEntry>().GreenInfluence += influence - 2;
                            grid[xPos + 2, yPos - 2].GetComponent<GridEntry>().GreenInfluence += influence - 2;
                            grid[xPos + 2, yPos - 1].GetComponent<GridEntry>().GreenInfluence += influence - 2;
                            //1 influence
                            grid[xPos + 1, yPos - 3].GetComponent<GridEntry>().GreenInfluence += influence - 3;
                            grid[xPos + 2, yPos - 3].GetComponent<GridEntry>().GreenInfluence += influence - 3;
                            grid[xPos + 3, yPos - 3].GetComponent<GridEntry>().GreenInfluence += influence - 3;
                            grid[xPos + 3, yPos - 1].GetComponent<GridEntry>().GreenInfluence += influence - 3;
                            grid[xPos + 3, yPos - 2].GetComponent<GridEntry>().GreenInfluence += influence - 3;
                            break;

                        case 2:
                            //2 influence
                            grid[xPos + 1, yPos - 1].GetComponent<GridEntry>().GreenInfluence += influence - 1;
                            //1 influence
                            grid[xPos + 1, yPos - 2].GetComponent<GridEntry>().GreenInfluence += influence - 2;
                            grid[xPos + 2, yPos - 2].GetComponent<GridEntry>().GreenInfluence += influence - 2;
                            grid[xPos + 2, yPos - 1].GetComponent<GridEntry>().GreenInfluence += influence - 2;
                            break;

                        case 1:
                            //2 influence
                            grid[xPos + 1, yPos - 1].GetComponent<GridEntry>().GreenInfluence += influence - 1;
                            break;

                        case 0:
                            //
                            break;
                    }
                    //gridspace bottom right
                    switch (i)
                    {
                        case 3:
                            //3 influence
                            grid[xPos + 1, yPos + 1].GetComponent<GridEntry>().GreenInfluence += influence - 1;
                            //2 influence
                            grid[xPos + 1, yPos + 2].GetComponent<GridEntry>().GreenInfluence += influence - 2;
                            grid[xPos + 2, yPos + 2].GetComponent<GridEntry>().GreenInfluence += influence - 2;
                            grid[xPos + 2, yPos + 1].GetComponent<GridEntry>().GreenInfluence += influence - 2;
                            //1 influence
                            grid[xPos + 1, yPos + 3].GetComponent<GridEntry>().GreenInfluence += influence - 3;
                            grid[xPos + 2, yPos + 3].GetComponent<GridEntry>().GreenInfluence += influence - 3;
                            grid[xPos + 3, yPos + 3].GetComponent<GridEntry>().GreenInfluence += influence - 3;
                            grid[xPos + 3, yPos + 1].GetComponent<GridEntry>().GreenInfluence += influence - 3;
                            grid[xPos + 3, yPos + 2].GetComponent<GridEntry>().GreenInfluence += influence - 3;
                            break;

                        case 2:
                            //2 influence
                            grid[xPos + 1, yPos + 1].GetComponent<GridEntry>().GreenInfluence += influence - 1;
                            //1 influence
                            grid[xPos + 1, yPos + 2].GetComponent<GridEntry>().GreenInfluence += influence - 2;
                            grid[xPos + 2, yPos + 2].GetComponent<GridEntry>().GreenInfluence += influence - 2;
                            grid[xPos + 2, yPos + 1].GetComponent<GridEntry>().GreenInfluence += influence - 2;
                            break;

                        case 1:
                            //2 influence
                            grid[xPos + 1, yPos + 1].GetComponent<GridEntry>().GreenInfluence += influence - 1;
                            break;

                        case 0:
                            //
                            break;
                    }
                    //Gridspace Top Left
                    switch (i)
                    {
                        case 3:
                            //3 influence
                            grid[xPos - 1, yPos - 1].GetComponent<GridEntry>().GreenInfluence += influence - 1;
                            //2 influence
                            grid[xPos - 1, yPos - 2].GetComponent<GridEntry>().GreenInfluence += influence - 2;
                            grid[xPos - 2, yPos - 2].GetComponent<GridEntry>().GreenInfluence += influence - 2;
                            grid[xPos - 2, yPos - 1].GetComponent<GridEntry>().GreenInfluence += influence - 2;
                            //1 influence
                            grid[xPos - 1, yPos - 3].GetComponent<GridEntry>().GreenInfluence += influence - 3;
                            grid[xPos - 2, yPos - 3].GetComponent<GridEntry>().GreenInfluence += influence - 3;
                            grid[xPos - 3, yPos - 3].GetComponent<GridEntry>().GreenInfluence += influence - 3;
                            grid[xPos - 3, yPos - 1].GetComponent<GridEntry>().GreenInfluence += influence - 3;
                            grid[xPos - 3, yPos - 2].GetComponent<GridEntry>().GreenInfluence += influence - 3;
                            break;

                        case 2:
                            //2 influence
                            grid[xPos - 1, yPos - 1].GetComponent<GridEntry>().GreenInfluence += influence - 1;
                            //1 influence
                            grid[xPos - 1, yPos - 2].GetComponent<GridEntry>().GreenInfluence += influence - 2;
                            grid[xPos - 2, yPos - 2].GetComponent<GridEntry>().GreenInfluence += influence - 2;
                            grid[xPos - 2, yPos - 1].GetComponent<GridEntry>().GreenInfluence += influence - 2;
                            break;

                        case 1:
                            //2 influence
                            grid[xPos - 1, yPos - 1].GetComponent<GridEntry>().GreenInfluence += influence - 1;
                            break;

                        case 0:
                            //
                            break;
                    }
                    //Top Right
                    switch (i)
                    {
                        case 3:
                            //3 influence
                            grid[xPos - 1, yPos + 1].GetComponent<GridEntry>().GreenInfluence += influence - 1;
                            //2 influence
                            grid[xPos - 1, yPos + 2].GetComponent<GridEntry>().GreenInfluence += influence - 2;
                            grid[xPos - 2, yPos + 2].GetComponent<GridEntry>().GreenInfluence += influence - 2;
                            grid[xPos - 2, yPos + 1].GetComponent<GridEntry>().GreenInfluence += influence - 2;
                            //1 influence
                            grid[xPos - 1, yPos + 3].GetComponent<GridEntry>().GreenInfluence += influence - 3;
                            grid[xPos - 2, yPos + 3].GetComponent<GridEntry>().GreenInfluence += influence - 3;
                            grid[xPos - 3, yPos + 3].GetComponent<GridEntry>().GreenInfluence += influence - 3;
                            grid[xPos - 3, yPos + 1].GetComponent<GridEntry>().GreenInfluence += influence - 3;
                            grid[xPos - 3, yPos + 2].GetComponent<GridEntry>().GreenInfluence += influence - 3;
                            break;

                        case 2:
                            //2 influence
                            grid[xPos - 1, yPos + 1].GetComponent<GridEntry>().GreenInfluence += influence - 1;
                            //1 influence
                            grid[xPos - 1, yPos + 2].GetComponent<GridEntry>().GreenInfluence += influence - 2;
                            grid[xPos - 2, yPos + 2].GetComponent<GridEntry>().GreenInfluence += influence - 2;
                            grid[xPos - 2, yPos + 1].GetComponent<GridEntry>().GreenInfluence += influence - 2;
                            break;

                        case 1:
                            //2 influence
                            grid[xPos - 1, yPos + 1].GetComponent<GridEntry>().GreenInfluence += influence - 1;
                            break;

                        case 0:
                            //
                            break;
                    }
                }
                i++;
            }
        }
    }

    public void UpdateInfluenceMap()
    {
        GridEntry currentGridPoint;
        for(int i = 0; i < 100; i++)
        {
            for(int j=0; j< 100; j++)
            {
                currentGridPoint = grid[i, j].GetComponent<GridEntry>();

                if(currentGridPoint.RedInfluence > currentGridPoint.GreenInfluence) //if red has won this grid point
                {
                    currentGridPoint.MeshRend.material = matColors[0];
                }
                else if(currentGridPoint.GreenInfluence > currentGridPoint.RedInfluence) //if green has won this grid point
                {
                    currentGridPoint.MeshRend.material = matColors[2];
                }
                else
                {
                    currentGridPoint.MeshRend.material = matColors[1];
                }
            }
        }
    }

    public void Reset()
    {
        GridEntry currentGridPoint;
        for (int i = 0; i < 100; i++)
        {
            for (int j = 0; j < 100; j++)
            {
                currentGridPoint = grid[i, j].GetComponent<GridEntry>();
                currentGridPoint.RedInfluence = 0;
                currentGridPoint.GreenInfluence = 0;
            }
        }
    }
}