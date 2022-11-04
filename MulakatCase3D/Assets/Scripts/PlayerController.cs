using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class PlayerController : MonoBehaviour , ICanMove
{
    private Rigidbody playerRigidbody;
    
    [Header("Animations")]
    [SerializeField] Animator playerAnimator;
    [SerializeField] int isWalkingHash;
    [SerializeField] int isFlyingHash;

    [Header("Move Speed of the Player ")]
    [SerializeField] private float moveSpeed = 7f;

    [Header("Player Play Area")]
    [SerializeField] private float xBound = 2f;
    [SerializeField] private float zBound = 4f;

    [Header("Jump Details")]

    [SerializeField] private float airTime = 1f;
    [SerializeField] private float jumpForce = 8f;
    [SerializeField] private GameObject wings;
    [SerializeField] private float wingsCooldown = 0.25f;
    [SerializeField] private bool isGround = true;
    [SerializeField] private UnityEvent<bool> flying;

    private void Awake()
    {
        playerAnimator = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
    }


    private void Update()
    {
        KeyboardMovement(CalculateMovement());
        Fly();
        
    }

    public void Fly()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            playerAnimator.SetBool("isFlying", true);
            playerRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGround = false;
            //wings.gameObject.SetActive(true);
            StartCoroutine(AirTimeCountdownRoutine());
        }
    }

    public void WingsDisplay()
    {
        wings.transform.position = new Vector3 (transform.position.x, transform.position.y + 0.5f, transform.position.z);
    }

    IEnumerator AirTimeCountdownRoutine()
    {
        //WingsDisplay();
        yield return new WaitForSeconds(airTime);
        playerAnimator.SetBool("isFlying", false);
        //wings.gameObject.SetActive(false);
        isGround = false;
        flying.Invoke(false);
        yield return new WaitForSeconds(wingsCooldown);
        isGround = true;
        
    }

    private void KeyboardMovement(Vector3 moveDir)
    {
        

        transform.position += moveDir * moveSpeed * Time.deltaTime;
        PlayerPosition();
    }

    private void PlayerPosition()
    {
        if (transform.position.x >= xBound)
            transform.position = new Vector3(xBound, transform.position.y, transform.position.z);
        if (transform.position.x <= -xBound)
            transform.position = new Vector3(-xBound, transform.position.y, transform.position.z);

        if (transform.position.z >= zBound)
            transform.position = new Vector3(transform.position.x, transform.position.y, zBound);
        if (transform.position.z <= -zBound)
            transform.position = new Vector3(transform.position.x, transform.position.y, -zBound);



    }
   

    private Vector3 CalculateMovement()
    {
        float moveX = 0f;
        float moveZ = 0f;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            playerAnimator.SetBool("isWalking", true);
            moveZ = +1f;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            playerAnimator.SetBool("isWalking", true);
            moveZ = -1f;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            playerAnimator.SetBool("isWalking", true);
            moveX = +1f;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            playerAnimator.SetBool("isWalking", true);
            moveX = -1f;
        }
        if(moveX  == 0 && moveZ == 0)
        {
            playerAnimator.SetBool("isWalking", false);
        }

        return new Vector3(moveX, 0, moveZ).normalized;
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
