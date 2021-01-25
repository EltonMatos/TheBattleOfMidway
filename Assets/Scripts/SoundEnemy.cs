using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEnemy : MonoBehaviour
{
    public StatusEnemy stEnemy;

    public PlaneEnemy enemy;

    public AudioClip audioExplosion;
    public AudioSource audioS;

    private void Update()
    {        

        if (stEnemy == StatusEnemy.dead)
        {
            AudioPlay(audioExplosion);
        }
    }


    void AudioPlay(AudioClip audio)
    {
        if (!audioS.isPlaying)
        {
            audioS.clip = audio;
            audioS.Play();
        }
    }

}
