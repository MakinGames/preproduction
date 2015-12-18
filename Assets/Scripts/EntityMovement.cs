using UnityEngine;
using Pathfinding;
using System.Collections;

public class EntityMovement : MonoBehaviour
{
    public Vector3 TargetPosition;
    public Transform TargetObject;
    private Seeker _seeker;

	void Start ()
	{
	    _seeker = GetComponent<Seeker>();
	    _seeker.pathCallback += OnPathComplete;

	    _seeker.StartPath(transform.position, TargetObject.position);
	}

    void OnDisable()
    {
        _seeker.pathCallback -= OnPathComplete;
    }

    // ReSharper disable once MemberCanBePrivate.Global
    public void OnPathComplete(Path p)
    {
        Debug.Log("Path Returned. Error: " + p.error);
    }
}
