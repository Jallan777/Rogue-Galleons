using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class continueButtonForInputScense : MonoBehaviour
{
   public Button contButton;

   void Start()
   {
      contButton.interactable = false;

      if (!string.IsNullOrEmpty(PlayerPrefs.GetString("PlayerName")))
      {
         contButton.interactable = true;
      }
   }
   public void PlayGame()
   {
      SceneManager.LoadSceneAsync("LandingScene"); // load the game level

   }


}
