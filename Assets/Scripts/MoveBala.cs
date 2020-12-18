using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBala : MonoBehaviour
{
    private float vel = 7;

    public float Vel
    {
        get { return vel; }
        set { vel = value; }
    }
    void Move()
    {        
        Vector3 aux = transform.position;
        aux.y += vel * Time.deltaTime;
        transform.position = aux;
    }

    private void Update()
    {
        Move();
    }
}
