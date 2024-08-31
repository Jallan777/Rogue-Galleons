using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    public Slider volumeSlider;
    public AudioSource audioSource;

    void Start()
    {
        // Initialize the slider to match the current volume level
        volumeSlider.value = audioSource.volume;
        // Add a listener to handle value changes
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    public void SetVolume(float sliderValue)
    {
        // Snap to specific volume levels (0%, 50%, 100%)
        float snappedValue = Mathf.Round(sliderValue * 100) / 100; // This will snap to 0, 0.5, 1
        audioSource.volume = snappedValue;
        volumeSlider.value = snappedValue;
    }

}
