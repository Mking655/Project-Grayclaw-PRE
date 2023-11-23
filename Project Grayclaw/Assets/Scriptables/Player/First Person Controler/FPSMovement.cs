using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FPSMovement : MonoBehaviour
{
    CharacterController controller;

    public float gravity;
    public float speed = 12f;
    bool isGrounded; // Grounded flag
    Vector3 velocity;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    void FixedUpdate()
    {
        applyMovement();
        applyGravity();
        // Move the character controller in the desired direction
        controller.Move(velocity);
    }
    void applyGravity()
    {
        //TODO
        //Apply gravity and jump effects
        isGrounded = controller.isGrounded;
        if (isGrounded)
        {
            velocity.y = 0;
        }
        else
        {
            velocity.y += gravity * Time.fixedDeltaTime;
        }
    }
    void applyMovement()
    {
        //preserve y velocity for gravity effects. X and Z axes will not preserve velocity each frame.
        float yVelocity = velocity.y;

        //Get Movement vectors
        Vector3 forward = transform.forward;
        Vector3 right = transform.right;

        // Get the input axis values for horizontal and vertical axes
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        //Apply input modifiers and speed to direction vectors and normalize so there is no diagonal exploit. 
        velocity = ((forward * verticalInput) + (right * horizontalInput));
        velocity.Normalize();
        velocity *= speed;
        velocity *= Time.fixedDeltaTime;

        //Preserve Y velocity for gravity
        velocity.y = yVelocity;
    }
}
