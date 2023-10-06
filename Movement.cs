using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Settings")]
    public Camera playerCam;
    public float sensitivity;
    public float speed;
    public float jumpForce;

    // Rotation on X axis for mouse movement
    private float yRotation;

    // Variables for movement smoothing (prevents instant start/stop)
    private float smoothTime = .2f;
    Vector2 direction = Vector2.zero;
    Vector2 currVelocity = Vector2.zero;

    // CharacterController for movement
    CharacterController controller;

    // Gravity and jumping
    private float gravity = -10.0f;
    private float yVelocity;

    // Start is called before the first frame update
    void Start()
    {
        // Cursor lock and visibility
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        MouseLook();
        Motion();
    }

    // Mouse Input + Player Camera Motion
    void MouseLook()
    {
        // Get player mouse input
        Vector2 mouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        // Horizontal motion: Rotate player object based on Mouse X around Y axis
        transform.Rotate(Vector3.up * mouseInput.x * sensitivity);

        // Vertical motion: Rotate player object + camera based on Mouse Y around X axis
        yRotation -= mouseInput.y * sensitivity;
        yRotation = Mathf.Clamp(yRotation, -90.0f, 90.0f); // Prevent camera from flipping upside down by wrapping vertically
        playerCam.transform.localEulerAngles = Vector3.right * yRotation;
    }

    // Keyboard Movement + Gravity
    void Motion()
    {
        // Get keyboard input on WASD
        Vector2 WASDInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        WASDInput.Normalize();

        // Smoothing out movement (takes current direction and tries to smooth the movement toward the direction of player input)
        direction = Vector2.SmoothDamp(direction, WASDInput, ref currVelocity, smoothTime);

        // Account for vertical motion and gravity
        if (controller.isGrounded)
            yVelocity = 0.0f;

        yVelocity += gravity * Time.deltaTime;

        // Get jump input on Space
        if (Input.GetKeyDown(KeyCode.Space) && controller.isGrounded)
            yVelocity = jumpForce;

        // Determine movement direction and magnitude
        Vector3 velocity = (transform.forward * direction.y + transform.right * direction.x) * speed;
        velocity.y = yVelocity;

        // Use character controller component to move the player object
        controller.Move(velocity * Time.deltaTime);
    }
}