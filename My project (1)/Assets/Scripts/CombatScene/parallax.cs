using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    // Material of the object
    private Material mat;
    // Distance for the parallax movement
    private float dis;

    [Range(0f, 0.5f)]
    public float speed = 0.3f;

    void Start()
    {
        // Get the material attached to the renderer of the object
        mat = GetComponent<Renderer>().material;
    }

    void Update()
    {
        // Increment the distance by the speed value
        dis += Time.deltaTime * speed;
        // Apply the offset to the texture's main texture property
        mat.SetTextureOffset("_MainTex", Vector2.right * dis);
    }
}
