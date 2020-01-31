using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    private Camera cam;
    private PlayerController playerController;

    // make these public?
    private float speed = 5f;
    private float lookSensitivity = 3f;
    private float xCameraRotation = 0f;
    private float xCameraRotationLimit = 85f;
    private float defaultJumpVelocity = 11f;
    public int maxJumpCharges = 2;

    private Vector3 movementVector = Vector3.zero;
    private Vector3 horizontalMomentum = Vector3.zero;
    private Vector3 rotationVector = Vector3.zero;
    private Vector3 cameraRotationVector;
	private float jumpVelocity = 0f;

    [HideInInspector]
    public int currentJumpCharges;

    private bool moveRequest = false;
    private bool turnRequest = false;
    private bool lookRequest = false;
    private bool jumpRequest = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        cam = GetComponentInChildren<Camera>();
        playerController = GetComponent<PlayerController>();

        currentJumpCharges = maxJumpCharges;
    }

    private void Update()
    {
        // Check the inputs for 0 rather than the vectors?

        // Movement.
        float xMovementInput = Input.GetAxisRaw("Horizontal");
        float zMovementInput = Input.GetAxisRaw("Vertical");

        Vector3 xMovement = transform.right * xMovementInput;
        Vector3 zMovement = transform.forward * zMovementInput;

        movementVector = (xMovement + zMovement).normalized * speed * Time.fixedDeltaTime;

        if (playerController.isGrounded)
        {
            horizontalMomentum = movementVector;

            if (movementVector != Vector3.zero)
            {
                moveRequest = true;
            }
        }
        else
        {
            movementVector = horizontalMomentum * 0.7f + movementVector * 0.3f;

            if (movementVector != Vector3.zero)
            {
                moveRequest = true;
            }
        }

        // Turning.
        float yRotationInput = Input.GetAxisRaw("Mouse X");

        rotationVector = new Vector3(0f, yRotationInput, 0f) * lookSensitivity;

        if (rotationVector != Vector3.zero)
        {
            turnRequest = true;
        }

        // Looking.
        float xRotationInput = Input.GetAxisRaw("Mouse Y");

        xCameraRotation -= xRotationInput * lookSensitivity;
        xCameraRotation = Mathf.Clamp(xCameraRotation, -xCameraRotationLimit, xCameraRotationLimit);

        cameraRotationVector = new Vector3(xCameraRotation, 0f, 0f);

        // I don't think I need this unless I start checking xRotationInput for zero
        lookRequest = true;

        // Jumping.
        if (Input.GetButtonDown("Jump") && currentJumpCharges > 0)
        {
            if (rb.velocity.y < 0)
            {
                jumpVelocity = defaultJumpVelocity - rb.velocity.y;
            }
            else
            {
                jumpVelocity = defaultJumpVelocity;
            }

            jumpRequest = true;

            currentJumpCharges -= 1;
        }
    }

    private void FixedUpdate()
    {
        if (moveRequest)
        {
            rb.MovePosition(transform.position + movementVector);

            moveRequest = false;
        }

        if (turnRequest)
        {
            rb.MoveRotation(transform.rotation * Quaternion.Euler(rotationVector));

            turnRequest = false;
        }

        if (lookRequest)
        {
            cam.transform.localEulerAngles = cameraRotationVector;

            lookRequest = false;
        }

        if (jumpRequest)
        {
            rb.velocity += new Vector3(0f, jumpVelocity, 0f);

            jumpRequest = false;
        }
    }
}
