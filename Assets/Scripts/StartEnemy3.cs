using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartEnemy3 : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("StartEnemy3"))
        {
            GameManager.instance.startEnemy3 = true;
        }

        if (col.gameObject.CompareTag("StartEnemy4"))
        {
            GameManager.instance.startEnemy4 = true;
        }
    }
}
