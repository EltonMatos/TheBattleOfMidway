using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentacaoEnemy : MonoBehaviour
{
    public float speed = 0.05f;
    private Transform pos;    

    private float step;
    private float aux;

    [SerializeField]
    private TipoEnemy tipoEn;

    private float vel = 2;

    public float Vel
    {
        get { return vel; }
        set { vel = value; }
    }
    void Move()
    {
        Vector3 aux = transform.position;
        aux.y += vel * Time.deltaTime;
        transform.position = aux;
    }

    private void Start()
    {
        step = speed * Time.deltaTime;
        aux = -1.0f;
        if (tipoEn == TipoEnemy.Enemy1) transform.Translate(step, 0, 0);
        if (tipoEn == TipoEnemy.Enemy2) transform.Translate(0, step, 0);
    }

    void Update()
    {        
        if (tipoEn == TipoEnemy.Enemy1)
        {            
            //float aux2 = Camera.main.transform.position.y;
            transform.Translate(step, 0, 0);
            if (this.transform.position.x > 4)
            {
                
                step = aux * Time.deltaTime;
            }

            if (this.transform.position.x < -4) step = -aux * Time.deltaTime;            
            
        }
        if(tipoEn == TipoEnemy.Enemy2)
        {
            transform.Translate(0, step*5, 0);
        }
       

    }
}
