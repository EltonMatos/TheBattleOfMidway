using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollows : MonoBehaviour
{
    private float t = 1f;
    private float speed = 0.016f;
    private float speed2 = 0.005f;

    public Transform objCamDonw, objCamUp, speedCam1;
    private bool testeCam = false;

    void Update()
    {
        if(GameManager.instance.gameStatus == GameStatus.Start)
        {
            //diminuir a velocidade da cam depois de alcançar o target
            if (transform.position.y >= speedCam1.position.y && testeCam == false)
            {
                speed = 0.01f;
                testeCam = true;
            }
            
            t -= speed * Time.deltaTime;
            transform.position = new Vector3(0, Mathf.SmoothStep(objCamUp.position.y, objCamDonw.position.y, t), -10);            
        } 

        if (GameManager.instance.gameStatus == GameStatus.InsertCoin)
        {            
            t -= speed2 * Time.deltaTime;
            transform.position = new Vector3(0, Mathf.SmoothStep(objCamUp.position.y, objCamDonw.position.y, t), -10);
        }
        

    }

    
}
