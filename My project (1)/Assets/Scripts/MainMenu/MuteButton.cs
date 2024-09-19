using UnityEngine;
using UnityEngine.UI;

public class MuteButton : MonoBehaviour
{
    public Button muteButton;
    public Button unmuteButton;
    public AudioSource audioSource;
    private bool isMuted = false;

    void Start()
    {
        //muteButton.gameObject.SetActive(true);
        //unmuteButton.gameObject.SetActive(false);


        muteButton.onClick.AddListener(Mute);
        unmuteButton.onClick.AddListener(Unmute);
    }

    void ToggleMute()
    {
        isMuted = !isMuted;
        audioSource.mute = isMuted;
    }

    void Mute()
    {
        muteButton.gameObject.SetActive(false);
        unmuteButton.gameObject.SetActive(true);

        isMuted = true;
        audioSource.mute = isMuted;


    }

    void Unmute()
    { 
        muteButton.gameObject.SetActive(true);
        unmuteButton.gameObject.SetActive(false);
        isMuted = false;
        audioSource.mute = isMuted;


    }
}
