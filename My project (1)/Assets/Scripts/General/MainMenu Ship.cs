using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shipMovement : MonoBehaviour
{
    public float bobbingAmplitude = 0.5f;  // How high the bobbing goes
    public float bobbingFrequency = 1.0f;  // How fast the bobbing happens
    public float rockingAmplitude = 0.1f;  // How much the ship rocks side to side
    public float rockingFrequency = 0.5f;  // How fast the rocking happens
    public Vector3 endPosition;            // Destination position for the ship
    public float moveSpeed = 1.0f;         // Speed of the ship's movement

    private Vector3 startPosition;         // Starting position of the ship
    private float time;                    // Time counter for animations
    private float journeyLength;           // Total distance between start and end positions
    private float startTime;               // Time when the movement started

    void Start()
    {
        // Save the initial position of the ship and calculate the journey length
        startPosition = transform.position;
        journeyLength = Vector3.Distance(startPosition, endPosition);
        startTime = Time.time;
    }

    void Update()
    {
        // Calculate the distance moved so far
        float distCovered = (Time.time - startTime) * moveSpeed;

        // Calculate the fraction of the journey completed (0 to 1)
        float fractionOfJourney = distCovered / journeyLength;

        // Interpolate between the start and end positions
        Vector3 currentPos = Vector3.Lerp(startPosition, endPosition, fractionOfJourney);

        // Calculate the bobbing and rocking effects
        time += Time.deltaTime;
        float bobbingY = Mathf.Sin(time * bobbingFrequency) * bobbingAmplitude;
        float rockingZ = Mathf.Sin(time * rockingFrequency) * rockingAmplitude;

        // Apply the new position with bobbing and the rotation with rocking
        transform.position = new Vector3(currentPos.x, currentPos.y + bobbingY, currentPos.z);
        transform.rotation = Quaternion.Euler(0, 0, rockingZ);
    }
}
