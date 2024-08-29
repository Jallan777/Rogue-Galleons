using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;




public class MainMenu : MonoBehaviour
{

    public void PlayGame() 
    {
        SceneManager.LoadSceneAsync(1); // load the game level
    
    }

    public void QuitGame() 
    {
        Application.Quit();
    
    }



   
}
