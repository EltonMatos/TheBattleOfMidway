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

    private int qntUp, qntShot = 0;

    [SerializeField]
    private StatusEnemy stEnemy;

    public GameObject bigExplosion;
    public GameObject explosionShot;

    [SerializeField]
    private float speed = 4f;
    private float step = 1f;

    private bool checkDown = false;

    private Animator feedbackDano;   


    void Start()
    {
        enemy5 = GetComponent<PlaneEnemy>();
        enemy5.LifeEnemy = 100;
        feedbackDano = GetComponent<Animator>();
        step = 0.01f;
        stEnemy = StatusEnemy.normal;
        tagLeft = GameObject.Find("TagLeft").GetComponent<Transform>();
        tagRight = GameObject.Find("TagRight").GetComponent<Transform>();
    }
        
    void Update()
    {
        Move();
        if (GameManager.instance.gameStatus == GameStatus.Start)
        {
            Life();
            if(enemy5.LifeEnemy <= 70 && stEnemy == StatusEnemy.normal)
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
                step = step * 1f;
                speed = 3f;
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
    void Shot()
    {        
        if (qntShot == 0)
        {
            qntShot++;
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
        yield return new WaitForSeconds(1);
        qntShot = 0;
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
            GameObject shot = Instantiate(explosionShot, transform.position, Quaternion.identity) as GameObject;
            enemy5.LifeEnemy -= 3;
            Destroy(col.gameObject);
            StartCoroutine(ExplodeShot(shot));
        }
        if (col.gameObject.CompareTag("ShotGun"))
        {
            GameObject shot = Instantiate(explosionShot, transform.position, Quaternion.identity) as GameObject;
            enemy5.LifeEnemy -= 5;
            Destroy(col.gameObject);
            StartCoroutine(ExplodeShot(shot));
        }
        if (col.gameObject.CompareTag("WayShot"))
        {
            GameObject shot = Instantiate(explosionShot, transform.position, Quaternion.identity) as GameObject;
            enemy5.LifeEnemy -= 8;
            Destroy(col.gameObject);
            StartCoroutine(ExplodeShot(shot));
        }
        if (col.gameObject.CompareTag("Auto"))
        {
            GameObject shot = Instantiate(explosionShot, transform.position, Quaternion.identity) as GameObject;
            enemy5.LifeEnemy -= 10;
            Destroy(col.gameObject);
            StartCoroutine(ExplodeShot(shot));
        }
        if (col.gameObject.CompareTag("SuperShell"))
        {
            GameObject shot = Instantiate(explosionShot, transform.position, Quaternion.identity) as GameObject;
            enemy5.LifeEnemy -= 15;
            Destroy(col.gameObject);
            StartCoroutine(ExplodeShot(shot));
        }
        if (col.gameObject.CompareTag("Player"))
        {
            GameObject shot = Instantiate(explosionShot, linhaTiroPrincipal.transform.position, Quaternion.identity) as GameObject;
            enemy5.LifeEnemy -= 10;
            StartCoroutine(ExplodeShot(shot));
        }
    }

    IEnumerator ExplodeShot(GameObject shot)
    {
        yield return new WaitForSeconds(1);
        Destroy(shot);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {        
        if (col.gameObject.CompareTag("CheckDown"))
        {
            checkDown = true;            
        }
    }
}
