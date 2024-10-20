using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobbingMovement : MonoBehaviour
{
    public float moveSpeed = 5f;            // Speed of the sprite movement
    public float respawnDelay = 3f;         // Time delay before respawning
    public float bobbingAmplitude = 0.5f;
    public float bobbingFrequency = 1.0f;

    public Vector3 startPoint;              // Start position for the sprite
    private Vector3 startPointVariance = new Vector3(110, 3, 0);            // Variance range for start position (x, y, z)
    public Vector3 endPoint;                // End position for the sprite
    private Vector3 endPointVariance = new Vector3(10, 5, 0);              // Variance range for end position (x, y, z)

    private float time;
    private bool isMoving = true;           // Flag to control movement
    private float originalY;

    void Start()
    {
        moveSpeed = Mathf.RoundToInt(Random.Range(moveSpeed - 20, moveSpeed + 15));
        respawnDelay = Mathf.RoundToInt(Random.Range(respawnDelay - 30, respawnDelay + 15));

        bobbingAmplitude = Random.Range(bobbingAmplitude - 0.2f, bobbingAmplitude + 0.3f);
        bobbingFrequency = Random.Range(bobbingFrequency - 0.4f, bobbingFrequency + 3.75f);
        
        startPoint = new Vector3(
            startPoint.x + Mathf.RoundToInt(Random.Range(-startPointVariance.x, startPointVariance.x)),
            startPoint.y + Mathf.RoundToInt(Random.Range(-startPointVariance.y, startPointVariance.y)),
            startPoint.z
        );

        endPoint = new Vector3(
            endPoint.x + Mathf.RoundToInt(Random.Range(-endPointVariance.x, endPointVariance.x)),
            endPoint.y + Mathf.RoundToInt(Random.Range(-endPointVariance.y, endPointVariance.y)),
            endPoint.z
        );

        if(moveSpeed < 10)
        {
            moveSpeed += 15; 

            Debug.Log("moveSpeed updated from " + (moveSpeed - 15));

            if(moveSpeed < 0)
            {
                moveSpeed = 10;
                Debug.Log("moveSpeed updated from below 0");

            }
        }
        if(respawnDelay < 5)
        {
            respawnDelay += 20; 
        }
        if(bobbingAmplitude < 0f)
        {
            bobbingAmplitude = 0.1f; 
        }
        if(bobbingFrequency < 0f)
        {
            bobbingFrequency = 0.5f; 
        }
        // Set the sprite at the start position when the game begins
        transform.position = startPoint;

        originalY = transform.position.y;
    }

    void Update()
    {
        if (isMoving)
        {
            MoveTowardsEndPoint();
        }
    }

    // Function to move the sprite from the start to the end point
    void MoveTowardsEndPoint()
    {
        time += Time.deltaTime;

        float bobbingY = Mathf.Sin(time * bobbingFrequency) * bobbingAmplitude;
        // Move the sprite towards the end point
        Vector3 newPosition = Vector3.MoveTowards(transform.position, endPoint, moveSpeed * Time.deltaTime);
        newPosition.y = originalY + bobbingY;        

        transform.position = newPosition;

        // Check if the sprite has reached the end point
        if (Mathf.Abs(transform.position.x - endPoint.x) < 0.02f)
        {
            // Stop the movement
            isMoving = false;

            // Make the sprite disappear
            gameObject.SetActive(false);

            // Call the respawn function after a delay
            Invoke("RespawnSprite", respawnDelay);
        }
    }

    // Function to respawn the sprite at the start point
    void RespawnSprite()
    {
        moveSpeed = Mathf.RoundToInt(Random.Range(moveSpeed - 20, moveSpeed + 15));
        respawnDelay = Mathf.RoundToInt(Random.Range(respawnDelay - 10, respawnDelay + 25));

        bobbingAmplitude = Random.Range(bobbingAmplitude - 0.2f, bobbingAmplitude + 0.3f);
        bobbingFrequency = Random.Range(bobbingFrequency - 0.4f, bobbingFrequency + 1.5f);
        
        startPoint = new Vector3(
            startPoint.x + Mathf.RoundToInt(Random.Range(-startPointVariance.x, startPointVariance.x)),
            startPoint.y + Mathf.RoundToInt(Random.Range(-startPointVariance.y, startPointVariance.y)),
            startPoint.z
        );

        endPoint = new Vector3(
            endPoint.x + Mathf.RoundToInt(Random.Range(-endPointVariance.x, endPointVariance.x)),
            endPoint.y + Mathf.RoundToInt(Random.Range(-endPointVariance.y, endPointVariance.y)),
            endPoint.z
        );


        // Move the sprite to the start point
        transform.position = startPoint;

        originalY = transform.position.y;

        // Make the sprite visible again
        gameObject.SetActive(true);

        // Start moving the sprite toward the end point again
        isMoving = true;

        time = 0;
    }
}