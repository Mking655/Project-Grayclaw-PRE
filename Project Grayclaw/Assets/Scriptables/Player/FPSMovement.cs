using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FPSMovement : MonoBehaviour
{
    CharacterController controller;

    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public float crouchSpeed = 6f;
    public float sprintSpeed = 18f;
    public float slideSpeed = 20f;
    public float slideDuration = 0.75f;
    public float ladderSpeed = 5f;

    Vector3 velocity;
    bool isGrounded;
    bool isCrouching = false;
    bool isSprinting = false;
    bool isSliding = false;
    bool isClimbing = false;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Ground check
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Small force to ensure the player stays grounded
        }

        // Sprinting
        if (Input.GetButtonDown("Sprint"))
        {
            isSprinting = true;
        }
        if (Input.GetButtonUp("Sprint"))
        {
            isSprinting = false;
        }

        // Crouching and sliding
        if (Input.GetButtonDown("Crouch"))
        {
            // If crouching and there's enough room above, toggle crouch state
            if (isCrouching && CanUncrouch())
            {
                ToggleCrouchSlide();
            }
            else if (!isCrouching) // If not crouching, crouch
            {
                ToggleCrouchSlide();
            }
        }
        // Jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            isClimbing = false;
        }

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // Movement input
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;

        // Climbing
        if (isClimbing)
        {
            ClimbLadder();
        }
        // Sliding
        else if (isSliding)
        {
            Slide();
        }
        // Regular movement
        else
        {
            float currentSpeed = isCrouching ? crouchSpeed : (isSprinting ? sprintSpeed : speed);
            controller.Move(move * currentSpeed * Time.deltaTime);
        }

    }

    private void ToggleCrouchSlide()
    {
        isCrouching = !isCrouching;
        isSliding = isSprinting && isCrouching; // Begin sliding if sprinting while crouching
        if (isSliding)
        {
            StartCoroutine(EndSlideAfterTime(slideDuration));
        }
        AdjustControllerHeight(isCrouching);
    }

    private void AdjustControllerHeight(bool isCrouching)
    {
        controller.height = isCrouching ? controller.height / 2 : controller.height * 2;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ladder"))
        {
            isClimbing = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Ladder"))
        {
            isClimbing = false;
        }
    }

    void ClimbLadder()
    {
        float y = Input.GetAxis("Vertical");
        Vector3 climbVelocity = new Vector3(0, y * ladderSpeed, 0);
        controller.Move(climbVelocity * Time.deltaTime);
    }

    void Slide()
    {
        controller.Move(transform.forward * slideSpeed * Time.deltaTime);
    }
    bool CanUncrouch()
    {
        // Check for space above the player
        RaycastHit hit;
        float checkHeight = controller.height;
        Vector3 origin = transform.position - new Vector3(0, controller.height/2, 0); // Start from bottom of player

        // If raycast hits something, there isn't enough room to stand up
        if (Physics.Raycast(origin, Vector3.up, out hit, checkHeight))
        {
            return false; // Not enough room to uncrouch
        }
        return true; // Enough room to uncrouch
    }

    IEnumerator EndSlideAfterTime(float duration)
    {
        yield return new WaitForSeconds(duration);
        isSliding = false; // End sliding
    }
}
