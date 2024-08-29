using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RG_Logo_Float : MonoBehaviour
{

    public float floatSpeed = 0.5f;
    public float scaleAmount = 0.1f;
    public float rotationSpeed = 2f;
    public float rotationAmount = 10f;
    private Vector3 initScale;
    private Vector3 initRotation;
    // Start is called before the first frame update
    void Start()
    {
        
        initScale = transform.localScale;
        initRotation = transform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        float scaleOffset = Mathf.Sin(Time.time * floatSpeed) * scaleAmount;
        transform.localScale = initScale + new Vector3(scaleOffset, scaleOffset, 0);

        float rotationOffset = Mathf.Sin(Time.time * rotationSpeed) * rotationAmount;

        transform.eulerAngles = initRotation + new Vector3(0, 0, rotationOffset);
    }
}
