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


        if (volumeSlider != null)
        {
            musicSource.volume = volumeSlider.value;
            volumeSlider.onValueChanged.AddListener(SetVolume);

        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Find active Canvas first
        GameObject newMusicObject = GameObject.Find("BG_Music");

        if (newMusicObject != null)
        {
            AudioSource newAudioSource = newMusicObject.GetComponent<AudioSource>();
            if (newAudioSource != null)
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
        // Changes based on if it is Combat Scene or not
        GameObject canvasOrUI = null;
        if(scene.name == "Combat")
        {
            canvasOrUI = GameObject.Find("#UI");
        }
        else
        {
            canvasOrUI = GameObject.Find("Canvas");
        }

        if (canvasOrUI != null)
        {
            // Find  Settings Panel inside  Canvas
            Transform settingsPanel = canvasOrUI.transform.Find("Setting Panel");

            if (settingsPanel != null)
            {
                // Find MusicSlider within Settings Panel
                volumeSlider = settingsPanel.Find("MusicSlider")?.GetComponent<Slider>();

                if (volumeSlider != null)
                {
                    Debug.Log("MusicSlider found: " + volumeSlider.name);

                    // Remove listeners to avoid duplication
                    volumeSlider.onValueChanged.RemoveAllListeners();

                    // Set music volume to current slider value
                    musicSource.volume = volumeSlider.value;

                    // Add new listener for volume changes
                    volumeSlider.onValueChanged.AddListener(SetVolume);
                }
                else
                {
                    Debug.LogError("MusicSlider not found in Setting Panel.");
                }
            }
            else
            {
                Debug.LogError("Setting Panel not found.");
            }
        }
        else
        {
            Debug.LogError((scene.name == "Combat" ? "UI" : "Canvas") + " not found in the scene.");
        }
    }



    public void SetVolume(float volume)
    {
        musicSource.volume = volume;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
