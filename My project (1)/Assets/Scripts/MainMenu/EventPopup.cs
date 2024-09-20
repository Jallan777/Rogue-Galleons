using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventPopup : MonoBehaviour
{

    public GameObject targetObj;
    public Text eventText;
    public float delayTime = 0.02f;

    public string[] texts = {
         
        "Nothing but blue stretches out before you, below you, and above you. \n\n You're beginning to feel very very small...", 
        "You could have sworn something just bumped the ship...", 
        "This looks like a nice spot for some fishing...", 
        "Theres a foul odour on the winds, bringing misfortune to those who may wander...", 
        "Oh look! Flying Fish!!", 
        "If only you could have afforded the boat with beds in it!"
    };

    // Start is called before the first frame update
    void Start()
    {
        targetObj.SetActive(false);
        StartCoroutine(ActivateAfterDelay());
    }

    IEnumerator ActivateAfterDelay()
    {
        yield return new WaitForSeconds(delayTime);

        string randSentence = texts[Random.Range(0, texts.Length)];
        eventText.text = randSentence;

        targetObj.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
