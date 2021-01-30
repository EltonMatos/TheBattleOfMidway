using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanePlayer : MonoBehaviour
{
    public static PlanePlayer instance;

    public int lifePlayer, minLife, maxLife, scorePlayer;

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

    void Start()
    {
        maxLife = 100;
        minLife = 0;
        scorePlayer = 0;
        lifePlayer = minLife;                       
    }

    public int LifePlayer
    {
        get { return lifePlayer; }
        set { lifePlayer = value; }
    }

    public int SocorePlayer
    {
        get { return scorePlayer; }
        set { scorePlayer = value; }
    }    
}
