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

        if (input.Length < 3 )
        {
            resultText.text = "Invalid Input, Please type longer name";
            resultText.color = Color.red;
        }
        else if(input.Length > 15)
        {
            resultText.text = "Invalid Input, Please type shorter name";
            resultText.color = Color.red;
        }
        else
        {
            resultText.text = "Welcome <b>"+ input + "</b>";
            resultText.color = Color.blue;

        }

    }
}