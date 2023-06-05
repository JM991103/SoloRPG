using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpController : MonoBehaviour
{
    public float jumpHeight = 2f;
    public float groundDistance = 0.2f;
    public LayerMask groundMask;

    private CharacterController characterController;
    private bool isJumping = false;
    private Vector3 velocity;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        // Check if the character is on the ground
        bool isGrounded = Physics.CheckSphere(transform.position, groundDistance, groundMask);

        // Reset velocity if on the ground
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Handle jump input
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            isJumping = true;
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y);
        }

        // Apply gravity
        velocity.y += Physics.gravity.y * Time.deltaTime;

        // Apply movement
        characterController.Move(velocity * Time.deltaTime);
    }
}
