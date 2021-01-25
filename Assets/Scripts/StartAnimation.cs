using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAnimation : MonoBehaviour
{

    public Animator animStartExplosion, animStart;
    
    // Start is called before the first frame update
    void Start()
    {
        animStartExplosion = GetComponent<Animator>();
        animStart = GetComponent<Animator>();
        animStart.Play("MotherShip");
        //animStartExplosion.Play("BigExplosion");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StartExplosion()
    {
        animStartExplosion.Play("BigExplosion");
        print("exexutou");
    }
}
