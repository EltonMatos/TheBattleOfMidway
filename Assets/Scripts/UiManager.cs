using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public static UiManager instance;

    public GameStatus gameStatus;
        
    private Text textCrono, textScore, textHiScore, txtInsertCoin;

    //cronometros
    private float timerShotGun = 50;
    private float timerWayShot = 20;
    private float timerAuto = 40;
    private float oldTimerShotGun;
    private float oldTimerWayShot;
    private float oldTimerAuto;
    private bool isRunning = true;

    private string textUpgrade;

    public Slider lifeBar;

    private Transform startAnim, startAnim2, startAnim3, startAnim4, startAnim5, startExplosion, startExplosion1, startExplosion2;

    public GameObject[] enemyAnim;
    public GameObject bigExplosion;

    [SerializeField]
    private Animator animMainEnemy, animMainEnemy1, animMainEnemy2, animMainEnemy3, animMainEnemy4;
    private Animator animMainPlayer, animMotherShip;

    public Text textFuel;

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
        if(OndeEstou.instance.fase == 1)
        {
            txtInsertCoin = GameObject.Find("TxtInsertCoin").GetComponent<Text>();
            txtInsertCoin.enabled = false;

            oldTimerShotGun = 50;
            oldTimerWayShot = 20;
            oldTimerAuto = 40;
            textFuel = GameObject.Find("TextFuel").GetComponent<Text>();
            textCrono = GameObject.Find("TextCrono").GetComponent<Text>();
            textScore = GameObject.Find("TextScore").GetComponent<Text>();
            textCrono.enabled = false;

            lifeBar.minValue = 0;            
            lifeBar.value = lifeBar.minValue;
        }
    }

    void Start()
    {
        if (OndeEstou.instance.fase == 1)
        {
            animMainEnemy = GameObject.Find("AnimEnemy").GetComponent<Animator>();
            animMainEnemy1 = GameObject.Find("AnimEnemy (1)").GetComponent<Animator>();
            animMainEnemy2 = GameObject.Find("AnimEnemy (2)").GetComponent<Animator>();
            animMainEnemy3 = GameObject.Find("AnimEnemy (3)").GetComponent<Animator>();
            animMainEnemy4 = GameObject.Find("AnimEnemy (4)").GetComponent<Animator>();
            startAnim = GameObject.Find("PositionStartAnim").GetComponent<Transform>();
            startAnim2 = GameObject.Find("PositionStartAnim2").GetComponent<Transform>();
            startAnim3 = GameObject.Find("PositionStartAnim3").GetComponent<Transform>();
            startAnim4 = GameObject.Find("PositionStartAnim4").GetComponent<Transform>();
            startAnim5 = GameObject.Find("PositionStartAnim5").GetComponent<Transform>();
            animMainEnemy.transform.position = startAnim.position;
            animMainEnemy1.transform.position = startAnim2.position;
            animMainEnemy2.transform.position = startAnim3.position;
            animMainEnemy3.transform.position = startAnim4.position;
            animMainEnemy4.transform.position = startAnim5.position;

            startExplosion = GameObject.Find("Explosion").GetComponent<Transform>();
            startExplosion1 = GameObject.Find("Explosion1").GetComponent<Transform>();
            startExplosion2 = GameObject.Find("Explosion2").GetComponent<Transform>();

            StartCoroutine(LoadingTextInsertCoinTrue());

            animMotherShip = GameObject.Find("AircraftCarrierMain").GetComponent<Animator>();
            //animMainPlayer = GameObject.Find("Player").GetComponent<Animator>();
            
            InitialAnimation();
        }            
    }
    
    void Update()
    {
        if(GameManager.instance.gameStatus == GameStatus.Start)
        {
            UpgradeText();
            LifeControl();
            ScoreControl();
        }
        if (GameManager.instance.gameStatus == GameStatus.InsertCoin) textCrono.enabled = false;
        if (OndeEstou.instance.fase == 3 || OndeEstou.instance.fase == 2) Destroy(this.gameObject);
    }

    private void InitialAnimation()
    {
        if(GameManager.instance.gameStatus == GameStatus.Anim)
        {            
            animMainEnemy.Play("StartLevel1");
            animMainEnemy1.Play("StartLevel1");
            animMainEnemy2.Play("StartLevel1");
            animMainEnemy3.Play("StartLevel1");
            animMainEnemy4.Play("StartLevel1");

            StartCoroutine(eventAnimMotherShip());
            StartCoroutine(eventAnimPlayer());            
        }
    }    

    IEnumerator eventAnimMotherShip()
    {
        yield return new WaitForSeconds(2);
        GameObject ex = Instantiate(bigExplosion, startExplosion.transform.position, Quaternion.identity) as GameObject;
        GameObject ex1 = Instantiate(bigExplosion, startExplosion1.transform.position, Quaternion.identity) as GameObject;
        GameObject ex2 = Instantiate(bigExplosion, startExplosion2.transform.position, Quaternion.identity) as GameObject;
        animMotherShip.Play("AnimMain");
        //animMainPlayer.Play("StartPlayer");
    }  
    
    IEnumerator eventAnimPlayer()
    {
        yield return new WaitForSeconds(3.5f);
        GameManager.instance.CriarAviao();                       
        yield return new WaitForSeconds(1.8f);
        Destroy(enemyAnim[4]);
        Destroy(enemyAnim[3]);
        Destroy(enemyAnim[2]);
        Destroy(enemyAnim[1]);
        Destroy(enemyAnim[0]);
        
    }

    IEnumerator LoadingTextInsertCoinFalse()
    {
        if(OndeEstou.instance.fase == 1)
        {
            yield return new WaitForSeconds(0.5f);
            txtInsertCoin.enabled = false;
            StartCoroutine(LoadingTextInsertCoinTrue());
        }        
    }

    IEnumerator LoadingTextInsertCoinTrue()
    {
        if (OndeEstou.instance.fase == 1)
        {
            yield return new WaitForSeconds(0.5f);
            txtInsertCoin.enabled = true;
            StartCoroutine(LoadingTextInsertCoinFalse());
        }        
    }

    private void UpgradeText()
    {
        if (PlaneController.instance.up == Upgrades.CommonShot)
        {
            textUpgrade = " ";
        }

        if (PlaneController.instance.up == Upgrades.WayShot)
        {            
            textCrono.enabled = true;
            textUpgrade = "WAY SHOT";
            useCronoWayShot();
            PlaneController.instance.itemCarregado = false;
        }

        if (PlaneController.instance.up == Upgrades.ShotGun)
        {
            textCrono.enabled = true;
            textUpgrade = "SHOT GUN";
            useCronoShotGun();
            PlaneController.instance.itemCarregado = false;
        }

        if (PlaneController.instance.up == Upgrades.Auto)
        {
            textCrono.enabled = true;
            textUpgrade = "AUTO";
            UseCronoAuto();
            PlaneController.instance.itemCarregado = false;
        }

        if (PlaneController.instance.up == Upgrades.SuperShell)
        {
            textCrono.enabled = true;
            textUpgrade = "SUPER SHELL";
            UseContSuperShell();            
        }
        textFuel.text = textUpgrade;        
    }

    private void LifeControl()
    {
        lifeBar.value = PlanePlayer.instance.lifePlayer;
        if (lifeBar.value >= PlanePlayer.instance.maxLife)
        {
            lifeBar.value = PlanePlayer.instance.maxLife;
        }
        if (lifeBar.value <= lifeBar.minValue)
        {
            lifeBar.value = lifeBar.minValue;
        }
    }

    private void ScoreControl()
    {
        textScore.text = "1PLAYER" + "\n\r" + PlanePlayer.instance.scorePlayer.ToString();
    }    

    void UseCronoAuto()
    {
        if (isRunning)
        {
            if (PlaneController.instance.itemCarregado == true) timerAuto = 40;
            PlaneController.instance.itemCarregado = false;
            timerAuto -= Time.deltaTime;
            textCrono.text = Mathf.RoundToInt(timerAuto).ToString();

            if (timerAuto <= 0)
            {
                isRunning = false;
                PlaneController.instance.up = Upgrades.CommonShot;
                textCrono.enabled = false;                
            }
        }
    }

    void useCronoWayShot()
    {
        if (isRunning)
        {
            if (PlaneController.instance.itemCarregado == true) timerWayShot = 20;
            PlaneController.instance.itemCarregado = false;
            timerWayShot -= Time.deltaTime;
            textCrono.text = Mathf.RoundToInt(timerWayShot).ToString();
            
            if (timerWayShot <= 0)
            {
                isRunning = false;
                PlaneController.instance.up = Upgrades.CommonShot;
                textCrono.enabled = false;                
            }
                
        }        
    }

    void useCronoShotGun()
    {
        if (isRunning)
        {
            if (PlaneController.instance.itemCarregado == true) timerShotGun = 50;
            PlaneController.instance.itemCarregado = false;
            timerShotGun -= Time.deltaTime;
            textCrono.text = Mathf.RoundToInt(timerShotGun).ToString();

            if (timerShotGun <= 0)
            {
                isRunning = false;
                PlaneController.instance.up = Upgrades.CommonShot;
                textCrono.enabled = false;                
            }

        }
    }

    void UseContSuperShell()
    {
        if(PlaneController.instance.superShell <= 0)
        {
            PlaneController.instance.up = Upgrades.CommonShot;
            textCrono.enabled = false;
            PlaneController.instance.itemCarregado = false;
        }

        textCrono.text = PlaneController.instance.superShell.ToString();
    }



}
