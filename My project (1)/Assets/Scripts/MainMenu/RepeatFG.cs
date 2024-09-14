using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatFG : MonoBehaviour
{
    Vector3 startPos;
    float repeatWidth;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        repeatWidth = GetComponent<BoxCollider2D>().size.x * 120;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < startPos.x - repeatWidth)
        {
            transform.position = startPos;

        }
    }
}
