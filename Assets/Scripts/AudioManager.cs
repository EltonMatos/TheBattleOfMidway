using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioClip audioLevel1, audioGameOver, audioMissioComplete;
    public AudioSource audioS;

    public bool audioPerfectFase1, audioPerfectFase2, audioPerfectFase3 = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (OndeEstou.instance.fase == 0 && audioS.isPlaying)
        {            
            audioS.Stop();
        }
        if (OndeEstou.instance.fase == 1 && audioS.isPlaying && audioPerfectFase1 == false)
        {
            audioPerfectFase1 = true;
            audioS.Stop();
        }
        if (OndeEstou.instance.fase == 1 && !audioS.isPlaying)
        {            
            audioS.clip = audioLevel1;
            audioS.Play();
        }
        if (OndeEstou.instance.fase == 2 && audioS.isPlaying && audioPerfectFase2 == false)
        {
            audioPerfectFase2 = true;
            audioS.Stop();
        }
        if (OndeEstou.instance.fase == 2 && !audioS.isPlaying)
        {
            audioS.clip = audioGameOver;
            audioS.Play();
        }
        if (OndeEstou.instance.fase == 3 && audioS.isPlaying && audioPerfectFase3 == false)
        {
            audioPerfectFase3 = true;
            audioS.Stop();
        }
        if (OndeEstou.instance.fase == 3 && !audioS.isPlaying)
        {
            audioS.clip = audioMissioComplete;
            audioS.Play();
        }        
    }
}
