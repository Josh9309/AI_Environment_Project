using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitPlacement : MonoBehaviour {
    public CameraChange camChange;
    public InfluenceUI iUI;
    [SerializeField] GameObject R_BlueUnit;
    [SerializeField] GameObject R_BlackUnit;
    [SerializeField] GameObject R_WhiteUnit;
    [SerializeField] GameObject R_YellowUnit;
    [SerializeField] GameObject G_BlackUnit;
    [SerializeField] GameObject G_BlueUnit;
    [SerializeField] GameObject G_WhiteUnit;
    [SerializeField] GameObject G_YellowUnit;
    RaycastHit mouseRayHit;
    private float heightOffset = 2.5f;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                Debug.Log("UI clicked");
                return;
            }
            Ray mouseRay =camChange.cameras[camChange.MainCamNum].GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            bool unitPlaced = false;
            int rayCount = 0;
            if(Physics.Raycast(mouseRay, out mouseRayHit, 1000))
            {
                while (!unitPlaced && rayCount < 2)
                {
                    if (mouseRayHit.collider.tag == "terrain")
                    {
                        switch (iUI.GameTeam)
                        {
                            case InfluenceUI.Team.RED:
                                switch (iUI.GameUnit)
                                {
                                    case InfluenceUI.Unit.BLACK:
                                        Instantiate(R_BlackUnit,new Vector3( mouseRayHit.point.x, mouseRayHit.point.y + heightOffset, mouseRayHit.point.z), Quaternion.identity);
                                        break;

                                    case InfluenceUI.Unit.BLUE:
                                        Instantiate(R_BlueUnit, new Vector3(mouseRayHit.point.x, mouseRayHit.point.y + heightOffset, mouseRayHit.point.z), Quaternion.identity);
                                        break;

                                    case InfluenceUI.Unit.YELLOW:
                                        Instantiate(R_YellowUnit, new Vector3(mouseRayHit.point.x, mouseRayHit.point.y + heightOffset, mouseRayHit.point.z), Quaternion.identity);
                                        break;

                                    case InfluenceUI.Unit.WHITE:
                                        Instantiate(R_WhiteUnit, new Vector3(mouseRayHit.point.x, mouseRayHit.point.y + heightOffset, mouseRayHit.point.z), Quaternion.identity);
                                        break;
                                }
                                break;

                            case InfluenceUI.Team.Green:
                                switch (iUI.GameUnit)
                                {
                                    case InfluenceUI.Unit.BLACK:
                                        Instantiate(G_BlackUnit, new Vector3(mouseRayHit.point.x, mouseRayHit.point.y + heightOffset, mouseRayHit.point.z), Quaternion.identity);
                                        break;

                                    case InfluenceUI.Unit.BLUE:
                                        Instantiate(G_BlueUnit, new Vector3(mouseRayHit.point.x, mouseRayHit.point.y + heightOffset, mouseRayHit.point.z), Quaternion.identity);
                                        break;

                                    case InfluenceUI.Unit.YELLOW:
                                        Instantiate(G_YellowUnit, new Vector3(mouseRayHit.point.x, mouseRayHit.point.y + heightOffset, mouseRayHit.point.z), Quaternion.identity);
                                        break;

                                    case InfluenceUI.Unit.WHITE:
                                        Instantiate(G_WhiteUnit, new Vector3(mouseRayHit.point.x, mouseRayHit.point.y + heightOffset, mouseRayHit.point.z), Quaternion.identity);
                                        break;
                                }
                                break;
                        }
                        unitPlaced = true;
                    }
                    else
                    {
                        Physics.Raycast(new Ray(mouseRayHit.point, mouseRay.direction), out mouseRayHit, 1000);
                        rayCount++;
                    }
                }
            }
        }
	}
}
