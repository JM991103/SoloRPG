using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    PlayerInputAction inputActions;
    CharacterController cc;
    Animator anim;
    Transform player;

    Vector3 inputDir;
    public float currentSpeed = 3.0f;
    public float walkSpeed = 3.0f;
    public float runSpeed = 5.0f;
    Quaternion targetRotation = Quaternion.identity;

    bool isMoveChange = false;
    public bool isCursor = false;


    Vector2 mouseDelta;

    enum MoveMode
    {
        Walk = 0,
        Run
    }

    MoveMode moveMode = MoveMode.Walk;

    private void Awake()
    {
        inputActions = new PlayerInputAction();
        cc = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();

        player = GetComponentInChildren<Transform>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Move.performed += OnMove;
        inputActions.Player.Move.canceled += OnMove;
        inputActions.Player.MoveModeChange.performed += OnMoveModeChange;
        inputActions.Player.MoveModeChange.canceled += OnMoveModeChange;
        inputActions.Player.CursorLock.performed += OnCursorLock;
        inputActions.Player.CursorLock.canceled += OnCursorLock;        
        inputActions.Player.Attack.performed += OnAttack;
    }

    private void onMousePosition(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    private void OnDisable()
    {
        inputActions.Player.Attack.performed -= OnAttack;        
        inputActions.Player.CursorLock.performed -= OnCursorLock;
        inputActions.Player.CursorLock.canceled -= OnCursorLock;
        inputActions.Player.MoveModeChange.performed -= OnMoveModeChange;
        inputActions.Player.MoveModeChange.canceled -= OnMoveModeChange;
        inputActions.Player.Move.performed -= OnMove;
        inputActions.Player.Move.canceled -= OnMove;
        inputActions.Disable();
    }

    private void Update()
    {
        cc.Move(currentSpeed * Time.deltaTime * inputDir);

        player.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10 * Time.deltaTime);
                
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        // WASD 입력을 받아옴(+x:D, -x:A, +y:W, -y:s)
        Vector2 input = context.ReadValue<Vector2>();

        inputDir.x = input.x;
        inputDir.y = 0.0f;
        inputDir.z = input.y;

        //Debug.Log($"{inputDir.x} {inputDir.z}");
        
        if (!context.canceled)
        {
            Quaternion cameraYRotation = Quaternion.Euler(0, Camera.main.transform.rotation.eulerAngles.y, 0);  // 카메라의 y축 회전만 분리
            // 카메라의 y축 회전을 inputDir에 곱한다. => inputDir과 카메라가 xz평면상에서 바라보는 방향을 일치시킴
            inputDir = cameraYRotation * inputDir;

            targetRotation = Quaternion.LookRotation(inputDir); // inputDir 방향으로 바라보는 회전 만들기

            if (moveMode == MoveMode.Walk)
            {
                anim.SetFloat("Speed", 0.5f);   // walk모드면 걷는 애니메이션

                //anim.SetFloat("InputY", inputDir.z);
                //anim.SetFloat("InputX", inputDir.x);
            }
            else
            {
                anim.SetFloat("Speed", 1.0f);   // Run모드면 달리는 애니메이션
            }

            //inputDir.y = -2.0f;
        }
        else
        {
            //inputDir = Vector3.zero;           
            anim.SetFloat("Speed", 0.0f);   // 입력이 안들어 왔으면 대기 애니메이션.
        }

    }

    private void OnMoveModeChange(InputAction.CallbackContext _)
    {
        isMoveChange = !isMoveChange;
        MoveChange(isMoveChange);
    }

    void MoveChange(bool change)
    {
        if (change)
        {
            moveMode = MoveMode.Run;
            currentSpeed = runSpeed;
            if (inputDir != Vector3.zero)
            {
                anim.SetFloat("Speed", 1.0f);   // 움직이는 중일 때만 재생하는 애니메이션 변경
            }
        }
        else
        {
            moveMode = MoveMode.Walk;
            currentSpeed = walkSpeed;
            if (inputDir != Vector3.zero)
            {
                anim.SetFloat("Speed", 0.5f);   // 움직이는 중일 때만 재생하는 애니메이션 변경
            }
        }
    } 

    private void OnCursorLock(InputAction.CallbackContext _)
    {
        isCursor = !isCursor;
        CursorVisible(isCursor);
    }

    void CursorVisible(bool isCursor)
    {
        
        if (isCursor)
        {            
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    private void OnAttack(InputAction.CallbackContext context)
    {
        
    }
}
