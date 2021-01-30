using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartEnemy2 : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("StartEnemy2"))
        {            
            GameManager.instance.startEnemy2 = true; 
        }

        if (col.gameObject.CompareTag("StartEnemy5"))
        {
            GameManager.instance.startEnemy5 = true;
        }
    }
}
