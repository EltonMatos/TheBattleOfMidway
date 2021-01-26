using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InsertCoin : MonoBehaviour
{
    public Text txtInsertCoin;
    public Text txtCommands;
    
    private void Start()
    {
        txtInsertCoin = GameObject.Find("TxtInsertCoin").GetComponent<Text>();
        //txtCommands = GameObject.Find("TxtCommands").GetComponent<Text>();

        txtInsertCoin.enabled = true;
        StartCoroutine(LoadingTextFalse());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            LoadingScene();
        }
    }

    IEnumerator LoadingTextFalse()
    {
        yield return new WaitForSeconds(0.3f); 
        txtInsertCoin.enabled = false;           
        StartCoroutine(LoadingTextTrue());       
    }

    IEnumerator LoadingTextTrue()
    {
        yield return new WaitForSeconds(0.3f);        
        txtInsertCoin.enabled = true;            
        StartCoroutine(LoadingTextFalse());        
    }

    void LoadingScene()
    {
        SceneManager.LoadScene(1);
    }


}
