using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadShot : MonoBehaviour
{
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("TiroPrincipal")|| col.gameObject.CompareTag("ShotGun") || col.gameObject.CompareTag("TiroInimigo") || col.gameObject.CompareTag("Enemy") || col.gameObject.CompareTag("Enemy5"))
        {            
            Destroy(col.gameObject);
        }
        if(col.gameObject.CompareTag("WayShot") || col.gameObject.CompareTag("Auto") || col.gameObject.CompareTag("SuperShell") || col.gameObject.CompareTag("TiroInimigo2"))
        {
            Destroy(col.gameObject);
        }
        if (col.gameObject.CompareTag("ExplosionShot"))
        {
            Destroy(col.gameObject);
        }        
    }   

}
