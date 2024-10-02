using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneRandomizer : MonoBehaviour
{

    public Button contButton;
    public string[] sceneNames;
    // Start is called before the first frame update
    void Start()
    {
        contButton.interactable = false;
        if (!string.IsNullOrEmpty(PlayerPrefs.GetString("PlayerName")))
        {
            contButton.interactable = true;
        }

        contButton.onClick.AddListener(PlayGame);

    }

    public void PlayGame()
    {
        if (sceneNames.Length > 0)
        {
            // Randomly select a scene from array
            int randChoice = Random.Range(0, sceneNames.Length);
            SceneManager.LoadSceneAsync(sceneNames[randChoice]); // Load selected scene
        }
        else
        {
            Debug.LogError("No scenes available to load! Please add scene names in the Inspector.");
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
