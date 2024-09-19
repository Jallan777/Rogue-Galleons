using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VolumeControl : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider volumeSlider;


    public AudioSource musicSource;
    private GameObject musicObject;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    // Start is called before the first frame update

    void Start()
    {
 
        
         if(volumeSlider != null)
         {
            musicSource.volume = volumeSlider.value;
            volumeSlider.onValueChanged.AddListener(SetVolume);

         }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
    // Find the active Canvas first
    GameObject newMusicObject = GameObject.Find("BG_Music");

    if (newMusicObject != null)
    {
        AudioSource newAudioSource = newMusicObject.GetComponent<AudioSource>();
        if(newAudioSource != null)
        {
            musicSource = newAudioSource;
        }
        else
        {
            Debug.Log("No AudioSource found on BG_Music");
        }
    }
    else
    {
        Debug.LogError("BG_Music not found in the scene.");
    }

    GameObject canvas = GameObject.Find("Canvas");

    if (canvas != null)
    {
        // Find the Settings Panel (inactive) inside the Canvas
        Transform settingsPanel = canvas.transform.Find("Setting Panel");

        if (settingsPanel != null)
        {
            // Now search for the MusicSlider specifically within the Settings Panel
            volumeSlider = settingsPanel.Find("MusicSlider")?.GetComponent<Slider>();

            if (volumeSlider != null)
            {
                Debug.Log("MusicSlider found: " + volumeSlider.name);

                // Remove previous listeners to avoid duplication
                volumeSlider.onValueChanged.RemoveAllListeners();

                // Set the music volume to the current slider value
                musicSource.volume = volumeSlider.value;

                // Add the new listener for volume changes
                volumeSlider.onValueChanged.AddListener(SetVolume);
            }
            else
            {
                Debug.LogError("MusicSlider not found in Setting Panel.");
            }
        }
        else
        {
            Debug.LogError("Setting Panel not found in Canvas.");
        }
    }
    else
    {
        Debug.LogError("Canvas not found in the scene.");
    }
}



    public void SetVolume(float volume) {
        musicSource.volume = volume;
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
