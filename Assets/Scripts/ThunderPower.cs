using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderPower : MonoBehaviour
{    

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Enemy") || col.gameObject.CompareTag("Enemy2"))
        {
            print("funcionou1");
            Destroy(col.gameObject);
        }
    }
}
