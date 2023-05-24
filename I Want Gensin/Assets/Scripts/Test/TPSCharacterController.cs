using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TPSCharacterController : MonoBehaviour
{
    [SerializeField]
    Transform characterBody;
    [SerializeField]
    Transform cameraArm;

    Animator anim;

    PlayerInputAction inputAction;

    private void Awake()
    {
        inputAction = new PlayerInputAction();
    }

    private void Start()
    {
        anim = characterBody.GetComponent<Animator>();        
    }

    private void OnEnable()
    {
        inputAction.Player.MousePosition.Enable();
        inputAction.Player.MousePosition.performed += OnTPSController;
    }

    private void OnTPSController(InputAction.CallbackContext context)
    {
        Vector2 mouseDelta = context.ReadValue<Vector2>().normalized;
        
        Vector3 camAngle = cameraArm.rotation.eulerAngles;

        cameraArm.rotation = Quaternion.Euler(camAngle.x = mouseDelta.y, camAngle.y + mouseDelta.x, camAngle.z);

        Debug.Log(cameraArm.rotation);
    }

    private void OnDisable()
    {
        inputAction.Player.MousePosition.performed -= OnTPSController;
        inputAction.Player.MousePosition.Disable();
    }

    private void Update()
    {
        
    }

    //void LookAround()
    //{
    //    Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
    //    Vector3 camAngle = cameraArm.rotation.eulerAngles;

    //    cameraArm.rotation = Quaternion.Euler(camAngle.x = mouseDelta.y, camAngle.y + mouseDelta.x, camAngle.z);
    //}
}
