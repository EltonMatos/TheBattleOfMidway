using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InsertCoin : MonoBehaviour
{
    public Text txtInsertCoin, txtCommands, txtMissionFinish, txtPlayAgain;
    
    private string textMainMenu = "Select Z to insert coin \n\r\n\r X - Shoot \n\r C - Special Power \n\r LeftArrow - Left \n\r RightArrow - Right \n\r UpArrow - Up \n\r DownArrow - Down";
    private string textMissionComplete = "MISSION COMPLETE";

    private string textGameOver;
    
    private char[] ctr;

    private void Start()
    {       

        if (OndeEstou.instance.fase == 0)
        {
            txtInsertCoin = GameObject.Find("TxtInsertCoin").GetComponent<Text>();
            txtCommands = GameObject.Find("TxtCommands").GetComponent<Text>();
            txtInsertCoin.enabled = true;
            StartCoroutine(LoadingTextFalse());
            ctr = textMainMenu.ToCharArray();
            StartCoroutine(ShowText());
        }
        
        if(OndeEstou.instance.fase == 3)
        {
            txtMissionFinish = GameObject.Find("TxtMissionFinish").GetComponent<Text>();
            txtPlayAgain = GameObject.Find("TxtPlayAgain").GetComponent<Text>();
            ctr = textMissionComplete.ToCharArray();
            StartCoroutine(LoadingTextFalse());
            StartCoroutine(ShowTextMc());
        }       
        
    }

    private void Update()
    {
        if(OndeEstou.instance.fase == 0)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {                
                SceneManager.LoadScene(1);
            }
        }

        if(OndeEstou.instance.fase == 3 || OndeEstou.instance.fase == 2)
        {            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GameManager.instance.gameStatus = GameStatus.Anim;
                SceneManager.LoadScene(0);
            }
        }
    }

    IEnumerator LoadingTextFalse()
    {
        yield return new WaitForSeconds(0.3f); 
        if(OndeEstou.instance.fase == 0)
        {
            txtInsertCoin.enabled = false;
            StartCoroutine(LoadingTextTrue());
        }
        if(OndeEstou.instance.fase == 3)
        {
            txtPlayAgain.enabled = false;
            StartCoroutine(LoadingTextTrue());
        }
    }

    IEnumerator LoadingTextTrue()
    {
        yield return new WaitForSeconds(0.3f);        
        if(OndeEstou.instance.fase == 0)
        {
            txtInsertCoin.enabled = true;
            StartCoroutine(LoadingTextFalse());
        }        
        if(OndeEstou.instance.fase == 3)
        {
            txtPlayAgain.enabled = true;
            StartCoroutine(LoadingTextFalse());
        }
    }

    IEnumerator ShowText()
    {
        int count = 0;
        while (count < ctr.Length)
        {
            yield return new WaitForSeconds(0.03f);
            txtCommands.text += ctr[count];
            count++;
        }
    }

    IEnumerator ShowTextMc()
    {
        int count = 0;
        while (count < ctr.Length)
        {
            yield return new WaitForSeconds(0.1f);
            txtMissionFinish.text += ctr[count];
            count++;
        }
    }

    void LoadingScene(int level)
    {
        SceneManager.LoadScene(level);
    }


}
