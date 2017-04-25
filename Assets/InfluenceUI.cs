using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfluenceUI : MonoBehaviour {
    public enum Team { RED, Green};
    public enum Unit { BLACK, YELLOW, BLUE, WHITE};

    [SerializeField] private Dropdown teamSelector;
    [SerializeField] private Dropdown unitSelector;

    private Team gameTeam = Team.RED;
    private Unit gameUnit = Unit.BLACK;

    public Team GameTeam
    {
        get { return gameTeam; }
    }

    public Unit GameUnit
    {
        get { return gameUnit; }
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UpdateTeam()
    {
        switch (teamSelector.value)
        {
            case 0:
                gameTeam = Team.RED;
                break;

            case 1:
                gameTeam = Team.Green;
                break;
        }
    }

    public void UpdateUnit()
    {
        switch (unitSelector.value)
        {
            case 0:
                gameUnit = Unit.BLACK;
                break;

            case 1:
                gameUnit = Unit.YELLOW;
                break;

            case 2:
                gameUnit = Unit.BLUE;
                break;

            case 3:
                gameUnit = Unit.WHITE;
                break;

        }
    }
}
