using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSegue : MonoBehaviour
{
    private float t = 1;
    void Update()
    {        
        t -= 0.02f * Time.deltaTime;
        transform.position = new Vector3(0, Mathf.SmoothStep(GameManager.instance.objUp.position.y, GameManager.instance.objDown.position.y, t), -10);  
    }
}
