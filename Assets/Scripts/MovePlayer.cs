using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public float speed = 5f;

    private Animator animMoveLeft, animMoveRight;
    private bool moveRight, moveLeft = false;

    private Transform targetFinish, targetDeadPlayer;


    private void Start()
    {
        animMoveLeft = GetComponent<Animator>();
        animMoveRight = GetComponent<Animator>();

        targetFinish = GameObject.Find("posFinal").GetComponent<Transform>();
        if(GameManager.instance.gameStatus == GameStatus.Start)
            targetDeadPlayer = GameObject.Find("TargetDeadPlayer").GetComponent<Transform>();
    }

    void Update()
    {
        if(OndeEstou.instance.fase == 1)
        {
            if (GameManager.instance.gameStatus == GameStatus.InsertCoin)
            {
                transform.position = Vector2.MoveTowards(transform.position, targetDeadPlayer.position, 0.02f);                
            }

            if (GameManager.instance.gameStatus == GameStatus.Start)
            {
                Move();
            }
            if (GameManager.instance.gameStatus == GameStatus.MissionComplete && GameManager.instance.gameStatus != GameStatus.InsertCoin)
            {                
                transform.position = Vector2.MoveTowards(transform.position, targetFinish.position, 0.05f);
                this.gameObject.layer = 12;
            }
        }
    }

    void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        if (horizontal > 0 || horizontal < 0)
        {
            transform.Translate(horizontal * Time.deltaTime * speed, 0, 0);
            if (horizontal > 0)
            {
                animMoveRight.Play("MoveRightPlayer");
                moveRight = true;
            }
            if (horizontal < 0)
            {
                animMoveLeft.Play("MoveLeftPlayer");
                moveLeft = true;
            }

        }

        if (vertical > 0 || vertical < 0)
        {            
            transform.Translate(0, vertical * Time.deltaTime * speed, 0);
        }

        if (horizontal == 0)
        {
            if (moveRight)
            {
                animMoveRight.Play("MoveRightPlayer_invers");
                moveRight = false;
            }
            if (moveLeft)
            {
                animMoveLeft.Play("MoveLeftPlayer_invers");
                moveLeft = false;
            }
        }
    }
}
