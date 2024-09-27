using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class continueButtonForInputScense : MonoBehaviour
{
   public Button contButton;
   private string playerName;
   //public string name;

   void Start()
   {
      playerName = PlayerPrefs.GetString("PlayerName");
   }
   public void PlayGame()
   {
      if(playerName.Length > 0)
      {
         SceneManager.LoadSceneAsync("LandingScene"); // load the game level
      }

   }

   void Update()
   {
      playerName = PlayerPrefs.GetString("PlayerName");
   }


}
