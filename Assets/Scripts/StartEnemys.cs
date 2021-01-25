using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartEnemys : MonoBehaviour
{  
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("StartEnemy1"))
        {            
            GameManager.instance.startEnemy1 = true;           
        }

        if (col.gameObject.CompareTag("StartEnemy3"))
        {
            GameManager.instance.startEnemy3 = true;
        }

    }
}
