using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class PlayerController : MonoBehaviour , ICanMove
{
    [Header("Move Speed of the Player ")]
    [SerializeField] private float moveSpeed = 7f;

    [Header("Player Play Area")]
    [SerializeField] private float xBound = 2f;
    [SerializeField] private float yBound = 4f;

    [Header("Jump Details")]
    [SerializeField] private float airTime = 1.5f;
    [SerializeField] private GameObject wings;
    [SerializeField] private float wingsCooldown = 0.5f;
    [SerializeField] private bool isGround;
    [SerializeField] private UnityEvent<bool> flying;



    private void Update()
    {
        KeyboardMovement(CalculateMovement());
        Fly();
        
    }

    public void Fly()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            flying.Invoke(true);
            isGround = false;
            wings.gameObject.SetActive(true);
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
        wings.gameObject.SetActive(false);
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

        if (transform.position.y >= yBound)
            transform.position = new Vector3(transform.position.x, yBound, transform.position.z);
        if (transform.position.y <= -yBound)
            transform.position = new Vector3(transform.position.x, -yBound, transform.position.z);



    }
   

    private Vector3 CalculateMovement()
    {
        float moveX = 0f;
        float moveY = 0f;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            moveY = +1f;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            moveY = -1f;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            moveX = +1f;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            moveX = -1f;
        }

        return new Vector3(moveX, moveY).normalized;
    }

    public void SetSpeed(float newSpeed)
    {
        moveSpeed = newSpeed;
    }

    public float GetSpeed()
    {
        return moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        
        if (collider.gameObject.CompareTag("Point"))
        {
            GameManager.Instance.UpdateScore();
            Destroy(collider.gameObject);
            Debug.Log("Collided");

        }
    }

}
