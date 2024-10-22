using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayerMovementTests
{
    private GameObject player;
    private PlayerMovement playerMovement;
    private Rigidbody2D rb;

    [SetUp]
    public void Setup()
    {
        // Create a new GameObject for the player and add the PlayerMovement and Rigidbody2D components
        player = new GameObject();
        playerMovement = player.AddComponent<PlayerMovement>();
        rb = player.AddComponent<Rigidbody2D>();

        // Setup playerMovement dependencies (such as ground check and layer mask)
        GameObject groundCheckObject = new GameObject();
        playerMovement.groundCheck = groundCheckObject.transform;
        playerMovement.groundLayer = LayerMask.GetMask("Ground");

        playerMovement.rb = rb;

        playerMovement.speed = 70f;
    }

    [TearDown]
    public void Teardown()
    {
        // Clean up after each test
        Object.Destroy(player);
    }

    [UnityTest]
    public IEnumerator PlayerMovesRightWhenMouseClicked()
    {
        // Simulate a right-click (Input.GetMouseButtonDown(1))
        playerMovement.isMovingRight = true;

        // Wait for a frame
        yield return null;

        // Check that the player is moving right (velocity should be positive on the X-axis)
        Assert.Greater(rb.velocity.x, 0f);
    }

    [UnityTest]
    public IEnumerator PlayerMovesLeftWhenMouseClicked()
    {
        // Simulate a left-click (Input.GetMouseButtonDown(0))
        playerMovement.isMovingLeft = true;

        // Wait for a frame
        yield return null;

        // Check that the player is moving left (velocity should be negative on the X-axis)
        Assert.Less(rb.velocity.x, 0f);
    }



}



