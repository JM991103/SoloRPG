using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    PlayerInputAction playerInput;
    PlayerController player;

    public Transform target;
    public float followSpeed = 10f;
    public float sensitity = 100f;
    public float clampAngle = 70f;

    private float rotX;
    private float rotY;

    public Transform realCamera;
    public Vector3 dirNormalized;
    public Vector3 finalDir;
    public float minDistance;
    public float maxDistance;
    public float finalDistance;
    public float smoothness = 10f;
    //public Vector3 offset;

    private void Awake()
    {
        playerInput = new PlayerInputAction();
        player = FindObjectOfType<PlayerController>();
    }

    private void OnEnable()
    {
        playerInput.Player.Enable();
        playerInput.Player.MousePosition.performed += onMousePosition;
        playerInput.Player.MouseWheel.performed += onMouseWheel;
    }

    private void onMouseWheel(InputAction.CallbackContext context)
    {
        Vector2 wheel = context.ReadValue<Vector2>();

        Debug.Log(wheel);
    }

    private void onMousePosition(InputAction.CallbackContext context)
    {
        if (!player.isCursor)
        {
            rotX += context.ReadValue<Vector2>().x * sensitity * Time.deltaTime;
            rotY += context.ReadValue<Vector2>().y * sensitity * Time.deltaTime;

            rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);
            Quaternion rot = Quaternion.Euler(rotX, rotY, 0);

            transform.rotation = rot;
            
            //Debug.Log($"{rotX}, {rotY}");
        }

    }

    private void OnDisable()
    {

        playerInput.Player.MouseWheel.performed -= onMouseWheel; 
        playerInput.Player.MousePosition.performed -= onMousePosition;
        playerInput.Player.Disable();
    }

    //private void Start()
    //{
    //    rotX = transform.localRotation.eulerAngles.x;
    //    rotY = transform.localRotation.eulerAngles.y;

    //    dirNormalized = realCamera.localPosition.normalized;
    //    finalDistance = realCamera.localPosition.magnitude;
    //}

    //private void Update()
    //{
    //    rotX += -(Input.GetAxis("Mouse Y")) * sensitity * Time.deltaTime;
    //    rotY += Input.GetAxis("Mouse X") * sensitity * Time.deltaTime;

    //    rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);

    //    Quaternion rot = Quaternion.Euler(rotX, rotY, 0);
    //    transform.rotation = rot;
    //}

    //private void LateUpdate()
    //{
    //    //transform.position = target.position + offset;

    //    transform.position = Vector3.MoveTowards(transform.position, target.position, followSpeed * Time.deltaTime);

    //    finalDir = transform.TransformPoint(dirNormalized * maxDistance);

    //    RaycastHit hit;
    //    if (Physics.Linecast(transform.position, finalDir, out hit))
    //    {
    //        finalDistance = Mathf.Clamp(hit.distance, minDistance, maxDistance);
    //    }
    //    else
    //    {
    //        finalDistance = maxDistance;
    //    }
    //    realCamera.localPosition = Vector3.Lerp(realCamera.localPosition, dirNormalized * finalDistance, Time.deltaTime * smoothness);
    //}
}
