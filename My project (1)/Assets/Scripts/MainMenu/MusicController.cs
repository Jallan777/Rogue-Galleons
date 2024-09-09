using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
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
    // Update is called once per frame
    void Update()
    {
        
    }
}
