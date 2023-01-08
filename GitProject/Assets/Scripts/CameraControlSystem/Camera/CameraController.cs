using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    [Header("Settings")]
    public Transform cameraTransform;
    public Transform followTransform;
    
    [Header("Properties")]
    [Header("Movement")]
    // The speed of the camera movement
    public float movementSpeed;
    public float normalSpeed;
    public float fastSpeed;

    // The time taken for camera to move
    public float movementTime;
    
    // The position of the camera
    private Vector3 newPosition;

    [Header("Rotation")]
    // The amount of rotation
    public float rotationAmount;
    private Quaternion newRotation;

    // The postion when rotation
    public Vector3 rotateStartPosition;
    public Vector3 rotateCurrentPosition;

    [Header("Zoom")]
    // The amount of Zoom
    public Vector3 zoomAmount;
    private Vector3 newZoom;

    // The amount of limit for zooming
    public float minZoom, maxZoom;

    //[Header("Drag")]
    // The position when dragging
    // public Vector3 dragStartPosition;
    // public Vector3 dragCurrentPosition;

    // Start is called before the first frame update
    private void Start()
    {
        instance= this;

        newPosition = transform.position;
        newRotation = transform.rotation;
        newZoom = cameraTransform.localPosition;
    }

    // Update is called once per frame
    private void Update()
    {
        // To check whether the camera is focused on something
        if(followTransform != null)
        {
            transform.position = followTransform.position;
        }
        else
        {
            HandleMouseInput();
            HandleMovementInput();
        }

        // To unfocus
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            followTransform = null;
        }
    }

    private void HandleMouseInput()
    {
        // To detect mouse scroll wheel to zoom
        if(Input.mouseScrollDelta.y != 0)
        {
            newZoom += Input.mouseScrollDelta.y * zoomAmount;
            newZoom.y = Mathf.Clamp(newZoom.y, minZoom, maxZoom);
            newZoom.z = Mathf.Clamp(newZoom.z, minZoom, maxZoom);
        }

        // To detect if the LMB is clicked to drag
        // if (Input.GetMouseButtonDown(0))
        // {
        //     Plane plane = new Plane(Vector3.up, Vector3.zero);

        //     Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //     float entry;

        //     if(plane.Raycast(ray, out entry))
        //     {
        //         dragStartPosition = ray.GetPoint(entry);
        //     }
        // }

        // if(Input.GetMouseButton(0))
        // {
        //     Plane plane = new Plane(Vector3.up, Vector3.zero);

        //     Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //     float entry;

        //     if(plane.Raycast(ray, out entry))
        //     {
        //         dragCurrentPosition = ray.GetPoint(entry);

        //         newPosition = transform.position + dragStartPosition - dragCurrentPosition;
        //     }
        // }

        // To detect the RMB is clicked to rotate
        if(Input.GetMouseButtonDown(1))
        {
            rotateStartPosition = Input.mousePosition;
        }
        if(Input.GetMouseButton(1))
        {
            rotateCurrentPosition = Input.mousePosition;

            Vector3 difference = rotateStartPosition - rotateCurrentPosition;

            rotateStartPosition = rotateCurrentPosition;

            newRotation *= Quaternion.Euler(Vector3.up * (-difference.x / 5f));
        }
    }

    private void HandleMovementInput()
    {
        // Determine whether the shift key is pressed to increase the speed of the camera movement
        if(Input.GetKey(KeyCode.LeftShift))
        {
            movementSpeed = fastSpeed;
        }
        else
        {
            movementSpeed = normalSpeed;
        }

        // Forward and Backward movement of the camera
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            newPosition += transform.forward * movementSpeed;
        }
        if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            newPosition += transform.forward * -movementSpeed;
        }

        // Left and Right movement of the camera
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            newPosition += transform.right * -movementSpeed;
        }
        if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            newPosition += transform.right * movementSpeed;
        }

        // To lerp for smooth movement
        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * movementTime);
        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newZoom, Time.deltaTime * movementTime);
    }
}