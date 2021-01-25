using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadAll : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DeadExplosion());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator DeadExplosion()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
