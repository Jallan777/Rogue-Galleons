using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MuteButton : MonoBehaviour
{
    public Button muteButton;
    public Button unmuteButton;
    public AudioSource audioSource;
    private bool isMuted = false;


    void Start()
    {
        unmuteButton.gameObject.SetActive(false);
        muteButton.gameObject.SetActive(true);

        muteButton.onClick.AddListener(Mute);
        unmuteButton.onClick.AddListener(Unmute);
    }

    

    void Mute()
    {
        isMuted = true;
        audioSource.mute = isMuted;
        muteButton.gameObject.SetActive(false);
        unmuteButton.gameObject.SetActive(true);
    }

    void Unmute()
    {
        isMuted = false;
        audioSource.mute = isMuted;
        muteButton.gameObject.SetActive(true);
        unmuteButton.gameObject.SetActive(false);
    }
    
}
