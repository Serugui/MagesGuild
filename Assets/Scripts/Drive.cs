using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Drive : MonoBehaviour, Controls.IPlayerActions
{
    CharacterController controller;
    private PlayerInput playerInput;
    public Camera topDownCamera;

    public float moveSpeed = 5f;
    public float currentSpeed = 0;
    public float rotationSpeed = 1000f;
    

    Vector3 forward, right, rotationTarget;
    Vector2 mouseLook, joystickLook;

    // Variable to store ongoing movement input
    private Vector2 movementInput;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();

        forward = (GameObject.Find("CameraTarget").GetComponentInChildren<Camera>()).transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;
    }
    void Update()
    {
        if (playerInput.currentControlScheme == "Keyboard&Mouse")
        {
            Ray ray = topDownCamera.ScreenPointToRay(mouseLook);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Assign the hit point as the rotation target
                rotationTarget = hit.point;
            }

            movePlayerWithAim();
            
        }
        else if (playerInput.currentControlScheme == "Gamepad")
        {
            if (joystickLook.magnitude == 0f)
            {
                Locomotion();
            }
            else
            {
                movePlayerWithAim();
            }
            
        }
    }

    void Locomotion()
    {
        // Calculate the movement direction based on camera orientation
        Vector3 moveDirection = (forward * movementInput.y + right * movementInput.x).normalized;

        // Magnitude of the movement vector (current speed)
        currentSpeed = moveDirection.magnitude * moveSpeed;

        if (moveDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
        // Move the player in the calculated direction
        controller.Move(moveDirection * moveSpeed * Time.deltaTime);
    }

    void movePlayerWithAim()
    {
        if (playerInput.currentControlScheme == "Keyboard&Mouse")
        {
            var lookPos = rotationTarget - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);

            Vector3 aimDir = new Vector3(rotationTarget.x, 0f, rotationTarget.z);

            if (aimDir != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.15f);
            }
        }
        else
        {
            Vector3 aimDir = new Vector3(joystickLook.x, 0f, joystickLook.y);

            if (aimDir != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(aimDir), 0.15f);
            }
            
        }

        Vector3 moveDirection = (forward * movementInput.y + right * movementInput.x).normalized;

        controller.Move(moveDirection * moveSpeed * Time.deltaTime);

    }

    public void OnMove(InputAction.CallbackContext context)
    {
        // Get the movement values from the input context
        movementInput = context.ReadValue<Vector2>();
    }

    public void OnMouseLook(InputAction.CallbackContext context)
    {
        // Get the movement values from the input context
        mouseLook = context.ReadValue<Vector2>();
    }

    public void OnJoystickLook(InputAction.CallbackContext context)
    {
        // Get the movement values from the input context
        joystickLook = context.ReadValue<Vector2>();
    }
}
