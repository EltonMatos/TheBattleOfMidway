using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3Control : MonoBehaviour
{
    private PlaneEnemy enemy3;

    [SerializeField]
    private GameObject linhaTiroPrincipal, tiroPrincipal;
    [SerializeField]
    private GameObject[] up;
    private StatusEnemy stEnemy;

    private bool shot3 = true;

    private float speed = 2f;
    private float step;

    private int rand;
    private int qntTiro, qntUp = 0;

    public GameObject explosionDano;
    private ExplosionPac explosion;
    


    void Start()
    {
        enemy3 = new PlaneEnemy(5, EnemyType.Enemy3);
        step = -0.01f;
        stEnemy = StatusEnemy.normal;
        rand = Random.Range(-2, 4);
    }
    
    void Update()
    {
        Move();
        if (GameManager.instance.gameStatus == GameStatus.Start)
        {
            Life();
        }
        if (this.transform.position.x <= rand)
        {
            ShotEnemy3();
        }
    }

    void Move()
    {
        if (stEnemy == StatusEnemy.normal)
        {            
            transform.Translate(step * speed, 0, 0);            
        }
    }

    void Life()
    {        
        if (enemy3.LifeEnemy <= 0 && stEnemy == StatusEnemy.normal)
        {
            stEnemy = StatusEnemy.dead;
            PlanePlayer.instance.scorePlayer += 10;            
            GameObject explosion = Instantiate(explosionDano, transform.position, Quaternion.identity) as GameObject;
            DropItem();
            this.gameObject.layer = 12;
            Destroy(gameObject);
        }
        if (PlaneController.instance.colisionEnemyAndPlayer == true)
        {
            PlaneController.instance.colisionEnemyAndPlayer = false;
            stEnemy = StatusEnemy.dead;
            GameObject explosion = Instantiate(explosionDano, transform.position, Quaternion.identity) as GameObject;
            this.gameObject.layer = 12;
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

    private void ShotEnemy3()
    {
        if (GameManager.instance.gameStatus == GameStatus.Start && shot3 == true)
        {
            shot3 = false;
            if (stEnemy == StatusEnemy.normal)
            {
                GameObject tiro2 = Instantiate(tiroPrincipal, linhaTiroPrincipal.transform.position, Quaternion.identity) as GameObject;
                tiro2.GetComponent<MoveShotEnemy>().Vel *= transform.localScale.y;
                StartCoroutine(DestroyShot(tiro2));
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
        if (col.gameObject.CompareTag("TiroPrincipal"))
        {
            enemy3.LifeEnemy -= 3;
            Destroy(col.gameObject);
        }
        if (col.gameObject.CompareTag("ShotGun"))
        {
            enemy3.LifeEnemy -= 5;
            Destroy(col.gameObject);
        }
        if (col.gameObject.CompareTag("Auto"))
        {
            enemy3.LifeEnemy -= 8;
            Destroy(col.gameObject);
        }
        if (col.gameObject.CompareTag("WayShot"))
        {
            enemy3.LifeEnemy -= 10;
            Destroy(col.gameObject);
        }
    }
}
