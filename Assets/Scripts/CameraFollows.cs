using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollows : MonoBehaviour
{
    private float t = 1f;

    public Transform objCamDonw, objCamUp;    

    void Update()
    {
        if(GameManager.instance.gameStatus == GameStatus.Start)
        {
            t -= 0.015f * Time.deltaTime;
            transform.position = new Vector3(0, Mathf.SmoothStep(objCamUp.position.y, objCamDonw.position.y, t), -10);            
        }          
    }

    
}
