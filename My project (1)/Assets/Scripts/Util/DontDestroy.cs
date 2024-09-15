using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    private static DontDestroy instance;

    // Start is called before the first frame update
    void Start()
    {
        if(instance == null) {
            instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }

    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
