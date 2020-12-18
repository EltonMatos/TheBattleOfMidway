using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cronometro : MonoBehaviour
{
    public float timer = 15;

    float oldTimer;
    bool isRunning = true;
    public Text tempo;

    void Start()
    {
        oldTimer = timer;
    }

    // Update is called once per frame
    void Update()
    {
        if (isRunning)
        {
            timer -= Time.deltaTime;
            tempo.text = Mathf.RoundToInt(timer).ToString();

            if (timer < 0)
                isRunning = false;
        }
    }
}
