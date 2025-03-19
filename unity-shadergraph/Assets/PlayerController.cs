using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float sprintMultiplier = 2f;
    
    [Header("Look Settings")]
    [SerializeField] private float mouseSensitivity = 2f;
    [SerializeField] private float lookUpLimit = 90f;
    [SerializeField] private float lookDownLimit = -90f;
    
    private Camera playerCamera;
    private float rotationX = 0f;
    private CharacterController characterController;
    
    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        playerCamera = GetComponentInChildren<Camera>();
        
        if (playerCamera == null)
        {
            Debug.LogError("No camera found as a child of the player object!");
        }
        
        // Lock and hide the cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    private void Update()
    {
        HandleMouseLook();
        HandleMovement();
        
        // Toggle cursor lock with Escape key
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = Cursor.lockState == CursorLockMode.Locked ? 
                CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = !Cursor.visible;
        }
    }
    
    private void HandleMouseLook()
    {
        if (Cursor.lockState != CursorLockMode.Locked)
            return;
            
        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
        
        // Rotate the camera up/down
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, lookDownLimit, lookUpLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
        
        // Rotate the player left/right
        transform.Rotate(Vector3.up * mouseX);
    }
    
    private void HandleMovement()
    {
        // Get keyboard input
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        
        // Calculate movement direction
        Vector3 moveDirection = transform.forward * vertical + transform.right * horizontal;
        
        // Normalize to prevent faster diagonal movement
        if (moveDirection.magnitude > 1f)
            moveDirection.Normalize();
            
        // Apply sprint multiplier if shift is held
        float currentSpeed = moveSpeed;
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            currentSpeed *= sprintMultiplier;
            
        // Apply movement (with no gravity)
        Vector3 movement = moveDirection * currentSpeed * Time.deltaTime;
        
        // No gravity applied - y component remains unchanged
        characterController.Move(movement);
    }
}
