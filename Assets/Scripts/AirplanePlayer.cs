using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirplanePlayer : MonoBehaviour
{
    public static AirplanePlayer instance;

    public int lifePlayer, maxLife;
    public float offensivePower, defensivePower, energyLevel, specialWeapons, specialWeaponsTimeLimit;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        //SceneManager.sceneLoaded += Carrega;
    }

    void Start()
    {
        maxLife = 20;
        lifePlayer = maxLife;
        offensivePower = 1;
        defensivePower = 1;
        energyLevel = 1;
        specialWeapons = 1;
        specialWeaponsTimeLimit = 1;
    }
    
    void Update()
    {
        
    }

    public int LifePlayer
    {
        get { return lifePlayer; }
        set { lifePlayer = value; }
    }
}
