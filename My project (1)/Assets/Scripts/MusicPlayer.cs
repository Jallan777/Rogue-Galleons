using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] clip;
    float randomNum;
    int state;

    // Use this for initialization
     void Start()
    {
       randomPlay();
    }
    
     // Update is called once per frame
     void Update()
    {

        if (!audioSource.isPlaying) {

            randomPlay();
        }

     }
 
    void randomPlay()
     {
         randomNum = Random.Range(1, 11);
         state = (int)randomNum; 
         audioSource.clip = clip[state-1];
         audioSource.Play();
       
    }

}
