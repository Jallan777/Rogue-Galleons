using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class EventPopup : MonoBehaviour
{

    public GameObject targetObj;
    public Text eventText;

    public float delayTime = 0.02f;
    public TakeDamageEventSO takeDamageEvent;
    public StatusBar playerBar;
    public string[] texts;
    public UnityEvent<string> onEventTrigger;
    private float playerHealth;
    private bool isDamagingEvent;
    private string rogueWaveMessage = "Ye  spot  a  rogue  wave  on  the  horizon,  movin'  fast!  Brace  for  impact!";
    private string reefMessage = "The  ship  creaks  and  groans  as  ye  scrape  over  a  shallow  reef!";

    string randSentence;

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = PlayerPrefs.GetFloat("PlayerHealth");
        targetObj.SetActive(false);
        StartCoroutine(ActivateAfterDelay());

    }

    IEnumerator ActivateAfterDelay()
    {
        yield return new WaitForSeconds(delayTime);

        randSentence = texts[Random.Range(0, texts.Length)];
        eventText.text = randSentence;
        targetObj.SetActive(true);

        Debug.Log("Event Text: " + eventText.text);

        if (eventText.text == rogueWaveMessage)
        {
            Debug.Log("Rogue Wave found!!!!!");
            isDamagingEvent = true;
        }

        if (eventText.text == reefMessage)
        {
            Debug.Log("Reef Ahead!!!!!");
            isDamagingEvent = true;
        }
        damageEvent();

        isDamagingEvent = false;

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnEnable()
    {
        takeDamageEvent.shipTakingDamage += ChangeHealth;
    }


    private void OnDisable()
    {
        takeDamageEvent.shipTakingDamage -= ChangeHealth;
    }


    private void ChangeHealth(Ship ship)
    {

        playerBar.onHealthChange(ship.currentHealth / ship.maxHealth);



    }
    void damageEvent()
    {


        if (isDamagingEvent)
        {
            if (eventText.text == rogueWaveMessage)
            {
                playerHealth -= 100;
                Debug.Log("Rogue Wave Damage Taken! Player Health: " + playerHealth);
                eventText.text = "\n" + eventText.text + "\n\n\n<i>- 100 Health</i>";
            }

            if (eventText.text == reefMessage)
            {
                playerHealth -= 50;
                Debug.Log("Reef Damage Taken! Player Health: " + playerHealth);
                eventText.text = "\n" + eventText.text + "\n\n\n<i>- 50 Health</i>";
            }
        }

        if (playerHealth < 0)
        {
            playerHealth = 0;
        }



        PlayerPrefs.SetFloat("PlayerHealth", playerHealth);
        PlayerPrefs.Save();

        Debug.Log("Player Health: " + playerHealth);

        onEventTrigger.Invoke(randSentence);
    }
}
