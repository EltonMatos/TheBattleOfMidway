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
        /*int aux = Random.Range(0, up.Length);
        int aux2 = Random.Range(0, 9);
        if (aux2 >= 8 && qntUp == 0)
        {
            GameObject ups = Instantiate(up[aux], en.transform.position, Quaternion.identity) as GameObject;
            qntUp = 1;
        }*/

        Destroy(explosion);

    }
}
