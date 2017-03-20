using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnviormentUI : MonoBehaviour {

    [SerializeField] private Toggle Cohesion;
    [SerializeField] private Toggle alignment;
    [SerializeField] private Toggle seperation;
    [SerializeField] private Toggle avoidance;
    [SerializeField] private Toggle queue;
    [SerializeField] private Toggle seek;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.C)) { Cohesion.isOn = !Cohesion.isOn; }
        if (Input.GetKeyDown(KeyCode.A)) { alignment.isOn = !alignment.isOn; }
        if (Input.GetKeyDown(KeyCode.S)) { seperation.isOn = !seperation.isOn; }
        if (Input.GetKeyDown(KeyCode.O)) { avoidance.isOn = !avoidance.isOn; }
        if(Input.GetKeyDown(KeyCode.Q)) { queue.isOn = !queue.isOn; }
        if (Input.GetKeyDown(KeyCode.K)) { seek.isOn = !seek.isOn; }
	}
}
