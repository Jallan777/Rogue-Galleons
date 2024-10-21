using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float horizontal;
    public float speed = 3f;
    public float jumpingPower = 15f;
    public bool isFacingRight = true;

    [SerializeField] public Rigidbody2D rb;
    [SerializeField] public Transform groundCheck;
    [SerializeField] public LayerMask groundLayer;

    public bool isMovingRight = false;
    public bool isMovingLeft = false;

    void Update()
    {

        // Detect left mouse click for moving right
        if (Input.GetMouseButtonDown(1)) // Left click
        {
            isMovingRight = true;
            isMovingLeft = false;
        }

        // Detect right mouse click for moving left
        if (Input.GetMouseButtonDown(0)) // Right click
        {
            isMovingLeft = true;
            isMovingRight = false;
        }

        // Stop movement on mouse button release
        if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
        {
            isMovingRight = false;
            isMovingLeft = false;
        }

        // Handle keyboard input (A/D or left/right arrow keys)
        horizontal = Input.GetAxisRaw("Horizontal");

        // Jump when grounded and spacebar or mouse click is pressed
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        // Stop upward movement if jump is released early
        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        Flip();
    }

    void FixedUpdate()
    {
        // Determine horizontal movement based on mouse and keyboard input
        if (isMovingRight)
        {
            horizontal = 1f;  // Move right (mouse)
        }
        else if (isMovingLeft)
        {
            horizontal = -1f; // Move left (mouse)
        }

        // Apply movement
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f; // Flip the character horizontally
            transform.localScale = localScale;
        }
    }
}