using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LandingPopup : MonoBehaviour
{
    public GameObject targetObj;
    public Text eventText;
    public float delayTime = 0.02f;



    // Start is called before the first frame update
    void Start()
    {
        eventText.text = "You set out to begin your turmultuous journey, shrouded by the cover of darkness. \n\n Guided by moonlight, any way is forward on the high seas...";
        targetObj.SetActive(false);
        StartCoroutine(ActivateAfterDelay());
    }

    IEnumerator ActivateAfterDelay()
    {
        yield return new WaitForSeconds(delayTime);

        targetObj.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
