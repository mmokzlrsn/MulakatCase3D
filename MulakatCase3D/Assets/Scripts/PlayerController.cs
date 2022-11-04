using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class PlayerController : MonoBehaviour , ICanMove
{
    private Rigidbody playerRigidbody;
    
    [Header("Animations")]
    [SerializeField] Animator playerAnimator;


    [Header("Movement of the Player ")]
    [SerializeField] private CharacterController controller;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float turnSmoothTime = 0.1f;
    [SerializeField] private float turnSmoothVelocity;
    [SerializeField] private Vector3 velocity;

    

    [Header("Jump Details")] 
    [SerializeField] private float jumpForce = 3f;
    [SerializeField] private bool isGround = true;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundMask;




    private void Awake()
    {
        playerAnimator = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
    }


    private void Update()
    {
         
        Movement();
         
    }

    public void Movement()
    {
        //jump

        isGround = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGround && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (Input.GetButtonDown("Jump") && isGround)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2 * gravity);
        }

        //gravity formula

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        //walk

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * moveSpeed * Time.deltaTime);
        }
            
    }

     

    public void SetSpeed(float newSpeed)
    {
        moveSpeed = newSpeed;
    }

    public float GetSpeed()
    {
        return moveSpeed;
    }
 
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Point"))
        {
            GameManager.Instance.UpdateScore();
            Debug.Log("Collided");

        }
    }

}
