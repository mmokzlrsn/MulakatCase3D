using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour , ICanMove
{
    [Header("Speed Of The Ball")]
    [SerializeField] private float moveSpeed = 5f;
    private Rigidbody ballRB;


    private void Awake()
    {
        ballRB = GetComponent<Rigidbody>();
    }

    private void Start()
    { 
        Throw();
    }

    private void Throw()
    {
        ballRB.velocity = new Vector3(moveSpeed * PositiveOrNegative(),0, moveSpeed * PositiveOrNegative());
    }

    public int PositiveOrNegative()
    {
        return Random.Range(0, 2) == 0 ? -1 : 1;
    }

    public void SetSpeed(float newSpeed)
    {
        moveSpeed = newSpeed;
    }

    public float GetSpeed()
    {
        return moveSpeed;
    }





}
