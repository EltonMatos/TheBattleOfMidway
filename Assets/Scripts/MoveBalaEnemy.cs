using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBalaEnemy : MonoBehaviour
{
    private float vel = 1;
    [SerializeField]
    private TipoEnemy tipoEn;
    [SerializeField]
    private AirplanePlayer player;

    public float Vel
    {
        get { return vel; }
        set { vel = value; }
    }

    
    void Move()
    {
        if(tipoEn == TipoEnemy.Enemy1)
        {
            Vector3 aux = transform.position;
            aux.y -= vel * Time.deltaTime;
            transform.position = aux;
        }
        else if(tipoEn == TipoEnemy.Enemy2)
        {
            Vector3 aux = player.transform.position;
            aux.y -= vel * Time.deltaTime;
            transform.position = aux;
        }
        
    }

    private void Update()
    {
        Move();
    }
}
