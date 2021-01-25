using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartEnemys2 : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("StartEnemy2"))
        {            
            GameManager.instance.startEnemy2 = true; 
        }
    }
}
