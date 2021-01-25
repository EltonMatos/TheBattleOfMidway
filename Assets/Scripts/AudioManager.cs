using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] clip;
    public AudioSource audioS;

    void Update()
    {
        if (!audioS.isPlaying)
        {            
            audioS.clip = clip[0];
            audioS.Play();
        }
    }

    AudioClip GetRandom()
    {
        return clip[Random.Range(0, clip.Length)];
    }
}
