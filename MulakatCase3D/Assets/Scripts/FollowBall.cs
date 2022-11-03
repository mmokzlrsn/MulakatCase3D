using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowBall : MonoBehaviour
{
    [Header("Target Object")]
    [SerializeField] private GameObject ball;

    // Update is called once per frame
    void Update()
    {
        MoveTowardsBall();
    }

    public void MoveTowardsBall()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, ball.transform.position.z); //GameObject will only follow the target on Y-axis
    }
}
