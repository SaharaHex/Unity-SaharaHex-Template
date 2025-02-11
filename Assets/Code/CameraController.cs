using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform cameraTransform;

    public float normalSpeed;
    public float fastSpeed;
    public float movementSpeed;
    public float movementTime;
    public float rotationAmount;
    public Vector3 zoomAmount;

    public Vector3 newPosition;
    public Quaternion newRotation;
    public Vector3 newZoom;

    public Vector3 dragStartPosition;
    public Vector3 dragCurrentPosition;
    public Vector3 rotateStartPosition;
    public Vector3 rotateCurrentPosition;

    public float leftLimit;
    public float rightLimit;
    public float bottomLimit;
    public float topLimit;

    public float ZoomLeftLimit;
    public float ZoomRightLimit;
    public float ZoomBottomLimit;
    public float ZoomTopLimit;
    public float ZoomFromLimit;
    public float ZoomDepthLimit;

    // Start is called before the first frame update
    void Start()
    {
        //transform is CameraRig
        newPosition = transform.position;
        newRotation = transform.rotation;
        newZoom = cameraTransform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        HandleMouseInput();
        HandleMovementInput();
    }

    void HandleMouseInput()
    {
        if (Input.mouseScrollDelta.y != 0)
        {
            if (newZoom.y >= ZoomBottomLimit && newZoom.y <= ZoomTopLimit)
                newZoom += Input.mouseScrollDelta.y * zoomAmount;
            else if (newZoom.y < ZoomBottomLimit)
            {
                newZoom.y = 250; newZoom.z = -250; //this is MianCamera position
            }
            else if (newZoom.y > ZoomTopLimit)
            {
                newZoom.y = 250; newZoom.z = -250; //this is MianCamera position
            }
        }

        if (Input.GetMouseButtonDown(0)) //left
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            float entry;
            if (plane.Raycast(ray, out entry))
            {
                dragStartPosition = ray.GetPoint(entry);
            }
        }
        if (Input.GetMouseButton(0)) //still clicked down
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            float entry;
            if (plane.Raycast(ray, out entry))
            {
                dragCurrentPosition = ray.GetPoint(entry);

                newPosition = transform.position + dragStartPosition - dragCurrentPosition;
            }
        }

        if (Input.GetMouseButtonDown(2)) //middle
        {
            rotateStartPosition = Input.mousePosition;
        }
        if (Input.GetMouseButton(2)) //still clicked down
        {
            rotateCurrentPosition = Input.mousePosition;

            Vector3 difference = rotateStartPosition - rotateCurrentPosition;

            rotateStartPosition = rotateCurrentPosition;

            newRotation *= Quaternion.Euler(Vector3.up * (-difference.x / 5f));
        }

        cameraTransform.localPosition = new Vector3
        (
            Mathf.Clamp(cameraTransform.localPosition.x, ZoomLeftLimit, ZoomRightLimit),
            Mathf.Clamp(cameraTransform.localPosition.y, ZoomBottomLimit, ZoomTopLimit),
            Mathf.Clamp(cameraTransform.localPosition.z, ZoomFromLimit, ZoomDepthLimit)
        );
    }

    void HandleMovementInput()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            movementSpeed = fastSpeed;
        }
        else
        {
            movementSpeed = normalSpeed;
        }


        //todo need to add xbox
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            if (newPosition.z <= topLimit)
                newPosition += (transform.forward * movementSpeed);
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            if (newPosition.z >= bottomLimit)
                newPosition += (transform.forward * -movementSpeed);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            if (newPosition.x <= rightLimit)
                newPosition += (transform.right * movementSpeed);
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            if (newPosition.x >= leftLimit)
                newPosition += (transform.right * -movementSpeed);
        }

        if (Input.GetKey(KeyCode.Q))
        {
            newRotation *= Quaternion.Euler(Vector3.up * rotationAmount);
        }
        if (Input.GetKey(KeyCode.E))
        {
            newRotation *= Quaternion.Euler(Vector3.up * -rotationAmount);
        }

        if (Input.GetKey(KeyCode.R)) //todo to much zoom, but Mouse zoom is ok
        {
            if (newZoom.y >= ZoomBottomLimit && newZoom.y <= ZoomTopLimit)
                newZoom += zoomAmount;
            else if (newZoom.y < ZoomBottomLimit)
            {
                newZoom.y = 250; newZoom.z = -250; //this is MianCamera position
            }
            else if (newZoom.y > ZoomTopLimit)
            {
                newZoom.y = 250; newZoom.z = -250; //this is MianCamera position
            }

        }
        if (Input.GetKey(KeyCode.F)) //todo to much zoom, but Mouse zoom is ok
        {
            if (newZoom.y >= ZoomBottomLimit && newZoom.y <= ZoomTopLimit)
                newZoom -= zoomAmount;
            else if (newZoom.y < ZoomBottomLimit)
            {
                newZoom.y = 250; newZoom.z = -250; //this is MianCamera position
            }
            else if (newZoom.y > ZoomTopLimit)
            {
                newZoom.y = 250; newZoom.z = -250; //this is MianCamera position
            }
        }

        //to make it smooth
        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * movementTime);
        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newZoom, Time.deltaTime * movementTime);

        //fix position limits
        transform.position = new Vector3
        (
            Mathf.Clamp(transform.position.x, leftLimit, rightLimit),
            Mathf.Clamp(transform.position.y, bottomLimit, topLimit),
            transform.position.z
        );
    }

    private void OnDrawGizmos()
    {
        //draw a box around our camera boundary display in debug
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(leftLimit, topLimit), new Vector3(rightLimit, topLimit));
        Gizmos.DrawLine(new Vector3(leftLimit, bottomLimit), new Vector3(rightLimit, bottomLimit));
        Gizmos.DrawLine(new Vector3(leftLimit, topLimit), new Vector3(leftLimit, bottomLimit));
        Gizmos.DrawLine(new Vector3(rightLimit, topLimit), new Vector3(rightLimit, bottomLimit));
    }
}
