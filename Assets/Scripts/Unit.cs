using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {

    public bool Red;
    public int Influence;
    
	void Start () {
        Grid gm = GameObject.Find("GridManager").GetComponent<Grid>();
        gm.UpdateInfluence(this.transform.position, Red, Influence);
	}
}