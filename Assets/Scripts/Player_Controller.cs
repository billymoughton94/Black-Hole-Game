﻿using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player_Controller : MonoBehaviour {
    [SerializeField] Transform playerCamera = null; // Used to access transform of the player camera
    [SerializeField] float mouseSensitivity = 3.5f;
    [SerializeField] float walkSpeed = 10.5f;
    [SerializeField] float runSpeed = 9.0f;
    [SerializeField] float runBuildUp = 9.0f;
    [SerializeField] private float movementSpeed = 10.5f;

    [SerializeField] float gravity = -13.0f;
    [SerializeField] [Range(0.0f, 0.5f)] float moveSmoothTime = 0.3f;

    [SerializeField] AnimationCurve jumpFallOff;
    [SerializeField] float jumpMultiplier = 10f;
    [SerializeField] KeyCode jumpKey = KeyCode.Space;
    [SerializeField] KeyCode runKey;
    bool isJumping;

    [SerializeField] bool lockCursor = true; // This variable will be used to lock the cursor and will start as true

    float cameraPitch = 0.0f; // Keeps track of Cameras x rotation (Used for vertical movement of camera)
    float velocityY = 0.0f;
    CharacterController controller = null;
    private UI_Controller UI;

    Vector2 currentDir = Vector2.zero;
    Vector2 currentDirVelocity = Vector2.zero;

    void Start() {
        controller = GetComponent<CharacterController>();
        UI = GameObject.Find("UI").GetComponent<UI_Controller>();
        // Conditional statement that checks if the lockCursor boolean is set to true or false in order to not only lock the cursor to the center but to also make it invisible
        if (lockCursor) {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    private void Awake()
    {
        Time.timeScale = 1; // begins the game
    }

    void Update() {
        UpdateMouseLook();
        UpdateMovement();
        inventoryInput();
        craftingInput();
    }

    // Dedicated update method for mouse movement functionality  
    void UpdateMouseLook() {
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        cameraPitch = cameraPitch - (mouseDelta.y * mouseSensitivity); // Responsible for vertical positioning of camera (The reason we subtract is to get the inverse of the mouseDelta in order to avoid inverted vertical movement
        cameraPitch = Mathf.Clamp(cameraPitch, -90.0f, 90.0f); //This line is needed so the player cannot move beyond  180 degrees up or down. Camera pitch is set to 0.0 and starts that way so if the player looks up or down they will be going max 90 degrees

        playerCamera.localEulerAngles = Vector3.right * cameraPitch;
        transform.Rotate(Vector3.up * mouseDelta.x * mouseSensitivity); //Responsible for manipulating the transform of the player object to rotate the player horrizontally multiplied by mouse 
    }

    // Dedicated update method for movement 
    void UpdateMovement() {
        Vector2 targetDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        targetDir.Normalize();

        currentDir = Vector2.SmoothDamp(currentDir, targetDir, ref currentDirVelocity, moveSmoothTime);

        if (controller.isGrounded)
            velocityY = 0.0f;

        velocityY += gravity * Time.deltaTime;

        Vector3 velocity = (transform.forward * currentDir.y + transform.right * currentDir.x) * walkSpeed + Vector3.up * velocityY;

        controller.Move(velocity * Time.deltaTime);

        SetMovementSpeed();
        JumpInput();
    }

    //Method to detect when jumping key is pressed 
    private void JumpInput() {
        if (Input.GetKeyDown(jumpKey) && !isJumping) {
            isJumping = true;
            StartCoroutine(JumpEvent());
        }
    }

    private void inventoryInput() {
        if (Input.GetKeyUp(KeyCode.I)) {
            UI.togglePanel("Inventory");
        }
    }
    
    private void craftingInput() {
        if (Input.GetKeyUp(KeyCode.C)) {
            UI.togglePanel("Crafting");
        }
    }

    private IEnumerator JumpEvent() //Method for jumping physics 
    {
        float timeInAir = 0.0f;

        do {
            float jumpForce = jumpFallOff.Evaluate(timeInAir);
            controller.Move(Vector3.up * jumpForce * jumpMultiplier * Time.deltaTime);
            timeInAir += Time.deltaTime;
            yield return null;
        } while (!controller.isGrounded && controller.collisionFlags != CollisionFlags.Above);

        isJumping = false;
    }

    void OnTriggerEnter(Collider collider) { //Method for how player interacts with key objects in the game. (This is to simluate picking it up and storing it)
        if (collider.gameObject.name == "Antenna" || collider.gameObject.name == "Ship Body" || collider.gameObject.name == "Fuel Containers") {
            Game_Manager.tickOffItem(collider.gameObject);
            Destroy(collider.gameObject);
        }
    }

    private void SetMovementSpeed()
    {
        if(Input.GetKey(runKey))
        {
            walkSpeed = Mathf.Lerp(walkSpeed, runSpeed, Time.deltaTime * runBuildUp);
        }
        else
        {
            walkSpeed = Mathf.Lerp(walkSpeed, movementSpeed, Time.deltaTime * runBuildUp);
        }
    }
}
