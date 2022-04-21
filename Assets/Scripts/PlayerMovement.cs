using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;


    public float walkSpeed = 20f,
    sprintSpeed = 18f,
    crouchMultiplier = 2f,
    JumpHeight = 3f,
    standingHeight = 3.2f,
    crouchHeight = 1.8f,
    gravity = -9.81f,
    groundDistance = 0.4f;

   
    private float regular = 0f;
    private bool isGrounded ,isCrouching;

    public Transform GroundCheck;
    public LayerMask groundmask; 
    private Vector3 velocity;

    
    // Start is called before the first frame update
    void Start()
    {
       
        regular = walkSpeed;
        controller.height = standingHeight;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(GroundCheck.position, groundDistance, groundmask);

        if(isGrounded && velocity.y < 0)
        {
            controller.slopeLimit = 45.0f;
            velocity.y = -2f; 
        }

        Move();
        Crouch();
        Jump();

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

    }

    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

       

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            walkSpeed = sprintSpeed;
        }
        else
        {
            walkSpeed = regular;
        }

           Vector3 move = Vector3.Normalize(transform.right * x + transform.forward * z);

        controller.Move(move * walkSpeed * Time.deltaTime);
    }


    void Crouch()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl)&& isGrounded)
        {
            isCrouching = true;
        }
        else
        {
            isCrouching = false;     
        }

        if(isCrouching == true)
        {
            controller.height = crouchHeight;
            walkSpeed *= crouchMultiplier;
        }
        else
        {
            controller.height = standingHeight;
        }

        if ((controller.collisionFlags & CollisionFlags.Above) != 0)
        {
            velocity.y = -2f;
        }
    }

    void Jump()
    {
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(JumpHeight * -2f * gravity);
        }
    }
    
}
