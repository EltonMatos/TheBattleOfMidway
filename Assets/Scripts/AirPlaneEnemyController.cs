using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum TipoEnemy
{
    Enemy1,
    Enemy2,
    Enemy3
}

public class AirPlaneEnemyController : MonoBehaviour
{
    [SerializeField]
    private GameObject linhaTiroPrincipal, linhaTiroEsquerda, linhaTiroDireita, tiroPrincipal;
    public bool lancaTiro;
    private AirplaneEnemy enemy;

    private TipoEnemy tEn;

    [SerializeField]
    private GameObject[] up;

    public Upgrades[] item;

    private void Start()
    {   
        lancaTiro = true;
        StartCoroutine(tiro());
    }

    void Update()
    {
        if(AirplaneEnemy.instance.lifeEnemy <= 0)
        {            
            int aux = Random.Range(0, 3);
            int aux2 = Random.Range(0, 9);             
            if(aux2 >= 8)
            {
                GameObject ups = Instantiate(up[aux], linhaTiroPrincipal.transform.position, Quaternion.identity) as GameObject;
            }            
            Destroy(this.gameObject);            
            GameManager.instance.enemyEmCena--;
        }          
    }

    IEnumerator tiro()
    {
        if(tEn == TipoEnemy.Enemy1)
        {
            while (lancaTiro)
            {
                yield return new WaitForSeconds(1);
                GameObject tiro1 = Instantiate(tiroPrincipal, linhaTiroPrincipal.transform.position, Quaternion.identity) as GameObject;
                tiro1.GetComponent<MoveBalaEnemy>().Vel *= transform.localScale.y;
            }
        }
        if (tEn == TipoEnemy.Enemy2)
        {
            while (lancaTiro)
            {
                yield return new WaitForSeconds(1);
                GameObject tiro2 = Instantiate(tiroPrincipal, linhaTiroPrincipal.transform.position, Quaternion.identity) as GameObject;
                tiro2.GetComponent<MoveBalaEnemy>().Vel *= transform.localScale.y;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("TiroPrincipal"))
        {
            Destroy(col.gameObject);
            AirplaneEnemy.instance.LifeEnemy -= 5; 
        }
        if (col.gameObject.CompareTag("ShotGun"))
        {
            Destroy(col.gameObject);
            AirplaneEnemy.instance.lifeEnemy -= 10;
        }
    }    

    

}
