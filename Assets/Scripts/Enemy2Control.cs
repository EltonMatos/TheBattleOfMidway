using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Enemy2Control : MonoBehaviour
{
    [SerializeField]
    private GameObject linhaTiroPrincipal, tiroPrincipal;
    public bool shotThrowEnemy2;   

    [SerializeField]
    private GameObject[] up;
    private PlaneEnemy enemy2;
    private int qntTiro, qntUp = 0;    
    private StatusEnemy stEnemy;
    private Animator animBigExplodeEnemy;
    private Animator animMoveEnemy2Down, animMoveEnemy2Left, animMoveEnemy2Right;      
    
    private float step = 0.02f;
    private float rand, movePos;
    private bool checkLeft, checkRight, checkDown, checkUp = false;  

    public GameObject explosionDano, explosionShot;
    public Transform targetUpEnemy2, targetDownEnemy2;


    private void Start()
    {
        enemy2 = GetComponent<PlaneEnemy>();
        enemy2.LifeEnemy = 200;

        animBigExplodeEnemy = GetComponent<Animator>();
        animMoveEnemy2Left = GetComponent<Animator>();
        animMoveEnemy2Right = GetComponent<Animator>();
        animMoveEnemy2Down = GetComponent<Animator>();

        stEnemy = StatusEnemy.normal;        
        shotThrowEnemy2 = true;

        //move
        if (GameManager.instance.gameStatus == GameStatus.Start)
        {
            StartCoroutine(Shot());            
        }

        // define direções de movimento do inimigo
        rand = Random.Range(-1, 1);        
        if (rand == 0) rand = 1;
        rand = rand / 100;

        targetUpEnemy2 = GameObject.Find("TargetUpEnemy2").GetComponent<Transform>();
        targetDownEnemy2 = GameObject.Find("TargetDownEnemy2").GetComponent<Transform>();

        // variavel que vai indicar se o avião vai estar baixo ou alto
        movePos = -1;

        Anim();
    }

    void Update()
    {
        if(GameManager.instance.gameStatus != GameStatus.MissionComplete)
            Move();

        if (GameManager.instance.gameStatus == GameStatus.Start)
        {
            Life();
            CollisionEnemy2();
        }                    
            
    }

    void Anim()
    { 
        if (movePos == 1)
        {
            stEnemy = StatusEnemy.low;
            animMoveEnemy2Down.Play("MoveEnemy2Down");            
            this.gameObject.layer = 13;
            checkDown = true;
            StartCoroutine(MoveDownUp());
        }
        if (movePos == -1)
        {
            stEnemy = StatusEnemy.normal;
            animMoveEnemy2Down.Play("MoveEnemy2Up");            
            this.gameObject.layer = 11;
            checkLeft = true;
            StartCoroutine(Shot());
            StartCoroutine(MoveDownUp());
        }
    }

    void Move()
    { 
        if (stEnemy == StatusEnemy.normal)
        {             
            transform.Translate(rand, step, 0);
            if (checkLeft) rand = -1f * rand;
            if (checkRight) rand = 1f * rand;
            if (checkUp)step = -1f * Time.deltaTime; 
            if (checkDown)step = 2.5f * Time.deltaTime;
        }
        if (stEnemy == StatusEnemy.low)
        {
            transform.Translate(rand, step, 0);
            if (checkLeft) rand = -1f * rand;            
            if(checkRight) rand = 1f * rand;
            if (checkUp)step = -1f * Time.deltaTime;            
            if (checkDown)step = 2.5f * Time.deltaTime;
        }
    }

    void Life()
    {        
        if (enemy2.LifeEnemy <= 0 && stEnemy == StatusEnemy.normal)
        {
            stEnemy = StatusEnemy.dead;
            PlanePlayer.instance.scorePlayer += 200;
            GameObject explosion = Instantiate(explosionDano, transform.position, Quaternion.identity) as GameObject;            
            DropItem();
            this.gameObject.layer = 12;
            Destroy(gameObject);            
        }        
    }

    void DropItem()
    {
        int aux = Random.Range(1, up.Length);
        int aux2 = Random.Range(0, 9);
        if (aux2 >= 8 && qntUp == 0)
        {
            GameObject ups = Instantiate(up[aux - 1], linhaTiroPrincipal.transform.position, Quaternion.identity) as GameObject;
            qntUp = 1;
        }
    }

    IEnumerator Shot()
    {        
        while (shotThrowEnemy2)
        {
            yield return new WaitForSeconds(1f);
            if (GameManager.instance.gameStatus == GameStatus.Start)
            {
                GameObject tiro2 = Instantiate(tiroPrincipal, linhaTiroPrincipal.transform.position, Quaternion.identity) as GameObject;
                tiro2.GetComponent<MoveShotEnemy>().Vel *= transform.localScale.y;
                qntTiro++;
                if (qntTiro >= 4)
                {
                    shotThrowEnemy2 = false;
                    StartCoroutine(LaunchAgainShot());
                }
                StartCoroutine(DestroyShot(tiro2));
            }            
        }        
    }                
    
    IEnumerator MoveDownUp()
    {
        yield return new WaitForSeconds(2);
        if (movePos == -1) movePos = 1;
        else movePos = -1;
        Anim();        
    }

    IEnumerator DestroyShot(GameObject tiro)
    {
        yield return new WaitForSeconds(3);
        Destroy(tiro.gameObject);
    }

    IEnumerator LaunchAgainShot()
    {
        yield return new WaitForSeconds(3);
        shotThrowEnemy2 = true;
        qntTiro = 0;
        if(stEnemy == StatusEnemy.normal)
        {
            StartCoroutine(Shot());
        }        
    }

    IEnumerator ExplodeEnemy()
    {
        yield return new WaitForSeconds(1.5f);
        int aux = Random.Range(0, up.Length);
        int aux2 = Random.Range(0, 9);        
        if (aux2 >= 2 && qntUp == 0)
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
            enemy2.LifeEnemy -= 3;
            Destroy(col.gameObject);            
            StartCoroutine(ExplodeShot(shot));
        }
        if (col.gameObject.CompareTag("ShotGun"))
        {
            GameObject shot = Instantiate(explosionShot, transform.position, Quaternion.identity) as GameObject;
            enemy2.LifeEnemy -= 5;
            Destroy(col.gameObject);
            StartCoroutine(ExplodeShot(shot));
        }
        if (col.gameObject.CompareTag("Auto"))
        {
            GameObject shot = Instantiate(explosionShot, transform.position, Quaternion.identity) as GameObject;
            enemy2.LifeEnemy -= 8;
            Destroy(col.gameObject);
            StartCoroutine(ExplodeShot(shot));
        }
        if (col.gameObject.CompareTag("WayShot"))
        {
            GameObject shot = Instantiate(explosionShot, transform.position, Quaternion.identity) as GameObject;
            enemy2.LifeEnemy -= 10;
            Destroy(col.gameObject);
            StartCoroutine(ExplodeShot(shot));
        }
        if (col.gameObject.CompareTag("SuperShell"))
        {
            GameObject shot = Instantiate(explosionDano, transform.position, Quaternion.identity) as GameObject;
            enemy2.LifeEnemy -= 15;
            Destroy(col.gameObject);
            StartCoroutine(ExplodeShot(shot));
        }
        if (col.gameObject.CompareTag("Player"))
        {
            GameObject shot = Instantiate(explosionDano, linhaTiroPrincipal.transform.position, Quaternion.identity) as GameObject;
            enemy2.LifeEnemy -= 10;            
            StartCoroutine(ExplodeShot(shot));
        }
    }

    IEnumerator ExplodeShot(GameObject shot)
    {
        yield return new WaitForSeconds(1);
        Destroy(shot);
    }
    //define o limite de movimentação do inimigo
    private void CollisionEnemy2()
    {
        if(transform.position.x <= -4)
        {
            animMoveEnemy2Right.Play("MoveEnemy2Right");
            checkLeft = true;
            checkRight = false;
            checkUp = false;
            checkDown = false;
        }
        if(transform.position.x >= 4)
        {
            animMoveEnemy2Left.Play("MoveEnemy2Left");
            checkRight = true;
            checkLeft = false;
            checkUp = false;
            checkDown = false;
        }
        if(transform.position.y >= targetUpEnemy2.transform.position.y)
        {
            checkUp = true;
            checkRight = false;
            checkLeft = false;
            checkDown = false;
        }
        if(transform.position.y <= targetDownEnemy2.transform.position.y)
        {
            checkDown = true;
            checkUp = false;
            checkRight = false;
            checkLeft = false;
        }
    }



}
