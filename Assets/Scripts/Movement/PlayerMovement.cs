using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;

    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    [HideInInspector] public float walkSpeed;
    [HideInInspector] public float sprintSpeed;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode resetKey = KeyCode.R;
    public KeyCode dash = KeyCode.LeftShift;


    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask ground;
    bool grounded;

    
    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    [Header("Cooldowns")]
    public float dashCooldown;
    float dashcd = 0;
    
   

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        readyToJump = true;
    }

    private void Update()
    {
        // ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, ground);

        SpeedControl();

        // handle drag
        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;

        
        if (Input.GetKeyDown(dash) && Time.time > dashcd)
        {
            moveSpeed = 200;
            Invoke("ResetDash", 0.1f);
            dashcd = Time.time + dashCooldown;
        }
        MyInput();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");


        // when to jump
        if(Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }
        else
        {
            MovePlayer();
        }

        
    }

    private void MovePlayer()
    {


        // calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // rotate physical body
        transform.rotation = orientation.rotation;

        // on ground
        if (grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 500f * Time.deltaTime, ForceMode.Force);

        // in air
        else if(!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 500f * airMultiplier * Time.deltaTime, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // limit velocity if needed
        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        // reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    private void ResetJump()
    {
        readyToJump = true;
    }

    private void ResetDash()
    {
        moveSpeed = 10;
    }
}