using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    PlayerInputAction inputActions;
    CharacterController cc;

    Vector3 inputDir;
    public float currentSpeed = 3.0f;

    private void Awake()
    {
        inputActions = new PlayerInputAction();
        cc = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Move.performed += OnMove;
        inputActions.Player.Move.canceled += OnMove;
        inputActions.Player.Attack.performed += OnAttack;
    }

    private void OnDisable()
    {
        inputActions.Player.Attack.performed -= OnAttack;
        inputActions.Player.Move.performed -= OnMove;
        inputActions.Player.Move.canceled -= OnMove;
        inputActions.Disable();
    }

    private void Update()
    {
        cc.Move(currentSpeed * Time.deltaTime * inputDir);
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        // WASD 입력을 받아옴(+x:D, -x:A, +y:W, -y:s)
        Vector2 input = context.ReadValue<Vector2>();

        inputDir.x = input.x;
        inputDir.y = 0.0f;
        inputDir.z = input.y;
    }

    private void OnAttack(InputAction.CallbackContext context)
    {
        
    }


}
