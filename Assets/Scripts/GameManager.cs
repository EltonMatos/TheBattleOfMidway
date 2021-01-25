using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameStatus
{
    Anim,
    Start,
    Pause,
    GameOver
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameStatus gameStatus;
    
    private PlanePlayer player1;    

    [SerializeField]
    private PlaneEnemy[] enemys;    

    public Transform posInicial;
    private int playerEmCena = 0;       
        
    public Transform[] posEnemy1;
    public Transform[] posEnemy2;
    public Transform[] posEnemy3;
    public bool startEnemy1, startEnemy2, startEnemy3 = false;
    private int positionFinalEnemy1, positionFinalEnemy2, positionFinalEnemy3 = 0;
    

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        SceneManager.sceneLoaded += Carrega;
    }

    void Carrega(Scene cena, LoadSceneMode modo)
    {
        player1 = GameObject.FindWithTag("Player").GetComponent<PlanePlayer>();
    }
    void Start()
    {
        gameStatus = GameStatus.Start;
        if(gameStatus == GameStatus.Start)
        {
            CriarAviao();
        }
    }
    
    void Update()
    {
        if(gameStatus == GameStatus.Start)
        {            
            StartEnemy();
        }        
    }

    public void StartEnemy()
    {
        if(startEnemy1 == true)
        {
            startEnemy1 = false;           
            Instantiate(enemys[0], posEnemy1[positionFinalEnemy1].transform.position, Quaternion.identity);            
            positionFinalEnemy1++;
        }
        if (startEnemy2 == true)
        {
            startEnemy2 = false;
            Instantiate(enemys[1], posEnemy2[positionFinalEnemy2].transform.position, Quaternion.identity);
            positionFinalEnemy2++;            
        }
        if (startEnemy3 == true)
        {
            startEnemy3 = false;
            Instantiate(enemys[2], posEnemy3[positionFinalEnemy3].transform.position, Quaternion.identity);            
            positionFinalEnemy3++;
        }
    }    

    public void CriarAviao()
    {   
        if (player1.transform.position != posInicial.position && playerEmCena == 0)
        {            
            player1.transform.position = posInicial.position;
            playerEmCena = 1;
        }
    }

    


}

