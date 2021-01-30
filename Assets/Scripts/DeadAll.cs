using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadAll : MonoBehaviour
{
    //classe criada para destruir objetos que de alguma forma não foram destruídos
    void Start()
    {
        StartCoroutine(DeadExplosion());
    }    

    IEnumerator DeadExplosion()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
