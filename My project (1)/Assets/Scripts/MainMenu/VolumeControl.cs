using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    public Slider volumeSlider;
    public AudioSource audioSource;

    void Start()
    {
        //  default settings
        float initialVolume = PlayerPrefs.GetFloat("VolumeLevel", 65.00f);
        audioSource.volume = initialVolume;
        volumeSlider.value = initialVolume;

       
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    //adjusting the volumn
    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
        PlayerPrefs.SetFloat("VolumeLevel", volume);
    }
}
