using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;




public class escape : MonoBehaviour
{

    public void esc()
    {
        SceneManager.LoadSceneAsync("MainMenu"); // load the game level

    }






}
