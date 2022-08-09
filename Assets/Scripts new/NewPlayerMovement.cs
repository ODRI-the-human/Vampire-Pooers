using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NewPlayerMovement : MonoBehaviour
{
    private Vector2 moveDirection;
    float dodgeTimer;
    float dodgeTimerLength = 15;
    int isDodging = 0;
    public float moveSpeed = 5;
    bool playerControlled;

    public GameObject dodgeAudio;
    public Rigidbody2D rb;
    GameObject Player;

    void Awake()
    {
        if (gameObject.tag == "Hostile")
        {
            Player = GameObject.Find("newPlayer");
            playerControlled = false;
        }
        else
        {
            playerControlled = true;
        }
    }

    void Update()
    {
        switch (playerControlled)
        {
            case true:
                if (isDodging == 0)
                {
                    float moveX = Input.GetAxisRaw("Horizontal");
                    float moveY = Input.GetAxisRaw("Vertical");
                    moveDirection = new Vector2(moveX, moveY).normalized;
                }

                if (dodgeTimer < -30)
                {
                    if (Input.GetButton("Dodge"))
                    {
                        Debug.Log("Dodge the Roll");
                        dodgeTimer = dodgeTimerLength * (0.7f);
                        //iFrames = dodgeTimerLength;
                        isDodging = 1;
                        Instantiate(dodgeAudio);
                    }
                }

                if (moveDirection.x > 0)
                {
                    gameObject.transform.localScale = new Vector3(1f, 1f, 1);
                }
                else
                {
                    if (moveDirection.x < 0)
                    {
                        gameObject.transform.localScale = new Vector3(-1f, 1f, 1);
                    }
                }

                break;
            case false:
                moveDirection = (Player.transform.position - gameObject.transform.position).normalized;
                break;
        }

        if (isDodging == 1)
        {
            if (dodgeTimer > 0)
            {
                gameObject.GetComponent<Collider2D>().enabled = false;
            }
            else
            {
                isDodging = 0;
                gameObject.GetComponent<Collider2D>().enabled = true;
            }
        }
    }

    void FixedUpdate()
    {
        rb.velocity = (1 + (isDodging * 1.5f)) * new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
        dodgeTimer--;
    }
}
