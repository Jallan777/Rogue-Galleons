using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;




public class contun : MonoBehaviour
{

    public void contin()
    {
        int randomNumber = Random.Range(1, 4);
        Debug.Log("Random number generated: " + randomNumber);

        if (randomNumber == 1)
        { 
        SceneManager.LoadSceneAsync("Ingame"); 
        
        }
        if (randomNumber == 2)
        {
            SceneManager.LoadSceneAsync("Ingame 2"); 

        }
        if (randomNumber == 3)
        {
            SceneManager.LoadSceneAsync("Ingame 3"); 

        }


    }






}
