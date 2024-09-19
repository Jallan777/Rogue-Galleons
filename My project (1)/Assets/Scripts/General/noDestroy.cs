using UnityEngine;

public class PersistentSoundEffect : MonoBehaviour
{
    private static PersistentSoundEffect instance = null;

    void Awake()
    {
        // Ensure that only one instance of this GameObject exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // Prevent this GameObject from being destroyed on scene loads
        }
        else
        {
            Destroy(gameObject);  // Destroy duplicate instances
        }
    }
}
