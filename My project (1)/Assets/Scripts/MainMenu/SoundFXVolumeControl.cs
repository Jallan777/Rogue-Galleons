using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SoundFXVolumeControl : MonoBehaviour
{
    public AudioMixer SFXMixer;
    public Slider SFXVolumeSlider;


    public AudioSource SFXSource;

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
                 if(SFXVolumeSlider != null)
         {
            SFXSource.volume = SFXVolumeSlider.value;
            SFXVolumeSlider.onValueChanged.AddListener(SetVolume);

         }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
{
    // Find the active Canvas first
    GameObject canvas = GameObject.Find("Canvas");

    if (canvas != null)
    {
        // Find the Settings Panel (inactive) inside the Canvas
        Transform settingsPanel = canvas.transform.Find("Setting Panel");

        if (settingsPanel != null)
        {
            // Now search for the MusicSlider specifically within the Settings Panel
            SFXVolumeSlider = settingsPanel.Find("SFXSlider")?.GetComponent<Slider>();

            if (SFXVolumeSlider != null)
            {
                Debug.Log("SFXSlider found: " + SFXVolumeSlider.name);

                // Remove previous listeners to avoid duplication
                SFXVolumeSlider.onValueChanged.RemoveAllListeners();

                // Set the music volume to the current slider value
                SFXSource.volume = SFXVolumeSlider.value;

                // Add the new listener for volume changes
                SFXVolumeSlider.onValueChanged.AddListener(SetVolume);
            }
            else
            {
                Debug.LogError("SFXSlider not found in Setting Panel.");
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
        SFXSource.volume = volume;
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
