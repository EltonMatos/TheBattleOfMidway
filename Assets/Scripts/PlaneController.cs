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
    EnergyTank,
    SideFighter,
    Yashichi
}

public class PlaneController : MonoBehaviour
{
    public static PlaneController instance;

    [SerializeField]
    private GameObject linhaTiroPrincipal, linhaTiroPrincipal2, linhaTiroEsquerda, linhaTiroDireita, linhaTiroCentro;
    [SerializeField]
    private GameObject tiroPrincipal, tiroWayShot, wayShotRight, wayShotLeft, tiroShotGun, shotGunRight, shotGunLeft, tiroAuto;
    public Upgrades up;
    
    public Transform targetShotLeft, targetShotRight, targetShotLeftShotGun, targetShotRightShotGun;

    public bool itemCarregado = false;

    public float fuelTimer = 10;
    private float oldTimer;
    private bool isRunning = true;

    private Animator animExplodePlayer, animFeedbackDano, animExplosionDano, animThunderPower;

    private int powerSpecial;
    public bool posThunder = false;
    public GameObject thunderPower;

    public AudioClip audioShot, audioUpgrade, audioLowLife, audioDead;
    public AudioSource audioS;

    public bool colisionEnemyAndPlayer;

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
        animExplodePlayer = GetComponent<Animator>();
        animFeedbackDano = GetComponent<Animator>();
        animExplosionDano = GetComponent<Animator>();
        animThunderPower = GetComponent<Animator>();

        up = Upgrades.CommonShot;
        powerSpecial = 3;

        colisionEnemyAndPlayer = false;
    }

    void Update()
    {
        Commands();              
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
                //AudioPlay(audioShot);
                GameObject tiro13 = Instantiate(tiroAuto, linhaTiroCentro.transform.position, Quaternion.identity) as GameObject;
            }
            if (Input.GetKeyDown(KeyCode.C) && powerSpecial > 0)
            {
                posThunder = true;
                GameObject thunder = Instantiate(thunderPower, transform.position, Quaternion.identity) as GameObject;                
                powerSpecial--;               
            }
            
            //pouca vida
            if (PlanePlayer.instance.lifePlayer >= 70)
            {
                AudioPlay(audioLowLife);                
            }
            //morreu
            if (PlanePlayer.instance.lifePlayer >= 100)
            {
                animExplodePlayer.Play("ExplodePlayer");
                AudioPlay(audioDead);
                GameManager.instance.gameStatus = GameStatus.GameOver;
                StartCoroutine(ExplodePlane());
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
            PlanePlayer.instance.lifePlayer += 15;
            colisionEnemyAndPlayer = true;
        }
        if (col.gameObject.CompareTag("Enemy2"))
        {
            animFeedbackDano.Play("FeedbackDanoPlayer");
            PlanePlayer.instance.lifePlayer += 15;
        }
        if (col.gameObject.CompareTag("TiroInimigo"))
        {
            animExplosionDano.Play("DanoPlayer");
            Destroy(col.gameObject);
            PlanePlayer.instance.lifePlayer += 5;
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
        if (col.gameObject.CompareTag("ShotGun"))
        {  
            if(up != Upgrades.WayShot || up != Upgrades.Auto)
            {
                AudioPlay(audioUpgrade);
                up = Upgrades.ShotGun;
            }            
            Destroy(col.gameObject);
        }
        if (col.gameObject.CompareTag("WayShot"))
        {
            print("entrou aqui");
            AudioPlay(audioUpgrade);

            up = Upgrades.WayShot;
            itemCarregado = true;
            Destroy(col.gameObject);
        }
        if (col.gameObject.CompareTag("Auto"))
        {
            if (up != Upgrades.WayShot)
            {
                AudioPlay(audioUpgrade);
                up = Upgrades.Auto;
            } 
            Destroy(col.gameObject);
        }
        if (col.gameObject.CompareTag("EnergyTank"))
        {
            AudioPlay(audioUpgrade);
            PlanePlayer.instance.lifePlayer = PlanePlayer.instance.minLife;            
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

        if (!audioS.isPlaying)
        {   
            audioS.clip = audio;
            audioS.Play();
        }
    }


}
