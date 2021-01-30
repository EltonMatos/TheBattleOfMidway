using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionComplete : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            GameManager.instance.posFinalPlayer = true;
            if(GameManager.instance.gameStatus != GameStatus.InsertCoin)
            {
                GameManager.instance.gameStatus = GameStatus.MissionComplete;
            }           
        }
    }
}
