using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MusicController : MonoBehaviour
{

    private AudioSource audioSource;
    public GameObject musicObject;

private static MusicController instance;

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
        
        if(instance == null) {
            instance = this;

            DontDestroyOnLoad(gameObject);
            musicObject = gameObject;
            audioSource = musicObject.GetComponent<AudioSource>();
        }
        else {

             Destroy(gameObject);
        }
        
    }

    public void PlayMusic() {
        if(!audioSource.isPlaying) {
            audioSource.Play();
        }
    }

    public void StopMusic() {
        if(audioSource.isPlaying) {
            audioSource.Stop();
        }
    }

    public void VolSet(float volume) {
        audioSource.volume = volume;
    }

    public void SetTrackPos(float time) {
        audioSource.time = time;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
    
    GameObject newMusicObject = GameObject.Find("BG_Music");

    if (newMusicObject != null)
    {
        AudioSource newAudioSource = newMusicObject.GetComponent<AudioSource>();
        if(newAudioSource != null)
        {
            audioSource = newAudioSource;
            PlayMusic();
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
}

    // Update is called once per frame
    void Update()
    {
        
    }
}
