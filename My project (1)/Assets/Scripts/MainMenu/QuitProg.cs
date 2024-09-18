using System.Collections;
using UnityEngine;

public class QuitGame : MonoBehaviour
{


    public void Quit()
    {
        StartCoroutine(quitGame());
    }

    IEnumerator quitGame()
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(0.3f);

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
