using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Animator animator;
    Camera _camera;
    CharacterController controller;

    public float speed = 5f;
    public float runSpeed = 8f;
    public float finalSpeed;
    public bool run;

    public float smoothness = 10f;

    private void Start()
    {
        animator = this.GetComponent<Animator>();
        _camera = Camera.main;
        controller = this.GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            run = true;
        }
        else
        {
            run = false;
        }

        InputMovement();
    }

    private void LateUpdate()
    {
        Vector3 playerRotation = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1));
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(playerRotation), Time.deltaTime * smoothness);
    }

    void InputMovement()
    {
        // 쉬프트 키를 눌렀을때 속도가 바뀐다.
        finalSpeed = (run) ? runSpeed : speed;

        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        Vector3 moveDirection = forward * Input.GetAxisRaw("Vertical") + right * Input.GetAxisRaw("Horizontal");

        controller.Move(moveDirection.normalized * finalSpeed * Time.deltaTime);

        float percent = ((run) ? 1 : 0.5f) * moveDirection.magnitude;
        animator.SetFloat("Speed", percent, 0.1f, Time.deltaTime);
    }
}
