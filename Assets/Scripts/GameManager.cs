using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public List<GameObject> enemys;    
    public AirplanePlayer player1;    
    
    public Transform posInicial, posEnemy, posEnemy2;
    public int playerEmCena = 0;
    public int enemyEmCena = 0;

    public Transform objDown, objUp;    

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        SceneManager.sceneLoaded += Carrega;
    }

    void Carrega(Scene cena, LoadSceneMode modo)
    {
        objDown = GameObject.FindWithTag("objDown").GetComponent<Transform>();
        objUp = GameObject.FindWithTag("objUp").GetComponent<Transform>();
        player1 = GameObject.FindWithTag("Player").GetComponent<AirplanePlayer>();
    }

    void Start()
    {
        CriarAviao();
        StartEnemy();
    }
    
    void Update()
    {
        if(enemyEmCena <= 2)
        {
            GameObject e1 = Instantiate(enemys[0], new Vector3(posEnemy.transform.position.x, posEnemy.transform.position.y, 0), Quaternion.identity) as GameObject;
            GameObject e2 = Instantiate(enemys[1], new Vector3(posEnemy2.transform.position.x, posEnemy2.transform.position.y, 0), Quaternion.identity) as GameObject;
            enemyEmCena++;

            e1.GetComponent<MovimentacaoEnemy>().Vel *= transform.localScale.y;
            e2.GetComponent<MovimentacaoEnemy>().Vel *= transform.localScale.y;            
        }
    }

    public void StartEnemy()
    {
        //Instantiate(enemys[0], new Vector3(posEnemy.transform.position.x, posEnemy.transform.position.y, 0), Quaternion.identity);
        //Instantiate(enemys[1], new Vector3(posEnemy2.transform.position.x, posEnemy.transform.position.y, 0), Quaternion.identity);
        //Instantiate(enemys[1], posEnemy2.transform.position, Quaternion.identity);
        //enemyEmCena++;        
    }

    void CriarAviao()
    {   
        if (player1.transform.position != posInicial.position && playerEmCena == 0)
        {            
            player1.transform.position = posInicial.position;
            playerEmCena = 1;
        }
    }
}
