using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimentacao : MonoBehaviour
{
    public float speed = 5f;    

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        if (horizontal > 0 || horizontal < 0)
        {
            transform.Translate(horizontal * Time.deltaTime * speed, 0, 0);           
            
        }

        if (vertical > 0 || vertical < 0)
        {
            transform.Translate(0, vertical * Time.deltaTime * speed, 0);         
        }

    }
}
