using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionPac : MonoBehaviour
{
    public GameObject bigExplosion;


    public void ExplosionEnemy(PlaneEnemy en)
    {
        GameObject explosion = Instantiate(bigExplosion, en.transform.position, Quaternion.identity) as GameObject;
        StartCoroutine(ExplodeEnemy(explosion, en));
    }

    IEnumerator ExplodeEnemy(GameObject explosion, PlaneEnemy en)
    {
        yield return new WaitForSeconds(1); 
        Destroy(explosion);

    }
}
