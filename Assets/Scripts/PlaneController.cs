using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Upgrades
{
    CommonShot,    
    Pow,
    ShotGun,
    WayShot,
    Auto,
    SuperShell,
    EnergyTank    
}

public class PlaneController : MonoBehaviour
{
    public static PlaneController instance;

    [SerializeField]
    private GameObject linhaTiroPrincipal, linhaTiroPrincipal2, linhaTiroEsquerda, linhaTiroDireita, linhaTiroCentro;
    [SerializeField]
    private GameObject tiroPrincipal, tiroWayShot, wayShotRight, wayShotLeft, tiroShotGun, shotGunRight, shotGunLeft, tiroAuto, tiroSuperShell;
    public Upgrades up;
    
    public Transform targetShotLeft, targetShotRight, targetPlayerPosition;

    public bool itemCarregado = false;

    public float fuelTimer = 10;
    private float oldTimer;
    private bool isRunning = true;

    private Animator animExplodePlayer, animDanoCollision, animExplosionDano, animThunderPower, animGoBack;
    public int powerSpecial, superShell;
    

    public AudioClip audioShot, audioUpgrade, audioLowLife, audioDead, audioDano, audioGoBack;
    public AudioSource audioS, audioL;

    public bool soundLowLife;

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

        //SceneManager.sceneLoaded += Carrega;
    }

    void Start()
    {
        if(OndeEstou.instance.fase == 1)
        {
            targetPlayerPosition.position = transform.position;
        }
        animExplodePlayer = GetComponent<Animator>();
        animDanoCollision = GetComponent<Animator>();
        animExplosionDano = GetComponent<Animator>();
        animThunderPower = GetComponent<Animator>();

        animGoBack = GetComponent<Animator>();

        up = Upgrades.CommonShot;
        powerSpecial = 3;
        superShell = 10;
        
        soundLowLife = false;
    }

    void Update()
    {
        Commands();
        if(GameManager.instance.gameStatus == GameStatus.MissionComplete)
        {
            audioS.Stop();
        }
    }

    void Commands()
    {
        if (GameManager.instance.gameStatus == GameStatus.Start)
        {
            useFuel();
            if (Input.GetKeyDown(KeyCode.X) && up == Upgrades.CommonShot)
            {
                AudioPlay(audioShot);
                GameObject tiro1 = Instantiate(tiroPrincipal, linhaTiroCentro.transform.position, Quaternion.identity) as GameObject;
            }
            if (Input.GetKeyDown(KeyCode.X) && up == Upgrades.ShotGun)
            {
                AudioPlay(audioShot);
                GameObject tiro3 = Instantiate(tiroShotGun, linhaTiroCentro.transform.position, Quaternion.identity) as GameObject;
                GameObject tiro5 = Instantiate(shotGunLeft, linhaTiroPrincipal.transform.position, Quaternion.identity) as GameObject;
                GameObject tiro6 = Instantiate(shotGunLeft, linhaTiroEsquerda.transform.position, Quaternion.identity) as GameObject;
                GameObject tiro7 = Instantiate(shotGunRight, linhaTiroDireita.transform.position, Quaternion.identity) as GameObject;
                GameObject tiro8 = Instantiate(shotGunRight, linhaTiroPrincipal2.transform.position, Quaternion.identity) as GameObject;

            }
            if (Input.GetKeyDown(KeyCode.X) && up == Upgrades.WayShot)
            {
                AudioPlay(audioShot);
                GameObject tiro9 = Instantiate(tiroWayShot, linhaTiroCentro.transform.position, Quaternion.identity) as GameObject;
                GameObject tiro10 = Instantiate(wayShotLeft, linhaTiroPrincipal.transform.position, Quaternion.identity) as GameObject;
                GameObject tiro11 = Instantiate(wayShotRight, linhaTiroPrincipal2.transform.position, Quaternion.identity) as GameObject;
            }
            if (Input.GetKeyDown(KeyCode.X) && up == Upgrades.Auto)
            {
                AudioPlay(audioShot);
                GameObject tiro13 = Instantiate(tiroAuto, linhaTiroCentro.transform.position, Quaternion.identity) as GameObject;
            }
            if (Input.GetKeyDown(KeyCode.X) && up == Upgrades.SuperShell)
            {
                AudioPlay(audioShot);
                GameObject tiro14 = Instantiate(tiroSuperShell, linhaTiroCentro.transform.position, Quaternion.identity) as GameObject;
                superShell--;
            }
            if (Input.GetKeyDown(KeyCode.C) && powerSpecial > 0)
            {
                //GameManager.instance.gameStatus = GameStatus.GoBack;
                animGoBack.Play("GoBack");
                AudioPlay(audioGoBack);
                powerSpecial--;               
            }
            
            //pouca vida
            if (PlanePlayer.instance.lifePlayer >= 70 && soundLowLife == false)
            {
                audioL.clip = audioLowLife;
                audioL.Play();
                soundLowLife = true;
            }
            //morreu
            if (PlanePlayer.instance.lifePlayer >= 100)
            {
                GameManager.instance.gameStatus = GameStatus.InsertCoin;
                animExplodePlayer.Play("ExplodePlayer");
                AudioPlay(audioDead);                
                StartCoroutine(ExplodePlane());                
                this.gameObject.layer = 12;                
            }
        }
    }

    IEnumerator ExplodePlane()
    {
        yield return new WaitForSeconds(3);        
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        //dano inimigo
        if (col.gameObject.CompareTag("Enemy"))
        {
            animDanoCollision.Play("FeedbackDanoPlayer");
            PlanePlayer.instance.lifePlayer += 15;            
        }
        if (col.gameObject.CompareTag("Enemy2"))
        {
            animDanoCollision.Play("FeedbackDanoPlayer");
            PlanePlayer.instance.lifePlayer += 20;
        }
        if (col.gameObject.CompareTag("Enemy5"))
        {
            animDanoCollision.Play("FeedbackDanoPlayer");
            PlanePlayer.instance.lifePlayer += 15;
        }
        if (col.gameObject.CompareTag("TiroInimigo"))
        {
            animExplosionDano.Play("DanoPlayer");
            AudioPlay(audioDano);
            Destroy(col.gameObject);
            PlanePlayer.instance.lifePlayer += 10;
        }
        if (col.gameObject.CompareTag("TiroInimigo2"))
        {
            animExplosionDano.Play("DanoPlayer");
            AudioPlay(audioDano);
            Destroy(col.gameObject);
            PlanePlayer.instance.lifePlayer += 15;
        }

        //Upgrades
        if (col.gameObject.CompareTag("Pow"))
        {            
            if(PlanePlayer.instance.lifePlayer < PlanePlayer.instance.maxLife)
            {
                AudioPlay(audioUpgrade);
                PlanePlayer.instance.lifePlayer -= 5;
                if(PlanePlayer.instance.lifePlayer > PlanePlayer.instance.maxLife)
                {
                    PlanePlayer.instance.lifePlayer = PlanePlayer.instance.maxLife;
                }
            }
            Destroy(col.gameObject);
        }
        if (col.gameObject.CompareTag("ShotGun") && itemCarregado == false)
        { 
            AudioPlay(audioUpgrade);
            up = Upgrades.ShotGun;                       
            Destroy(col.gameObject);
        }
        if (col.gameObject.CompareTag("WayShot") && itemCarregado == false)
        {
            AudioPlay(audioUpgrade);
            up = Upgrades.WayShot;
            itemCarregado = true;
            Destroy(col.gameObject);
        }
        if (col.gameObject.CompareTag("Auto") && itemCarregado == false)
        {
            
            AudioPlay(audioUpgrade);
            up = Upgrades.Auto;
            itemCarregado = true;             
            Destroy(col.gameObject);
        }
        if (col.gameObject.CompareTag("EnergyTank"))
        {
            AudioPlay(audioUpgrade);
            PlanePlayer.instance.lifePlayer = PlanePlayer.instance.minLife;            
            Destroy(col.gameObject);
        }
        if (col.gameObject.CompareTag("SuperShell") && itemCarregado == false)
        {
            AudioPlay(audioUpgrade);
            up = Upgrades.SuperShell;
            itemCarregado = true;
            Destroy(col.gameObject);
        }
    }

    void useFuel()
    {
        if (isRunning)
        { 
            fuelTimer -= Time.deltaTime;
            oldTimer = Mathf.RoundToInt(fuelTimer);

            if (oldTimer <= 0)
            {
                fuelTimer = 10;
                PlanePlayer.instance.lifePlayer += 2;
            }
        }
    }

    void AudioPlay(AudioClip audio)
    {           
        audioS.clip = audio;
        audioS.Play();        
    }

}
