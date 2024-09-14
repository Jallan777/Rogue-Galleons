using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class REPEATFG1 : MonoBehaviour
{
    public float speed;  // Speed of the layer
    private Vector3 startPos;  // Initial position of the layer
    private float repeatWidth; // Width after which the layer repeats

    void Start()
    {
        // Save the initial position of the layer
        startPos = transform.position;
        // Calculate half the width of the background based on the BoxCollider2D size
        repeatWidth = GetComponent<BoxCollider2D>().size.x - 155;
    }

    void Update()
    {
        // Move the layer to the left
        transform.Translate(Vector3.left * speed * Time.deltaTime);

        // Reset the position once the layer has moved beyond the screen
        if (transform.position.x < startPos.x - repeatWidth)
        {
            // Move the layer back to the starting position to repeat the background
            transform.position = new Vector3(startPos.x, transform.position.y, transform.position.z);
        }
    }
}
