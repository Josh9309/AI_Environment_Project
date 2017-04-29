using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridEntry : MonoBehaviour {

    protected int redInfluence = 0, greenInfluence = 0;
    protected MeshRenderer meshrend;

    public int RedInfluence
    {
        get { return redInfluence; }
        set { redInfluence = value; Debug.Log(redInfluence); }
    }
    public int GreenInfluence
    {
        get { return greenInfluence; }
        set { greenInfluence = value; Debug.Log(greenInfluence); }
    }
    public MeshRenderer MeshRend
    {
        get { return meshrend; }
        set { meshrend = value; }
    }

    void Start()
    {
        meshrend = this.GetComponent<MeshRenderer>();
    }
}
