using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualScore : MonoBehaviour, ICanMove
{
    [Header("Coin Move Speed")]
    [SerializeField] private float moveSpeed;

    [Header("Position of The Score")]
    [SerializeField] private Transform scoreText;


    private Vector3 startPosition;
    private float desiredDuration = 1f;
    private float elapsedTime;

    private void Update()
    {
        MoveTowardsScoreText();
    }

    public void MoveTowardsScoreText()
    {
        startPosition = transform.parent.position;
        gameObject.SetActive(true);
        elapsedTime += Time.deltaTime;
        float percentageComplete = elapsedTime / desiredDuration;

        transform.position = Vector3.Lerp(startPosition, scoreText.position, percentageComplete);

        if(transform.position == scoreText.transform.position)
        {
            elapsedTime = 0;
            gameObject.SetActive(false);
            transform.position = transform.parent.position;
        }
    }

    public float GetSpeed()
    {
        return moveSpeed;
    }
     

    public void SetSpeed(float newSpeed)
    {
        moveSpeed = newSpeed;
    }
}
