using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LandingPopup : MonoBehaviour
{
    public GameObject targetObj;
    public Text eventText;
    public Button contButton;
    public float delayTime = 0.02f;
    public float betweenTime = 0.01f;
    private string playerName;
    private int currentTextIndex = 0;
    private string[] introSentences;



    // Start is called before the first frame update
    void Start()
    {

        playerName = PlayerPrefs.GetString("PlayerName");

        if(playerName == null)
        {
            playerName = "<b>Player 1</b>";
        }
        else


        introSentences = new string[] {
            "Welcome to Rogue Galleons!\n\nYou are Captain " + playerName.ToUpper() + " of \"The Eagle\", and you've just set sail on a voyage towards fame and riches. \n\nClick continue to move on",
            "\nYou set out to begin your turmultuous journey, shrouded by the cover of darkness. \n\n Guided by moonlight, any way is forward on the high seas...\n\nCan you make it to where X marks the spot..?", 
            "\nTREASURE ISLAND!!\n\nThe dream of every Crook, Pirate and Scallywag, to find the mysterious Treasure Island, has fallen into your hands.\n\nTake to the seas, and make history!",
            "- Open Map to travel to new locations.\n- Fight or Flee from enemies\n- Press ESC key to pause game.\n- Volume controls in Settings Menu."
        };



        targetObj.SetActive(false);
        contButton.onClick.AddListener(OnContinueClicked);
        StartCoroutine(ActivateAfterDelay());
    }

    IEnumerator ActivateAfterDelay()
    {
        yield return new WaitForSeconds(delayTime);

        targetObj.SetActive(true);
        eventText.text = introSentences[currentTextIndex];
    }

    void OnContinueClicked()
    {
        currentTextIndex++;

        if(currentTextIndex < introSentences.Length)
        {
            StartCoroutine(ChangeText());
        }
        else
        {
            targetObj.SetActive(false);
        }
    }

    IEnumerator ChangeText()
    {
        yield return new WaitForSeconds(betweenTime);
        eventText.text = introSentences[currentTextIndex];
    }

    // Update is called once per frame
    void Update()
    {
        playerName = PlayerPrefs.GetString("PlayerName");
    }
}
