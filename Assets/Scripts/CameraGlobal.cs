using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class CameraGlobal : MonoBehaviour
{
    /*
    //TODO use these values to create an area that the camera cannot move beyond
    public float LevelBoundNorthSouth;
    public float LevelBoundEastWest;
    public float LevelBoundUppr;
    public float LevelBoundDown;
    */
    public float DefaultZoomHeight = 10.0f;
    public float ZoomIncrements = 1.0f;
    public float ZoomSpeed = 10.0f;
    public float PanSpeed = 10.0f;
    public bool CameraEnabled = true;

    private KeyCode MoveCameraForwardKey = KeyCode.W;
    private KeyCode MoveCameraBackwardKey = KeyCode.S;
    private KeyCode MoveCameraRightKey = KeyCode.D;
    private KeyCode MoveCameraLeftKey = KeyCode.A;
    private KeyCode RotateCameraLeftKey = KeyCode.Q;
    private KeyCode RotateCameraRightKey = KeyCode.E;

    //_currentRotation refers to the left edge
    private string _currentRotation = "south";
    private readonly Vector3 _southEastPosition = new Vector3(0, 315, 0);
    private readonly Vector3 _westSouthPosition = new Vector3(0, 45, 0);
    private readonly Vector3 _northWestPosition = new Vector3(0, 135, 0);
    private readonly Vector3 _eastNorthPosition = new Vector3(0, 225, 0);


    void Update()
    {
        if (Input.GetKey(MoveCameraForwardKey))
        {
            PanCamera("north");
        }
        if (Input.GetKey(MoveCameraBackwardKey))
        {
            PanCamera("south");
        }
        if (Input.GetKey(MoveCameraRightKey))
        {
            PanCamera("east");
        }
        if (Input.GetKey(MoveCameraLeftKey))
        {
            PanCamera("west");
        }
        if (Input.GetAxis("Mouse ScrollWheel") >= 0)
        {
            ZoomCamera(true);
        }
        if (Input.GetAxis("Mouse ScrollWheel") <= 0)
        {
            ZoomCamera(false);
        }

        #region Camera Rotate
        if (Input.GetKeyDown((RotateCameraLeftKey)))
        {
            string currentRotation = GetCurrentRotation();
            switch (currentRotation)
            {
                case "south":
                    RotateCamera("west", "south");
                    break;
                case "west":
                    RotateCamera("north", "west");
                    break;
                case "east":
                    RotateCamera("south", "east");
                    break;
                case "north":
                    RotateCamera("east", "north");
                    break;
            default:
                Debug.Log("Invalid Rotate Direction for Camera");
                break;
            }
        }
        if (Input.GetKeyDown((RotateCameraRightKey)))
        {
            string currentRotation = GetCurrentRotation();
            switch (currentRotation)
            {
                case "south":
                    RotateCamera("east", "north");
                    break;
                case "west":
                    RotateCamera("south", "east");
                    break;
                case "east":
                    RotateCamera("north", "west");
                    break;
                case "north":
                    RotateCamera("west", "south");
                    break;
            default:
                Debug.Log("Invalid Rotate Direction for Camera");
                break;
            }
        }

        #endregion
    }

    private void PanCamera(string moveDirection)
    {
        if (!CameraEnabled) return;
        switch (moveDirection)
        {
            case "north":
                transform.parent.position += transform.parent.forward*(PanSpeed*Time.deltaTime);
                break;
            case "south":
                transform.parent.position += -(transform.parent.forward)*(PanSpeed*Time.deltaTime);
                break;
            case "east":
                transform.parent.position += transform.parent.right*(PanSpeed*Time.deltaTime);
                break;
            case "west":
                transform.parent.position += -(transform.parent.right)*(PanSpeed*Time.deltaTime);
                break;
            default:
                Debug.Log("Invalid Move Direction for Camera");
                break;
        }
    }

    private void ZoomCamera(bool zoomin)
    {
        if (!CameraEnabled) return;
        float direction;
        if (zoomin)
        {
            direction = (1*ZoomSpeed);
        }
        else
        {
            direction = (-1*ZoomSpeed);
        }
        transform.position = transform.position + (transform.forward*direction);
    }

    private void RotateCamera(string leftEdge, string rightEdge)
    {
        if (leftEdge == "south" && rightEdge == "east")
        {
            transform.parent.eulerAngles = _southEastPosition;
            _currentRotation = "south";
        }
        else if (leftEdge == "west" && rightEdge == "south")
        {
            transform.parent.eulerAngles = _westSouthPosition;
            _currentRotation = "west";
        }
        else if (leftEdge == "north" && rightEdge == "west")
        {
            transform.parent.eulerAngles = _northWestPosition;
            _currentRotation = "north";
        }
        else if (leftEdge == "east" && rightEdge == "north")
        {
            transform.parent.eulerAngles = _eastNorthPosition;
            _currentRotation = "east";
        }
        else
        {
            Debug.Log("Incorrect Camera Rotation");
        }
    }

    public string GetCurrentRotation()
    {
        return _currentRotation;
    }
}