using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorteTiros : MonoBehaviour
{
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("TiroPrincipal")|| col.gameObject.CompareTag("ShotGun") || col.gameObject.CompareTag("TiroInimigo") || col.gameObject.CompareTag("Enemy"))
        {            
            Destroy(col.gameObject);
        }
        if(col.gameObject.CompareTag("WayShot") || col.gameObject.CompareTag("Auto"))
        {
            Destroy(col.gameObject);
        }
    }   

}
