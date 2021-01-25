using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public float speed = 5f;

    private Animator animMoveLeft, animMoveRight;
    private bool moveRight, moveLeft = false;


    private void Start()
    {
        animMoveLeft = GetComponent<Animator>();
        animMoveRight = GetComponent<Animator>();
    }

    void Update()
    {
        if(GameManager.instance.gameStatus == GameStatus.GameOver)
        {
            speed = 2f;
        }
        
        if(GameManager.instance.gameStatus == GameStatus.Start)
        {
            Move();
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
