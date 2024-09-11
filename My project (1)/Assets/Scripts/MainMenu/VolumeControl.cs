using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    public Slider volumeSlider;
    public AudioSource audioSource;


    void Start()
    {
        //default setting
        volumeSlider.value = 65.0f;

       //changes
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    public void SetVolume(float sliderValue)
    {
      
        float normalizedValue = sliderValue / 100.0f;  //convert to precentage
        audioSource.volume = normalizedValue;
    }
}
