using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowBall : MonoBehaviour
{
    [Header("Target Object")]
    [SerializeField] private GameObject ball;


    [SerializeField] private NavMeshAgent agent; //it will getcomponent on awake
    [SerializeField] private LayerMask whatIsGround, whatIsBall;

    [Header("Patrolling")]
    [SerializeField] private Vector3 walkPoint;
    [SerializeField] private bool walkPointSet;
    [SerializeField] private float walkPointRange;

    [Header("States")]
    [SerializeField] private float sightRange;
    [SerializeField] private bool playerInSightRange;


    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }


    void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsBall);
     

        if (!playerInSightRange) Patroling();
        if (playerInSightRange) ChaseBall();
        
    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
        {
            agent.SetDestination(new Vector3(transform.position.x, transform.position.y, walkPoint.z));
        }
        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;

    }

    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }


    private void ChaseBall()
    {
        agent.SetDestination(new Vector3(transform.position.x, transform.position.y ,ball.transform.position.z));
    }

}
