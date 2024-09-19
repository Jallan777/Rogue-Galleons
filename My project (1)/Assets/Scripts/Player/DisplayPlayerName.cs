using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisplayPlayerName : MonoBehaviour
{

    private string playerName;
        [SerializeField] Text resultText;


    public void Start()
    {
        playerName = PlayerPrefs.GetString("PlayerName");

        
        
            resultText.text = "<b>"+ playerName.ToUpper() + "</b>";
            resultText.color = Color.black;

        

    }
}
