using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisplayPlayerHealth : MonoBehaviour
{

    private float playerHealth;
    [SerializeField] Text resultText;


    public void Start()
    {
        playerHealth = PlayerPrefs.GetFloat("PlayerHealth", 500);

     
            resultText.text =  playerHealth.ToString();   
            resultText.color = Color.black;

        

    }

    public void Update()
    {
        playerHealth = PlayerPrefs.GetFloat("PlayerHealth", 500);
        resultText.text = playerHealth.ToString();

    }
}
