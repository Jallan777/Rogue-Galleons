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
         ResetMap();
         
         SceneManager.LoadSceneAsync("LandingScene"); // load the game level
      }

   }

   void Update()
   {
      playerName = PlayerPrefs.GetString("PlayerName");
   }

   void ResetMap()
   {
      //int otherIslandsCount = 3;
      int edgeIslandsCount = 2;
      int centerIslandsCount = 10;
      int index = 0;

      for(int i = 0; i < edgeIslandsCount; i++)
      {
         PlayerPrefs.DeleteKey("EdgeIslandX_" + i);
         PlayerPrefs.DeleteKey("EdgeIslandY_" + i);
      }

      for(int i = 0; i < centerIslandsCount; i++)
      {
         PlayerPrefs.DeleteKey("CenterIslandX_" + i);
         PlayerPrefs.DeleteKey("CenterIslandY_" + i);
      }

      while(PlayerPrefs.HasKey("NodeX_" + (index + 1)))
      {
         PlayerPrefs.DeleteKey("NodeX_" + index);
         PlayerPrefs.DeleteKey("NodeY_" + index);
         index++;
      }


      PlayerPrefs.Save();

   }

}
