using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerMovement : MonoBehaviour
{


    private CharacterController controller;
    private Animator anim; 

    [Header("Player Movement Variables")] // this is used to make it more organised in the insepctor
    
    [SerializeField] private float regularSpeedMoveSpeed;
    [SerializeField] private float sprintingMoveSpeed;
    [SerializeField] private float crounchMultiplier;
    [SerializeField] private float JumpHeight;
    [SerializeField] private float gravity;
    [SerializeField] private float forceMagnitude;
    [SerializeField] private float groundDistance;

    private float regularSpeed;
    [SerializeField] private float crouchingHeight;
    [SerializeField] private float standingHeight;

    private bool isGrounded ,isCrouching, isSprinting;

    [Space(20)]
    [Header("Player Movement References")]

    public Transform GroundCheck;
    public LayerMask groundmask; 

    public StaminaBar staminaBar;
    private Vector3 velocity;

    public GameObject standingSprite;
    public GameObject crouchSprite;
    public GameObject walkingSprite;
    public GameObject runningSprite;

    

    
    // Start is called before the first frame update
    void Start()
    {
        GetReferences();
        standingHeight = controller.height;
        regularSpeed = regularSpeedMoveSpeed;
  
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
            regularSpeedMoveSpeed *= crounchMultiplier;

        }
        else
        {

            controller.height = standingHeight;
        }

        if(isSprinting == true)
        {
            regularSpeedMoveSpeed = sprintingMoveSpeed;
            staminaBar.UseStamina(15);
        }
        else
        {
            regularSpeedMoveSpeed = regularSpeed;
        }
        controller.Move(move * regularSpeedMoveSpeed * Time.deltaTime);

        if(Input.GetKeyDown(KeyCode.Space))
        {
            staminaBar.UseStamina(15);
        }



    }



    void Jump()
    {
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(JumpHeight * -2f * gravity);
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody rigidbody = hit.collider.attachedRigidbody;

        if(rigidbody != null)
        {
            Vector3 forceDirection = hit.gameObject.transform.position - transform.position;
            forceDirection.y = 0;
            forceDirection.Normalize();

            rigidbody.AddForceAtPosition(forceDirection* forceMagnitude, transform.position, ForceMode.Impulse);
        }
    }





    void GetReferences(){
        anim = GetComponentInChildren<Animator>();
        controller = GetComponent<CharacterController>();
    }
    
}
