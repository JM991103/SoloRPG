using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

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
    public float smoothness = 10;

    public float jumpHeight = 2f;
    public float groundDistance = 0.2f;
    private bool isJumping = false;
    bool isGrounded;
    public LayerMask groundMask;
    private Vector3 velocity;

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
        anim = GetComponent<Animator>();

        player = GetComponent<Transform>();
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
        inputActions.Player.Jump.performed += OnJump;
        inputActions.Player.Attack.performed += OnAttack;
    }

    private void OnDisable()
    {
        inputActions.Player.Attack.performed -= OnAttack;
        inputActions.Player.Jump.performed -= OnJump;
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

        JumpController();
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

    private void OnJump(InputAction.CallbackContext _)
    {
        if (isGrounded)
        {
            isJumping = true;
            anim.SetTrigger("Jump");
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y);
        }
    }

    void JumpController()
    {
        isGrounded = Physics.CheckSphere(transform.position, groundDistance, groundMask);
        

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
            //anim.ResetTrigger("Jump");
        }

        velocity.y += Physics.gravity.y * Time.deltaTime;

        cc.Move(velocity * Time.deltaTime);
    }


    private void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (context.interaction is HoldInteraction) // 차지 공격
            {
                Debug.Log("차지 공격");
                
            }
            else if (context.interaction is PressInteraction)   // 일반 공격
            {
                int comboState = anim.GetInteger("ComboState"); // comboState를 애니메이터에서 읽어와서                                

                if (comboState == 0)
                {
                    anim.SetTrigger("Attack");                  // Attack 트리거 발동
                }

                if (comboState <= 3)
                {
                    comboState++;   // 콤보 상태 1 증가 시키기;
                    anim.SetInteger("ComboState", comboState);  // 애니메이터에 증가된 콤보 상태 설정
                }
            }
        }
        
    }

}
