using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameStatus
{
    Anim,
    Start,
    GoBack,
    InsertCoin,
    MissionComplete,
    GameOver
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameStatus gameStatus;
    public GameObject player;    

    [SerializeField]
    private PlaneEnemy[] enemys;

    //targets
    public Transform posInicial, posFinal;    
    public Transform[] posEnemy1, posEnemy2, posEnemy3, posEnemy4, posEnemy5;
    public bool startEnemy1, startEnemy2, startEnemy3, startEnemy4, startEnemy5, posFinalPlayer = false;
    private int positionFinalEnemy1, positionFinalEnemy2, positionFinalEnemy3, positionFinalEnemy4, positionFinalEnemy5 = 0;

    private Animator playerReturn, animMainPlayer;

    //moedas para usar (vida)
    private int playerCoin;
    private bool insertCoinTime = true;
    private bool isRunning = true;

    private float oldTimerCoin;
    public float timerCoin = 10;

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
    }
    
    void Start()
    {
        gameStatus = GameStatus.Anim;
        playerReturn = GetComponent<Animator>();
        animMainPlayer = GetComponent<Animator>();        
        posInicial = GameObject.Find("PosInicial").GetComponent<Transform>();        
        oldTimerCoin = timerCoin;        
        playerCoin = 2;        
    }
    
    void Update()
    {
        StartEnemy();
        insertCoinOrDie();
        if (posFinalPlayer)StartCoroutine(CompleteMission());
        if (OndeEstou.instance.fase == 3 || OndeEstou.instance.fase == 2) Destroy(this.gameObject);
    }

    void insertCoinOrDie()
    {
        if (gameStatus == GameStatus.InsertCoin) useCronoInsertCoin();
        if (gameStatus == GameStatus.InsertCoin && Input.GetKeyDown(KeyCode.Z) && playerCoin > 0)
        {
            insertCoinTime = true;
            timerCoin = oldTimerCoin;
            playerCoin -= 1;
            CriarAviao();
        }
        // tempo acabou ou não tem mais moedas - gameover
        if (gameStatus == GameStatus.InsertCoin && playerCoin == 0 || (gameStatus == GameStatus.InsertCoin && insertCoinTime == false))
        {            
            SceneManager.LoadScene(2);
            gameStatus = GameStatus.GameOver;
        }
    }    

    IEnumerator CompleteMission()
    {
        if(gameStatus == GameStatus.MissionComplete)
        {            
            yield return new WaitForSeconds(5);            
            SceneManager.LoadScene(3);            
        }
    }

    public void StartEnemy()
    {
        if (gameStatus == GameStatus.Start || gameStatus == GameStatus.InsertCoin)
        {
            if (startEnemy1 == true)
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
            if (startEnemy4 == true)
            {
                startEnemy4 = false;
                Instantiate(enemys[3], posEnemy4[positionFinalEnemy4].transform.position, Quaternion.identity);
                positionFinalEnemy4++;
            }
            if (startEnemy5 == true)
            {
                startEnemy5 = false;
                Instantiate(enemys[4], posEnemy5[positionFinalEnemy5].transform.position, Quaternion.identity);
                positionFinalEnemy5++;
            }
        }
    }    
    public void CriarAviao()
    {
        if (gameStatus == GameStatus.Anim)
        {            
            GameObject pl = Instantiate(player, posInicial.position, Quaternion.identity) as GameObject;            
            animMainPlayer.Play("StartPlayer");            
            gameStatus = GameStatus.Start;            
        }        
        else if (gameStatus == GameStatus.InsertCoin)
        {
            playerReturn.Play("PlayerReturns");            
            GameObject pl = Instantiate(player, posInicial.position, Quaternion.identity) as GameObject;
            gameStatus = GameStatus.Start;
        }        
    }

    IEnumerator timeInsertCoin()
    {
        yield return new WaitForSeconds(5);
        insertCoinTime = false; 
    }

    void useCronoInsertCoin()
    {
        if (isRunning)
        {            
            timerCoin -= Time.deltaTime;
            UiManager.instance.textFuel.text = Mathf.RoundToInt(timerCoin).ToString();

            if (timerCoin < 0)
            {
                isRunning = false;
                insertCoinTime = false;
                UiManager.instance.textFuel.enabled = false;
            }
        }
    }    
}

