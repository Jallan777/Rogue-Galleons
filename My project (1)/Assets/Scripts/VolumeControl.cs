using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider volumeSlider;

    public AudioSource musicSource;

    // Start is called before the first frame update
    void Start()
    {
 
 musicSource.volume = volumeSlider.value;
 volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    public void SetVolume(float volume) {
        musicSource.volume = volume;
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
