using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{    
    [SerializeField]
    private Text textLife, textCrono;

    public float timer = 15;

    float oldTimer;
    bool isRunning = true;    
    
    void Start()
    {
        oldTimer = timer;
        textLife = GameObject.Find("TextLife").GetComponent<Text>();
        textCrono = GameObject.Find("TextCrono").GetComponent<Text>();
        textCrono.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(AirplaneController.instance.up == Upgrades.WayShot)
        {
            textCrono.enabled = true;
            useCrono();
            AirplaneController.instance.itemCarregado = false;
        }
        textLife.text = "Vida: " + AirplanePlayer.instance.lifePlayer.ToString();
        
    }

    void useCrono()
    {
        if (isRunning)
        {
            if (AirplaneController.instance.itemCarregado == true) timer = oldTimer;
            timer -= Time.deltaTime;
            textCrono.text = Mathf.RoundToInt(timer).ToString();
            
            if (timer < 0)
            {
                isRunning = false;
                AirplaneController.instance.up = Upgrades.ItemComum;
                textCrono.enabled = false;
            }
                
        }        
    }
}
