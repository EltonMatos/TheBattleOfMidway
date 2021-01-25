using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Enemy2Control : MonoBehaviour
{
    [SerializeField]
    private GameObject linhaTiroPrincipal, linhaTiroEsquerda, linhaTiroDireita, tiroPrincipal;
    public bool shotThrowEnemy2;   

    [SerializeField]
    private GameObject[] up;
    private PlaneEnemy enemy2;
    private int qntTiro, qntUp = 0;    
    private StatusEnemy stEnemy;
    private Animator animBigExplodeEnemy;
    private Animator animMoveEnemy2Down, animMoveEnemy2Left, animMoveEnemy2Right;


    
    private Transform pos;
    private float step = 0.01f;

    private float rand, movePos;
    private bool checkLeft, checkRight, checkDown, checkUp = false;

    private Transform target;


    private void Start()
    {
        enemy2 = new PlaneEnemy(100, EnemyType.Enemy2);

        animBigExplodeEnemy = GetComponent<Animator>();
        animMoveEnemy2Left = GetComponent<Animator>();
        animMoveEnemy2Right = GetComponent<Animator>();
        animMoveEnemy2Down = GetComponent<Animator>();

        //stEnemy = StatusEnemy.normal;
        
        shotThrowEnemy2 = true;

        //move
        if (GameManager.instance.gameStatus == GameStatus.Start)
        {
            StartCoroutine(Shot());
            //target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        }

        rand = Random.Range(-1, 1);        
        if (rand == 0) rand = 1;
        rand = rand / 100;

        movePos = -1;
        Anim();

    }

    void Update()
    {
        Move();

        if (GameManager.instance.gameStatus == GameStatus.Start)
        {            
            Life();            
        }
    }

    void Anim()
    { 
        if (movePos == 1)
        {
            stEnemy = StatusEnemy.low;
            animMoveEnemy2Down.Play("MoveEnemy2Down");            
            this.gameObject.layer = 12;
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
        if(stEnemy == StatusEnemy.normal)
        {             
            transform.Translate(rand, step, 0);            
            if (checkLeft == true || checkRight == true)
            {
                rand = -1f * rand;
                checkLeft = false;
                checkRight = false;

            }
            if (checkUp)
            {
                step = 1f;
                step = -step * Time.deltaTime;
                checkUp = false;
            }                        
        }
        if (stEnemy == StatusEnemy.low)
        {
            transform.Translate(rand, step, 0);
            if (checkDown)
            {
                step = 1.5f;
                step = step * Time.deltaTime;
                //rand = 0.5f;
                checkDown = false;
            }
        }
    }

    void Life()
    {        
        if (enemy2.LifeEnemy <= 0)
        {
            stEnemy = StatusEnemy.dead;            
            animBigExplodeEnemy.Play("BigExplodeEnemy2");
            PlanePlayer.instance.scorePlayer += 200;
            StartCoroutine(ExplodeEnemy());
        }        
    }

    IEnumerator Shot()
    {        
        while (shotThrowEnemy2)
        {
            yield return new WaitForSeconds(1f);
            GameObject tiro2 = Instantiate(tiroPrincipal, linhaTiroPrincipal.transform.position, Quaternion.identity) as GameObject;
            tiro2.GetComponent<MoveShotEnemy>().Vel *= transform.localScale.y;
            qntTiro++;
            if (qntTiro >= 4)
            {
                shotThrowEnemy2 = false;
                StartCoroutine(lancarNovamente());
            }            
            StartCoroutine(DestroyShot(tiro2));
        }
        
    }                
    
    IEnumerator MoveDownUp()
    {
        yield return new WaitForSeconds(3);
        if (movePos == -1) movePos = 1;
        else movePos = -1;
        Anim();
        
    }

    IEnumerator DestroyShot(GameObject tiro)
    {
        yield return new WaitForSeconds(3);
        Destroy(tiro.gameObject);
    }

    IEnumerator lancarNovamente()
    {
        yield return new WaitForSeconds(3);
        shotThrowEnemy2 = true;
        qntTiro = 0;
        if(stEnemy == StatusEnemy.high)
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
            enemy2.LifeEnemy -= 3;
            Destroy(col.gameObject);
        }
        if (col.gameObject.CompareTag("ShotGun"))
        {
            enemy2.LifeEnemy -= 5;
            Destroy(col.gameObject);
        }
        if (col.gameObject.CompareTag("Auto"))
        {
            enemy2.LifeEnemy -= 8;
            Destroy(col.gameObject);
        }
        if (col.gameObject.CompareTag("WayShot"))
        {
            enemy2.LifeEnemy -= 10;
            Destroy(col.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("CheckLeft"))
        {
            animMoveEnemy2Right.Play("MoveEnemy2Right");
            checkLeft = true;
        }
        if (col.gameObject.CompareTag("CheckRight"))
        {
            animMoveEnemy2Left.Play("MoveEnemy2Left");
            checkRight = true;
        }
        if (col.gameObject.CompareTag("CheckUp"))
        {
            checkUp = true;
        }      

    }



}
