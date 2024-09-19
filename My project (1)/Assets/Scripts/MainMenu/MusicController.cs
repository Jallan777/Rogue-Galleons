using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{

    private AudioSource audioSource;
    public GameObject musicObject;

private static MusicController instance;
    // Start is called before the first frame update
    void Start()
    {
        

        if(instance == null) {
            instance = this;

            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(musicObject);

            audioSource = musicObject.GetComponent<AudioSource>();
        }
        else {
            Destroy(gameObject);
            Destroy(musicObject);
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
    // Update is called once per frame
    void Update()
    {
        
    }
}
