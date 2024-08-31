using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputHandler : MonoBehaviour
{
    [SerializeField] InputField inputField;
    [SerializeField] Text resultText;


    public void VaildateInput()
    {
        string input = inputField.text;

        if (input.Length < 3)
        {
            resultText.text = "Invalid input, Please type longer";
            resultText.color = Color.red;
        }
        else
        {
            resultText.text = "welcome "+ input;
            resultText.color = Color.blue;
        }

    }
}