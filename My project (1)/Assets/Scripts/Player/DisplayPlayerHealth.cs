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

        healthBarFill.fillAmount = playerHealth / 500;
        healthBarRedFill.fillAmount = playerHealth / 500;
        resultText.text = playerHealth.ToString();
        resultText.color = Color.black;



    }

    public void Update()
    {
        playerHealth = healthBarFill.fillAmount * 500;
        PlayerPrefs.SetFloat("PlayerHealth", playerHealth);
        PlayerPrefs.Save();
        resultText.text = playerHealth.ToString();

    }

    public void UpdateHealthBar()
    {
        healthBarFill.fillAmount = CurrentHealth / maxHealth;
        healthBarRedFill.fillAmount = CurrentHealth / maxHealth;
        resultText.text = CurrentHealth.ToString();
    }

}
