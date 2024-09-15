using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipInWater : MonoBehaviour
{
    // Start is called before the first frame update
    public float bobbingAmplitude = 0.5f;  // How high the bobbing goes
    public float bobbingFrequency = 1.0f;  // How fast the bobbing happens
    public float rockingAmplitude = 0.1f;  // How much the ship rocks side to side
    public float rockingFrequency = 0.5f;  // How fast the rocking happens
    private Vector3 startPosition;
    private float time;

    void Start()
    {
        // Save the initial position of the ship
        startPosition = transform.position;
    }

void Update()
    {
        // Calculate the new Y position for bobbing
        time += Time.deltaTime;
        float bobbingY = Mathf.Sin(time * bobbingFrequency) * bobbingAmplitude;

        // Calculate the new rotation for rocking
        float rockingZ = Mathf.Sin(time * rockingFrequency) * rockingAmplitude;

        // Apply the new position and rotation
        transform.position = new Vector3(startPosition.x, startPosition.y + bobbingY, startPosition.z);
        transform.rotation = Quaternion.Euler(0, 0, rockingZ);
    }
}
