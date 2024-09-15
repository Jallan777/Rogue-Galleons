using UnityEngine;

public class Bird_Movement : MonoBehaviour
{
    public float moveSpeed = 5f;            // Speed of the sprite movement
    public float respawnDelay = 3f;         // Time delay before respawning
    public Vector2 startXRange;             // Range for randomizing start position on the X-axis
    public Vector2 startYRange;             // Range for randomizing start position on the Y-axis
    public Vector2 endXRange;               // Range for randomizing end position on the X-axis
    public Vector2 endYRange;               // Range for randomizing end position on the Y-axis

    private Vector3 startPoint;              // Start position for the sprite
    private Vector3 endPoint;                // End position for the sprite

    private bool isMoving = true;           // Flag to control movement

    void Start()
    {
        SetRandomPoints();
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
        SetRandomPoints();

        // Move the sprite to the start point
        transform.position = startPoint;

        // Make the sprite visible again
        gameObject.SetActive(true);

        // Start moving the sprite toward the end point again
        isMoving = true;
    }

    void SetRandomPoints()
    {
        // Generate a random start position within the specified range
        startPoint = new Vector3(
            Random.Range(startXRange.x, startXRange.y), 
            Random.Range(startYRange.x, startYRange.y), 
            -0.7505112f); // 2D, so z remains 0

        // Generate a random end position within the specified range
        endPoint = new Vector3(
            Random.Range(endXRange.x, endXRange.y), 
            Random.Range(endYRange.x, endYRange.y), 
            -0.7505112f); // 2D, so z remains 0
    }

}
