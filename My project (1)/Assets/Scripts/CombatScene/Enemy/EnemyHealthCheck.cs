using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthCheck : MonoBehaviour
{
    public Image fillImage;         // Reference to the UI Slider
    public GameObject targetObj;  // The GameObject to disable when the slider is zero
    

    void Update()
    {
        // Check if the slider's value is zero
        if (fillImage != null && targetObj != null)
        {
            if (fillImage.fillAmount <= 0)
            {
                // Disable the target GameObject if the fill amount is zero
                targetObj.SetActive(false);
            }
            else
            {
                // Enable the target GameObject if the fill amount is greater than zero
                targetObj.SetActive(true);
            }
        }
    }
}
