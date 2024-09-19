using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class continueButtonForInputScense : MonoBehaviour
{
    public float delay = 0.1f; 

    public void PlayGame()
    {
        StartCoroutine(delayScene());
    }

    IEnumerator delayScene()
    {

        yield return new WaitForSeconds(delay);

 
        int randChoice = Random.Range(0, 7);

        if (randChoice == 0)
        {
            SceneManager.LoadSceneAsync("LandingScene");  
        }
        if (randChoice == 1)
        {
            SceneManager.LoadSceneAsync("PeacefulSea");   
        }
        if (randChoice == 2)
        {
            SceneManager.LoadSceneAsync("ingame 1");   
        }
        if (randChoice == 3)
        {
            SceneManager.LoadSceneAsync("ingame 2");

        }
        if (randChoice == 4)
        {
            SceneManager.LoadSceneAsync("Combat");

        }
        if (randChoice == 5)
        {
            SceneManager.LoadSceneAsync("Combat");

        }
        if (randChoice == 6)
        {
            SceneManager.LoadSceneAsync("Combat");

        }
    }
}
