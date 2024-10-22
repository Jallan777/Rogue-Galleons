using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisplayPlayerHealth : MonoBehaviour
{

    private float playerHealth;
    public Image healthBarFill;
    public Image healthBarRedFill;
    public float CurrentHealth { get; set; }
    public float maxHealth = 500;
    [SerializeField] Text resultText;

    public void Start()
    {
        playerHealth = PlayerPrefs.HasKey("PlayerHealth") ? PlayerPrefs.GetFloat("PlayerHealth") : 500;

        UpdateHealthUI();

        EventPopup eventPopup = FindObjectOfType<EventPopup>();

        if(eventPopup != null)
        {
            eventPopup.onEventTrigger.AddListener(ApplyDamage);
        }

        healthBarFill.fillAmount = playerHealth / 500;
        healthBarRedFill.fillAmount = playerHealth / 500;
        resultText.text = playerHealth.ToString();
        resultText.color = Color.black;



    }

    public void UpdateHealth(float newHealth)
    {
        playerHealth = Mathf.Clamp(newHealth, 0 , 500);
        PlayerPrefs.SetFloat("PlayerHealth", playerHealth);
        PlayerPrefs.Save();

        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        healthBarFill.fillAmount = playerHealth / 500;
        healthBarRedFill.fillAmount = playerHealth / 500;
        
        resultText.text = playerHealth.ToString();
        resultText.color = Color.black;

        Debug.Log("UPDATE HEALTH UI run with health: " + playerHealth);

        PlayerPrefs.SetFloat("PlayerHealth", playerHealth);
        PlayerPrefs.Save();
    }

    public void ApplyDamage(string eventDesc)
    {
        if(eventDesc.Contains("rogue  wave"))
        {
            UpdateHealth(playerHealth - 100);
        }

        if(eventDesc.Contains("shallow  reef"))
        {
            UpdateHealth(playerHealth - 50);
        }
    }

    public void ApplyHeal(float healAmount)
    {
        Debug.Log("Heal Amount: " + healAmount);
        UpdateHealth(playerHealth + healAmount);
        Debug.Log("New Health: " + playerHealth);

        
    }

    public void Update()
    {
        
        //healthBarFill.fillAmount = playerHealth / 500;
        //healthBarRedFill.fillAmount = playerHealth / 500;

        playerHealth = healthBarFill.fillAmount * 500;
        PlayerPrefs.SetFloat("PlayerHealth", playerHealth);
        PlayerPrefs.Save();
        resultText.text = playerHealth.ToString();

    }

    public float GetHealth()
    {
        return playerHealth;
    }

    public void UpdateHealthBar()
    {
        healthBarFill.fillAmount = CurrentHealth / maxHealth;
        healthBarRedFill.fillAmount = CurrentHealth / maxHealth;
        resultText.text = CurrentHealth.ToString();
    }

}
