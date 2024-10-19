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
    private float enemyHealth;
    [SerializeField] Text healthText;
    private int enteredHealth;

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
                // Disable the target GameObject if the fill amount is zero
                enemyShip.SetActive(false);
            }
            else
            {
                // Enable the target GameObject if the fill amount is greater than zero
                enemyShip.SetActive(true);
            }
        }


        enemyHealth = enemyHealthBarFill.fillAmount * enteredHealth;
        

        //enemyHealthBarFill.fillAmount = enemyHealth / enteredHealth;
        //enemyHealthBarRedFill.fillAmount = enemyHealth / enteredHealth;
        healthText.text = enemyHealth.ToString();
    }
}
