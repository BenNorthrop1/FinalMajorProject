using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{


    private CharacterController controller;
    private Animator anim; 

    [Header("Player Movement Variables")]
    
    [SerializeField] private float walkSpeed = 20f;
    [SerializeField] private float JumpHeight = 3f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private float sprintingMultiplier;
    [SerializeField] private float crounchMultiplier;
    [SerializeField] private float crouchingHeight;
    [SerializeField] private float standingHeight;
    [SerializeField] private float maxStamina;
    private float currentStamina;


   
    private float regular = 0f;
    private bool isGrounded ,isCrouching, isSprinting;

    [Space(40)]
    [Header("Player Movement References")]

    public Transform GroundCheck;
    public LayerMask groundmask; 
    private Vector3 velocity;

    
    // Start is called before the first frame update
    void Start()
    {
        GetReferences();
        standingHeight = controller.height;
        regular = walkSpeed;
        currentStamina = maxStamina;
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
        Jump();

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

    }

    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        if(Input.GetKey(KeyCode.LeftShift))
        {
            isSprinting = true;
        }
        else
        {
            isSprinting = false;
        }

        if(Input.GetKey(KeyCode.LeftControl))
        {
            isCrouching = true;
        }
        else
        {
            isCrouching = false;
        }


        Vector3 move = Vector3.Normalize(transform.right * x + transform.forward * z);

        if(isCrouching == true)
        {
            controller.height = crouchingHeight;
            walkSpeed *= crounchMultiplier;
        }
        else
        {
            controller.height = standingHeight;
        }

        if(isSprinting == true)
        {
            walkSpeed = sprintingMultiplier;
        }
        else
        {
            walkSpeed = regular;
        }

        if(move == Vector3.zero)
        {
            anim.SetFloat("Speed", 0, 0.2f , Time.deltaTime);
        }
        else if (move != Vector3.zero && !Input.GetKey(KeyCode.LeftShift))
        {
            anim.SetFloat("Speed", 0.5f, 0.2f , Time.deltaTime);
        }
        else if (move != Vector3.zero && Input.GetKey(KeyCode.LeftShift))
        {
            anim.SetFloat("Speed", 1f, 0.2f , Time.deltaTime);
        }

        controller.Move(move * walkSpeed * Time.deltaTime);
    }


   

    void Jump()
    {
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(JumpHeight * -2f * gravity);
        }
    }

    void GetReferences(){
        anim = GetComponentInChildren<Animator>();
        controller = GetComponent<CharacterController>();
    }
    
}
