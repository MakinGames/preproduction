using UnityEngine;
using Pathfinding;
using System.Collections;
using UnityEditor;

public class EntityMovement : MonoBehaviour
{
    public Transform TargetObject;
    public float EntitySpeed;
    public float NextWaypointDistance;
    private Path _path;
    private int _currentWaypoint;
    private Seeker _seeker;
    private CharacterController _characterController;

	void Start ()
	{
	    _seeker = GetComponent<Seeker>();
	    _characterController = GetComponent<CharacterController>();

	    _seeker.StartPath(transform.position, TargetObject.position, OnPathComplete);
	}

    void Update()
    {
        if (_path == null)
        {
            return;
        }

        if (_currentWaypoint >= _path.vectorPath.Count)
        {
            Debug.Log("End of Path Reached");
            _path = null;
            return;
        }

        //Check if close enough to waypoint to move to next
        if (Vector3.Distance(transform.position, _path.vectorPath[_currentWaypoint]) < NextWaypointDistance)
        {
            _currentWaypoint++;
            return;
        }

        //Move in the direction of next waypoint
        Vector3 dir = (_path.vectorPath[_currentWaypoint] - transform.position).normalized;
        dir *= EntitySpeed*Time.deltaTime;
        //_characterController.SimpleMove(dir);
        transform.position += dir;
    }

    // ReSharper disable once MemberCanBePrivate.Global
    public void OnPathComplete(Path p)
    {
        Debug.Log("Path Returned. Error: " + p.error);
        if (!p.error)
        {
            _path = p;
            _currentWaypoint = 0;
        }
    }
}