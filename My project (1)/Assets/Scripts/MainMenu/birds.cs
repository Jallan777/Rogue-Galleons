using UnityEngine;

public class BirdMovement : MonoBehaviour
{
    public float moveSpeed = 5f;        // Speed of the bird
    public float respawnDelay = 2f;     // Delay before respawn
    public Vector2 startPosition;       // Starting position off-screen
    public Vector2 endPosition;         // Ending position off-screen

    private bool isRespawning = false;  // To prevent multiple coroutines from running

    void Start()
    {
        // Set the initial position to the start position
        transform.position = startPosition;
    }

    void Update()
    {
        // If the bird is not respawning, keep moving it
        if (!isRespawning)
        {
            // Move the bird towards the end position
            transform.position = Vector2.MoveTowards(transform.position, endPosition, moveSpeed * Time.deltaTime);

            // Check if the bird has reached the end position (flew off-screen)
            if (Vector2.Distance(transform.position, endPosition) < 0.1f)
            {
                // Start the respawn process
                StartCoroutine(Respawn());
            }
        }
    }

    // Coroutine to respawn the bird after a delay
    System.Collections.IEnumerator Respawn()
    {
        Debug.Log("Bird reached end position, starting respawn...");

        isRespawning = true; // Prevent movement while respawning

        // Deactivate the bird so it temporarily disappears
        gameObject.SetActive(false);

        // Wait for the specified delay before respawning
        yield return new WaitForSeconds(respawnDelay);

        // Reset the bird's position back to the starting point
        transform.position = startPosition;

        Debug.Log("Respawning bird at start position...");

        // Reactivate the bird and make it fly again
        gameObject.SetActive(true);

        // Set isRespawning to false so the bird can move again
        isRespawning = false;
    }
}
