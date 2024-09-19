using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{


    public void PlayGame()
    {
      
        StartCoroutine(LoadSceneAfterDelay("NameInputPage", 0.1f));
    }

    public void returnGame()
    {

        StartCoroutine(LoadSceneAfterDelay("MainMenu", 0.1f));
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator LoadSceneAfterDelay(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadSceneAsync(sceneName);
    }
}
