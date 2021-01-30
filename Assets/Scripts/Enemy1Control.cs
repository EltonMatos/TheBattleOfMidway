using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    Enemy1,
    Enemy2,
    Enemy3,
    Enemy4,
    Enemy5    
}

public enum StatusEnemy
{
    normal,
    dead,
    high,
    low
}

public class Enemy1Control : MonoBehaviour
{
    [SerializeField]
    private GameObject linhaTiroPrincipal, tiroPrincipal;
    [SerializeField]
    private GameObject[] up;
    private PlaneEnemy enemy1;

    [SerializeField]
    private StatusEnemy stEnemy;
    private Animator moveEnemy1;       

    [SerializeField]
    private float speed = 2.2f;    
    private float step;

    private bool checkDown = false;

    public GameObject explosionDano;
    private ExplosionPac explosion;
    private int qntUp, rand = 0;

    private Transform posEnemy;    

    private void Start()
    {               
        moveEnemy1 = GetComponent<Animator>();
        posEnemy = GetComponent<Transform>();
        step = -0.01f;
        stEnemy = StatusEnemy.normal;
        enemy1 = GetComponent<PlaneEnemy>();
        enemy1.LifeEnemy = 3;        
        rand = Random.Range(0, 2);        
    }

    void Update()
    {        
        Move();
        if (GameManager.instance.gameStatus == GameStatus.Start)
        {            
            Life();            
        }        
    }

    void Move()
    {
        if(stEnemy == StatusEnemy.normal)
        {            
            transform.Translate(0, step * speed, 0);
            if (checkDown)
            {                    
                step = step * -1f;
                speed = 4f;
                checkDown = false;
            }                        
        }        
    }

    void Life()
    {
        //morreu
        if (enemy1.LifeEnemy <= 0 && stEnemy == StatusEnemy.normal)
        {
            stEnemy = StatusEnemy.dead;
            PlanePlayer.instance.scorePlayer += 5;
            GameObject explosion = Instantiate(explosionDano, transform.position, Quaternion.identity) as GameObject;
            DropItem();
            this.gameObject.layer = 12;
            Destroy(gameObject);
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

    IEnumerator ShotEnemy1()
    {        
        yield return new WaitForSeconds(rand);
        if (stEnemy == StatusEnemy.normal)
        {
            if (GameManager.instance.gameStatus == GameStatus.Start)
            {
                GameObject tiro1 = Instantiate(tiroPrincipal, linhaTiroPrincipal.transform.position, Quaternion.identity) as GameObject;
                tiro1.GetComponent<MoveShotEnemy>().Vel *= transform.localScale.x;
                StartCoroutine(DestroyShot(tiro1));
            }
        }               
    }

    IEnumerator DestroyShot(GameObject tiro)
    {
        yield return new WaitForSeconds(3);
        Destroy(tiro.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            enemy1.LifeEnemy -= 3;            
        }
        if (col.gameObject.CompareTag("TiroPrincipal"))
        {
            enemy1.LifeEnemy -= 3;
            Destroy(col.gameObject);
        }
        if (col.gameObject.CompareTag("ShotGun"))
        {
            enemy1.LifeEnemy -= 5;
            Destroy(col.gameObject);
        }
        if (col.gameObject.CompareTag("WayShot"))
        {
            enemy1.LifeEnemy -= 8;
            Destroy(col.gameObject);
        }
        if (col.gameObject.CompareTag("Auto"))
        {
            enemy1.LifeEnemy -= 10;
            Destroy(col.gameObject);
        }
        if (col.gameObject.CompareTag("SuperShell"))
        {
            enemy1.LifeEnemy -= 15;
            Destroy(col.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("CheckDown"))
        {
            //tiro acontece depois que inimigo muda de direção
            StartCoroutine(ShotEnemy1());
            moveEnemy1.Play("AnimEnemy1");
            checkDown = true;
        }
    }    
}
