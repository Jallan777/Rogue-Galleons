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

      if(!string.IsNullOrEmpty(PlayerPrefs.GetString("PlayerName")))
      {
         contButton.interactable = true;
      }
   }
       public void PlayGame() 
    {
      int randChoice = Random.Range(0,2);

      
      
          //if(randChoice == 0) {
             SceneManager.LoadSceneAsync("LandingScene"); // load the game level
          //}
          //else{
          //  SceneManager.LoadSceneAsync("PeacefulSea"); // load the game level

          //}
      

       
    
    }
        
  
}
