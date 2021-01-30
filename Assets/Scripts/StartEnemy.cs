using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartEnemy : MonoBehaviour
{  
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("StartEnemy1"))
        {            
            GameManager.instance.startEnemy1 = true;           
        }        
    }
}
