using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyHealth : MonoBehaviour
{
    public Image enemyHealthBarFill;     // Reference to the UI Slider
    public Image enemyHealthBarRedFill;
    public GameObject enemyShip;  // The GameObject to disable when the slider is zero
    public float fallSpeed = 0.2f;
    public float fallDistance = 10.0f;
    private float enemyHealth;
    [SerializeField] Text healthText;
    private int enteredHealth;

    public AudioSource audioSource;
    public AudioClip deathSound;
    private bool hasPlayedDeathSound = false;

    void Start()
    { 
        enteredHealth = int.Parse(healthText.text);
        enemyHealth = enteredHealth;

        enemyHealthBarFill.fillAmount = enemyHealth / enteredHealth;
        enemyHealthBarRedFill.fillAmount = enemyHealth / enteredHealth;
        healthText.text = enemyHealth.ToString();
        healthText.color = Color.black;
    }
    void Update()
    {
        // Check if the slider's value is zero
        if (enemyHealthBarFill != null && enemyShip != null)
        {
            if (enemyHealthBarFill.fillAmount <= 0)
            {
                if(!hasPlayedDeathSound)
                {
                    audioSource.PlayOneShot(deathSound);
                    hasPlayedDeathSound = true;
                }
                // Disable the target GameObject if the fill amount is zero
                //enemyShip.SetActive(false);
                if(enemyShip.transform.position.y < -10 || enemyShip.transform.position.x < -11)
                {
                    Vector3 finalPos = enemyShip.transform.position;
                    finalPos.y = -25f;
                    finalPos.x = -11f;
                    enemyShip.transform.position = finalPos;
                }
                else
                {
                   StartCoroutine(MoveAndDisable(enemyShip));
                }
            }
            else
            {
                // Enable the target GameObject if the fill amount is greater than zero
                enemyShip.SetActive(true);
                hasPlayedDeathSound = false;
            }
        }


        enemyHealth = enemyHealthBarFill.fillAmount * enteredHealth;
        

        //enemyHealthBarFill.fillAmount = enemyHealth / enteredHealth;
        //enemyHealthBarRedFill.fillAmount = enemyHealth / enteredHealth;
        healthText.text = enemyHealth.ToString();
    }

    IEnumerator MoveAndDisable(GameObject obj)
    {
        Vector3 startPos = obj.transform.position;
        Vector3 endPos = new Vector3(startPos.x, startPos.y - fallDistance, startPos.z);

        float elapsedTime = 0f;
        float maxTilt = 25f;
        float tiltSpeed = 50f;

        

        while(Vector3.Distance(obj.transform.position, endPos) > 0.01f)
        {
            obj.transform.position = Vector3.MoveTowards(obj.transform.position, endPos, (fallSpeed / 2) * Time.deltaTime);
            
            float tiltAngle = Mathf.Sin(elapsedTime * Mathf.PI) * maxTilt;

            obj.transform.rotation = Quaternion.Euler(0f, 0f, tiltAngle);

            elapsedTime += Time.deltaTime * fallSpeed;
            
            yield return null;
        }

        obj.transform.position = endPos;
        obj.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        
        obj.SetActive(false);
    }
}
