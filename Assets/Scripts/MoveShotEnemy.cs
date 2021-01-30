using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveShotEnemy : MonoBehaviour
{
    private float vel = 1.6f;
    [SerializeField]
    private EnemyType tipoEn;

    private Transform targetPlayer;

    private bool posPlayer = true;
        
    private float posTarget = 0;
    private Vector2 posTargetPlayer;

    private void Start()
    {
        if(GameManager.instance.gameStatus == GameStatus.Start)
        {
            targetPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();            
            posTarget = targetPlayer.position.x;
        }        
    }

    public float Vel
    {
        get { return vel; }
        set { vel = value; }
    }

    private void Update()
    {        
        Move();                
    }

    void Move()
    { 
        if(tipoEn == EnemyType.Enemy1 && GameManager.instance.gameStatus == GameStatus.Start)
        {
            Vector3 auxEn1 = transform.position;             
            /*if(posTarget == 0)
            {
                auxEn1.y -= vel * Time.deltaTime;
                transform.position = auxEn1;
            }*/
            if (posTarget > 0)
            {                
                auxEn1.x += vel * Time.deltaTime;
                transform.position = auxEn1;
            }
            if (posTarget <= 0)
            {
                auxEn1.x -= vel * Time.deltaTime;
                transform.position = auxEn1;                
            }           
        }
        if (tipoEn == EnemyType.Enemy2 && GameManager.instance.gameStatus == GameStatus.Start)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPlayer.position, 0.02f);
            StartCoroutine(Shot());
        }
        if (tipoEn == EnemyType.Enemy3 || tipoEn == EnemyType.Enemy5)
        {
            Vector3 auxEn1 = transform.position;
            auxEn1.y -= vel * Time.deltaTime;
            transform.position = auxEn1;            
        }
    }
    IEnumerator Shot()
    {
        yield return new WaitForSeconds(2f);
        Destroy(this.gameObject);
    }

    


}
