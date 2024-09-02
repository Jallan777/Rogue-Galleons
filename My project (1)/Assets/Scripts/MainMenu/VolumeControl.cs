using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    public Slider volumeSlider;
    public AudioSource audioSource;

    void Start()
    {
        // Set the slider to a value corresponding to a percentage (e.g., 65%)
        volumeSlider.value = 65.0f;

        // Add a listener to handle volume change
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    public void SetVolume(float sliderValue)
    {
        // Set the volume directly based on the slider's value normalized between 0 and 1
        float normalizedValue = sliderValue / 100.0f;  // Converting from percentage (0 to 100) to the range Unity uses (0 to 1)
        audioSource.volume = normalizedValue;
    }
}
