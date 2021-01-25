using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{    
    [SerializeField]
    private Text textFuel, textCrono, textScore, textHiScore;

    public float timerShotGun = 50;
    public float timerWayShot = 20;

    private float oldTimerShotGun;
    private float oldTimerWayShot;
    private bool isRunning = true;

    private string textUpgrade;

    public Slider lifeBar;

    [SerializeField]
    private Animator animMainEnemy, animMainEnemy1;
    private Animator animMainPlayer, animMotherShip;
    
    
    
    void Start()
    {
        animMainEnemy = GameObject.Find("AnimEnemy").GetComponent<Animator>();
        //animMainEnemy1 = GameObject.Find("AnimEnemy1").GetComponent<Animator>();
        //animMainEnemy[2] = GameObject.Find("AnimEnemy2").GetComponent<Animator>();
        //animMainEnemy[3] = GameObject.Find("AnimEnemy3").GetComponent<Animator>();
        //animMainEnemy[4] = GameObject.Find("AnimEnemy4").GetComponent<Animator>();
        
        animMainPlayer = GameObject.Find("Player").GetComponent<Animator>();
        animMotherShip = GameObject.Find("AircraftCarrierMain").GetComponent<Animator>();

        oldTimerShotGun = timerShotGun;
        oldTimerWayShot = timerWayShot;
        textFuel = GameObject.Find("TextFuel").GetComponent<Text>();
        textCrono = GameObject.Find("TextCrono").GetComponent<Text>();
        textScore = GameObject.Find("TextScore").GetComponent<Text>();
        textCrono.enabled = false;

        lifeBar.minValue = 0;
        lifeBar.maxValue = PlanePlayer.instance.maxLife;
        lifeBar.value = lifeBar.minValue;
    }

    // Update is called once per frame
    void Update()
    {
        InitialAnimation();
        UpgradeText();
        LifeControl();
        ScoreControl();
    }

    private void InitialAnimation()
    {
        if(GameManager.instance.gameStatus == GameStatus.Anim)
        {
            animMainEnemy.Play("StartLevel1");
            //animMainEnemy1.Play("StartLevelEnemy2");  
            StartCoroutine(eventAnimMotherShip());
            StartCoroutine(eventAnimPlayer());
            
        }
    }

    IEnumerator eventAnimMotherShip()
    {
        yield return new WaitForSeconds(3);
        animMotherShip.Play("AnimMain");
    }  
    
    IEnumerator eventAnimPlayer()
    {
        yield return new WaitForSeconds(4.5f);
        GameManager.instance.gameStatus = GameStatus.Start;
        GameManager.instance.CriarAviao();
        animMainPlayer.Play("StartPlayer");
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
            //PlaneController.instance.itemCarregado = false;
        }

        if (PlaneController.instance.up == Upgrades.Auto)
        {
            //textCrono.enabled = true;
            textUpgrade = "AUTO";
            //useCronoAuto();
            PlaneController.instance.itemCarregado = false;
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

    /*void UseCronoAuto()
    {
        if (isRunning)
        {
            if (PlaneController.instance.itemCarregado == true) timerWayShot = oldTimerWayShot;
            timerWayShot -= Time.deltaTime;
            textCrono.text = Mathf.RoundToInt(timerWayShot).ToString();

            if (timerWayShot < 0)
            {
                isRunning = false;
                PlaneController.instance.up = Upgrades.CommonShot;
                textCrono.enabled = false;
            }

        }
    }*/

    void useCronoWayShot()
    {
        if (isRunning)
        {
            if (PlaneController.instance.itemCarregado == true) timerWayShot = oldTimerWayShot;
            timerWayShot -= Time.deltaTime;
            textCrono.text = Mathf.RoundToInt(timerWayShot).ToString();
            
            if (timerWayShot < 0)
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
            if (PlaneController.instance.itemCarregado == true) timerShotGun = oldTimerShotGun;
            timerShotGun -= Time.deltaTime;
            textCrono.text = Mathf.RoundToInt(timerShotGun).ToString();

            if (timerShotGun < 0)
            {
                isRunning = false;
                PlaneController.instance.up = Upgrades.CommonShot;
                textCrono.enabled = false;
            }

        }
    }

   

}
