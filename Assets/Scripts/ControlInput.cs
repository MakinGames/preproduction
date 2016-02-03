using UnityEngine;
using System.Collections;
using System.Net.NetworkInformation;

public class ControlInput : MonoBehaviour
{
    public GameObject unit;
    private EntityMovement unitScript;

    void Start()
    {
        unitScript = unit.GetComponent<EntityMovement>();
    }
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetMouseButtonDown(0))
	    {
	        Vector3 mousePosition = GetMouseWorldPosition();
	        if (mousePosition != new Vector3(0, 0, 0))
	        {
                unitScript.SeekToPath(mousePosition);
	        }
	    }
	}

    Vector3 GetMouseWorldPosition()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            return hit.point;
        }
        return new Vector3(0, 0, 0);
    }
}
