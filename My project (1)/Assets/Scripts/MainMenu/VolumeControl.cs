using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider volumeSlider;

    // Start is called before the first frame update
    void Start()
    {
        float currentVol;
        audioMixer.GetFloat("MusicVolume", out currentVol);
        volumeSlider.value = currentVol;

        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    public void SetVolume(float volume) {
        audioMixer.SetFloat("MusicVolume", volume);
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
