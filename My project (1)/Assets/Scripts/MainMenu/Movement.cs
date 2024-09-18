using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 5f;            // Speed of the sprite movement
    public float respawnDelay = 3f;         // Time delay before respawning
    public Vector3 startPoint;              // Start position for the sprite
    public Vector3 endPoint;                // End position for the sprite

    private bool isMoving = true;           // Flag to control movement

    void Start()
    {
        // Set the sprite at the start position when the game begins
        transform.position = startPoint;
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
        // Move the sprite towards the end point
        transform.position = Vector3.MoveTowards(transform.position, endPoint, moveSpeed * Time.deltaTime);

        // Check if the sprite has reached the end point
        if (Vector3.Distance(transform.position, endPoint) < 0.01f)
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
        // Move the sprite to the start point
        transform.position = startPoint;

        // Make the sprite visible again
        gameObject.SetActive(true);

        // Start moving the sprite toward the end point again
        isMoving = true;
    }
}