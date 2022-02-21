using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class CCFirstPerson_Controller : MonoBehaviour
{
    //Some variables defined for use with InputSystem
    private PlayerInput playerInput;
    private Vector2 rawInputVector;
    private Vector2 inputVector;
    private string playerInputDevice;
    private float mouseInputX;
    private float mouseInputY;
    private float verticalCameraRotation;

    //Variables that relate to the movement and feel of the controller
    private bool isMoving;
    private bool isFalling;
    private bool isJumping;
    private Vector3 moveDirection;
    [Header("Movement Variables")]
    [Tooltip("The maximum possible movespeed for the player.")]
    public float maxMoveSpeed;
    [Tooltip("The inertia level on the player's movement. Values closer to 0.1 mean they'll come to a stop faster.")]
    [Range(0.01f, 0.1f)]
    public float inertiaSpeed = 0.01f;
    [Tooltip("The force at which the player can jump.")]
    public float jumpForce;
    [Tooltip("The turning sensitivity of the Player's mouse inputs on the X axis (Left and Right)")]
    [Range(1, 100)]
    public float mouseSensitivityX = 10;
    [Tooltip("The sensitivity of the Player's mouse inputs on the Y axis (Up and Down)")]
    [Range(1, 100)]
    public float mouseSensitivityY = 1;
    [Tooltip("The maximum angle that the Player can look downwards")]
    [Range(-90, 0)]
    public int downwardLookClamp;
    [Tooltip("The maximum angle that the Player can look downwards")]
    [Range(0, 90)]
    public int upwardLookClamp;


    //Variables that relate to gravity acting upon the Player Controller
    [Header("Gravity Variables")]
    [Tooltip("The default strength of gravity, be mindful that the value of Gravity Ramp Up is added to this each frame that the player is falling.")]
    public float defaultGravityMultiplier;
    [Tooltip("The maximum strength of gravity. For example: A value of 2 would mean that that the strength can go up to double the Unity default.")]
    public float maxGravityMultiplier = 2;
    [Tooltip("The intensity that gravity ramps up by so long as the player is falling, high values can look a bit goofy, beware.")]
    public float gravityRampUp;
    [HideInInspector]
    public float gravityMultiplier = 0;
    private Vector3 currentGravity;


    //Variable for the Character Controller component on the player.
    private CharacterController controller;
    private GameObject playerCamera;

    //Called when the object containing the script is activated or instantiated.
    public void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerCamera = GetComponentInChildren<Camera>().gameObject;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void OnJumpAction(CallbackContext context)
    {
        if (context.started && !isFalling)
        {
            gravityMultiplier = defaultGravityMultiplier;
            isJumping = true;
        }
        else if (context.canceled && isFalling)
            EndJumpEarly(6);
    }

    public void SetInputVector(CallbackContext context)
    {
        rawInputVector = context.ReadValue<Vector2>();
    }

    public void SetMouseVectorX(CallbackContext context)
    {
        mouseInputX = context.ReadValue<float>();
    }

    public void SetMouseVectorY(CallbackContext context)
    {
        mouseInputY = context.ReadValue<float>();
    }

    public void OnEscapePress(CallbackContext context)
    {
        if (context.performed)
        {
            if (Cursor.visible == false)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }

    private void Update()
    {
        inputVector.x = Mathf.Lerp(inputVector.x, rawInputVector.x, inertiaSpeed);
        inputVector.y = Mathf.Lerp(inputVector.y, rawInputVector.y, inertiaSpeed);
        CalculateMovement();

        MouseLookX();
        MouseLookY();
    }

    //Calculates the movement direction of the player
    public void CalculateMovement()
    {
        //Sets the player's horizontal movement direction using the input vector from the Player's Input
        Vector3 horizontalVelocity = (transform.right * inputVector.x + transform.forward * inputVector.y) * maxMoveSpeed;

        //Checks if the player is grounded for the purposes of calculating vertical movement (jumping or falling)
        if (controller.isGrounded)
        {
            isFalling = false;
            gravityMultiplier = defaultGravityMultiplier;
            moveDirection.y = -2;
            if (isJumping)
            {
                moveDirection.y = jumpForce;
                isJumping = false;
            }
        }
        else
        {
            isFalling = true;
            currentGravity = moveDirection - (Physics.gravity * gravityMultiplier);
            moveDirection.y -= currentGravity.y * Time.deltaTime;
            CalculateGravityRamp();
        }

        //Debug.Log(moveDirection);
        moveDirection = new Vector3(horizontalVelocity.x, moveDirection.y, horizontalVelocity.z);
        controller.Move(moveDirection * Time.deltaTime);
    }

    //HORIZONTAL axis control for the camera look. (Left to Right), handled on the Y-Axis of the player object
    public void MouseLookX()
    {
        mouseInputX *= mouseSensitivityX;
        transform.Rotate(Vector3.up, mouseInputX * Time.deltaTime);
    }

    //VERTICAL axis control for the camera look. (Up to Down), handled on the X-Axis of the camera object
    public void MouseLookY()
    {
        mouseInputY = (mouseInputY * mouseSensitivityY) * Time.deltaTime;
        verticalCameraRotation -= mouseInputY;
        verticalCameraRotation = Mathf.Clamp(verticalCameraRotation, downwardLookClamp, upwardLookClamp);
        Vector3 targetRotation = transform.eulerAngles;
        targetRotation.x = verticalCameraRotation;
        playerCamera.transform.eulerAngles = targetRotation;
    }

    public void EndJumpEarly(float jumpEndForce)
    {
        if (moveDirection.y < jumpEndForce)
            return;
        else
            moveDirection.y /= 2;
    }

    public void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //Debug.Log("Direction of Character Controller collision: " + hit.moveDirection);
        if (hit.moveDirection.y > 0.3)
        {
            EndJumpEarly(0);
        }
    }

    public void CalculateGravityRamp()
    {
        gravityMultiplier += gravityRampUp * Time.deltaTime;
    }
}
