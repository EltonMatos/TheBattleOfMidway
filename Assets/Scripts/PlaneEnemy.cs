using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneEnemy : MonoBehaviour
{
    public static PlaneEnemy instance;

    private int lifeEnemy;
    
    public bool checkColision;
    private EnemyType enemy;

    
    public int LifeEnemy
    {
        get { return lifeEnemy; }
        set { lifeEnemy = value; }
    }

    public EnemyType GetTipoEnemy()
    {
        return enemy;
    }

    public bool CheckCollision
    {
        get { return checkColision; }
        set { checkColision = value; }
    }
    
}
