using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirplaneEnemy : MonoBehaviour
{
    public static AirplaneEnemy instance;

    public int lifeEnemy;

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

    public AirplaneEnemy(int life, TipoEnemy enemy)
    {
        this.lifeEnemy = life;        
    }
    void Start()
    {        
        lifeEnemy = 10;
    }

    public int getLifeEnemy()
    {
        return lifeEnemy;
    }

    public int LifeEnemy
    {
        get { return lifeEnemy; }
        set { lifeEnemy = value; }
    }
    
}
