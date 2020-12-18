using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Upgrades
{
    ItemComum,    
    Pow,
    ShotGun,
    WayShot,
    Auto,
    SuperShell,
    EnergyTank,
    SideFighter,
    Yashichi
}

public class AirplaneController : MonoBehaviour
{
    public static AirplaneController instance;

    [SerializeField]
    private GameObject linhaTiroPrincipal, linhaTiroEsquerda, linhaTiroDireita, tiroPrincipal, tiroMaster;
    public Upgrades up;

    public bool itemCarregado = false;

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

        //SceneManager.sceneLoaded += Carrega;
    }

    void Start()
    {
        StartCoroutine(tiro());
        up = Upgrades.ItemComum;
        
    }

    void Update()
    {        
        if (Input.GetKeyDown(KeyCode.X) && up == Upgrades.ItemComum)
        {

            GameObject tiro1 = Instantiate(tiroPrincipal, linhaTiroPrincipal.transform.position, Quaternion.identity) as GameObject;
            tiro1.GetComponent<MoveBala>().Vel *= transform.localScale.y;
        }
        if (Input.GetKeyDown(KeyCode.X) && up == Upgrades.ShotGun)
        {
            GameObject tiro2 = Instantiate(tiroMaster, linhaTiroPrincipal.transform.position, Quaternion.identity) as GameObject;
            tiro2.GetComponent<MoveBala>().Vel *= transform.localScale.y;
        }
        if (Input.GetKeyDown(KeyCode.X) && up == Upgrades.WayShot)
        {
            GameObject tiro3 = Instantiate(tiroMaster, linhaTiroPrincipal.transform.position, Quaternion.identity) as GameObject;
            GameObject tiro4 = Instantiate(tiroMaster, linhaTiroEsquerda.transform.position, Quaternion.identity) as GameObject;
            GameObject tiro5 = Instantiate(tiroMaster, linhaTiroDireita.transform.position, Quaternion.identity) as GameObject;
            tiro3.GetComponent<MoveBala>().Vel *= transform.localScale.y;
            tiro4.GetComponent<MoveBala>().Vel *= transform.localScale.y;
            tiro5.GetComponent<MoveBala>().Vel *= transform.localScale.y;
        }
        if (AirplanePlayer.instance.lifePlayer == 0)
        {            
            GameManager.instance.playerEmCena = 0;
            Destroy(this.gameObject);
        }
    }

    IEnumerator tiro()
    {
        yield return new WaitForSeconds(2);
        
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            AirplanePlayer.instance.lifePlayer -= 5;
        }
        if (col.gameObject.CompareTag("TiroInimigo"))
        {
            Destroy(col.gameObject);
            AirplanePlayer.instance.lifePlayer -= 5;
        }
        if (col.gameObject.CompareTag("Pow"))
        {
            Destroy(col.gameObject);
            if(AirplanePlayer.instance.lifePlayer < AirplanePlayer.instance.maxLife)
            {
                AirplanePlayer.instance.lifePlayer += 5;
                if(AirplanePlayer.instance.lifePlayer > AirplanePlayer.instance.maxLife)
                {
                    AirplanePlayer.instance.lifePlayer = AirplanePlayer.instance.maxLife;
                }
            }            
        }
        if (col.gameObject.CompareTag("ShotGun"))
        {
            if (AirplanePlayer.instance.specialWeapons <= 10)
            {
                AirplanePlayer.instance.specialWeapons++;
            }
            if(up != Upgrades.WayShot)
            {
                up = Upgrades.ShotGun;
            }            
            Destroy(col.gameObject);
        }
        if (col.gameObject.CompareTag("WayShot"))
        {
            if (AirplanePlayer.instance.specialWeapons <= 20)
            {
                AirplanePlayer.instance.specialWeapons++;
            }
            up = Upgrades.WayShot;
            itemCarregado = true;
            Destroy(col.gameObject);
        }
    }

    
}
