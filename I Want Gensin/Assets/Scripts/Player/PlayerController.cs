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
        // WASD �Է��� �޾ƿ�(+x:D, -x:A, +y:W, -y:s)
        Vector2 input = context.ReadValue<Vector2>();

        inputDir.x = input.x;
        inputDir.y = 0.0f;
        inputDir.z = input.y;

        //Debug.Log($"{inputDir.x} {inputDir.z}");
        
        if (!context.canceled)
        {
            Quaternion cameraYRotation = Quaternion.Euler(0, Camera.main.transform.rotation.eulerAngles.y, 0);  // ī�޶��� y�� ȸ���� �и�
            // ī�޶��� y�� ȸ���� inputDir�� ���Ѵ�. => inputDir�� ī�޶� xz���󿡼� �ٶ󺸴� ������ ��ġ��Ŵ
            inputDir = cameraYRotation * inputDir;

            targetRotation = Quaternion.LookRotation(inputDir); // inputDir �������� �ٶ󺸴� ȸ�� �����

            if (moveMode == MoveMode.Walk)
            {
                anim.SetFloat("Speed", 0.5f);   // walk���� �ȴ� �ִϸ��̼�

                //anim.SetFloat("InputY", inputDir.z);
                //anim.SetFloat("InputX", inputDir.x);
            }
            else
            {
                anim.SetFloat("Speed", 1.0f);   // Run���� �޸��� �ִϸ��̼�
            }

            //inputDir.y = -2.0f;
        }
        else
        {
            //inputDir = Vector3.zero;           
            anim.SetFloat("Speed", 0.0f);   // �Է��� �ȵ�� ������ ��� �ִϸ��̼�.
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
                anim.SetFloat("Speed", 1.0f);   // �����̴� ���� ���� ����ϴ� �ִϸ��̼� ����
            }
        }
        else
        {            
            moveMode = MoveMode.Walk;
            currentSpeed = walkSpeed;
            if (inputDir != Vector3.zero)
            {
                anim.SetFloat("Speed", 0.5f);   // �����̴� ���� ���� ����ϴ� �ִϸ��̼� ����
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
            if (context.interaction is HoldInteraction) // ���� ����
            {
                Debug.Log("���� ����");
                
            }
            else if (context.interaction is PressInteraction)   // �Ϲ� ����
            {
                int comboState = anim.GetInteger("ComboState"); // comboState�� �ִϸ����Ϳ��� �о�ͼ�                                

                if (comboState == 0)
                {
                    anim.SetTrigger("Attack");                  // Attack Ʈ���� �ߵ�
                }

                if (comboState <= 3)
                {
                    comboState++;   // �޺� ���� 1 ���� ��Ű��;
                    anim.SetInteger("ComboState", comboState);  // �ִϸ����Ϳ� ������ �޺� ���� ����
                }
            }
        }
        
    }

}
