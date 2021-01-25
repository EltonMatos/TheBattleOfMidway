using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy5Control : MonoBehaviour
{
    [SerializeField]
    private GameObject linhaTiroPrincipal, tiroPrincipal;
    [SerializeField]
    private GameObject[] up;

    private PlaneEnemy enemy5;

    [SerializeField]
    private Transform tagLeft, tagRight;
    private float posX = 0.01f;

    private int qntUp = 0;

    [SerializeField]
    private StatusEnemy stEnemy;

    public GameObject bigExplosion;


    [SerializeField]
    private float speed = 2.5f;
    private float step;

    private bool checkDown = false;


    void Start()
    {
        enemy5 = new PlaneEnemy(50, EnemyType.Enemy5);        
        step = 0.01f;
        stEnemy = StatusEnemy.normal;        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if (GameManager.instance.gameStatus == GameStatus.Start)
        {
            Life();
            if(enemy5.LifeEnemy <= 30 && stEnemy == StatusEnemy.normal)
            {                
                Shot();
            }
        }
    }

    void Move()
    {
        if (stEnemy == StatusEnemy.normal)
        {
            transform.Translate(posX, step * speed, 0);
            if (checkDown)
            {
                step = step * -1f;
                speed = 1f;
                checkDown = false;
            }
            if(transform.position.x <= tagLeft.position.x)
            {
                posX = 0.01f;
            }
            if(transform.position.x >= tagRight.position.x)
            {
                posX = -0.01f;
            }
        }
    }

    void Life()
    {
        if (enemy5.LifeEnemy <= 0 && stEnemy == StatusEnemy.normal)
        {
            stEnemy = StatusEnemy.dead;
            GameObject explosion = Instantiate(bigExplosion, transform.position, Quaternion.identity) as GameObject;
            this.gameObject.layer = 12;
            PlanePlayer.instance.scorePlayer += 20;
            DropItem();            
        }
    }
    void DropItem()
    {
        int aux = Random.Range(0, up.Length);
        int aux2 = Random.Range(0, 9);
        if (aux2 >= 8 && qntUp == 0)
        {
            GameObject ups = Instantiate(up[aux], linhaTiroPrincipal.transform.position, Quaternion.identity) as GameObject;
            qntUp = 1;
        }
    }

    void Shot()
    {
        qntUp++;
        if (qntUp == 1)
        {
            GameObject tiro = Instantiate(tiroPrincipal, linhaTiroPrincipal.transform.position, Quaternion.identity) as GameObject;
            tiro.GetComponent<MoveShotEnemy>().Vel *= transform.localScale.y;
            StartCoroutine(DestroyShot(tiro));
            StartCoroutine(lancarNovamente());
        }
        
    }

    IEnumerator DestroyShot(GameObject tiro)
    {
        yield return new WaitForSeconds(3);
        Destroy(tiro.gameObject);
    }

    IEnumerator lancarNovamente()
    {
        yield return new WaitForSeconds(3);
        qntUp = 0;
        Shot();        
    }

    IEnumerator ExplodeEnemy()
    {
        yield return new WaitForSeconds(1.5f);
        int aux = Random.Range(0, up.Length);
        int aux2 = Random.Range(0, 9);
        if (aux2 >= 8 && qntUp == 0)
        {
            GameObject ups = Instantiate(up[aux], linhaTiroPrincipal.transform.position, Quaternion.identity) as GameObject;
            qntUp = 1;
        }
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("TiroPrincipal"))
        {
            enemy5.LifeEnemy -= 3;
            Destroy(col.gameObject);
        }
        if (col.gameObject.CompareTag("ShotGun"))
        {
            enemy5.LifeEnemy -= 5;
            Destroy(col.gameObject);
        }
        if (col.gameObject.CompareTag("WayShot"))
        {
            enemy5.LifeEnemy -= 8;
            Destroy(col.gameObject);
        }
        if (col.gameObject.CompareTag("Auto"))
        {
            enemy5.LifeEnemy -= 10;
            Destroy(col.gameObject);
        }
    }

}
