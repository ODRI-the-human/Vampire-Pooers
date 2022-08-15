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
    int dodgeMarties;
    public float moveSpeed = 5;
    bool playerControlled;

    Vector2 collisionVector;
    float knockBackTimer = 0;
    float maxKnockBack = 0;
    public int dodgeUp = 1;

    public GameObject dodgeAudio;
    public Rigidbody2D rb;
    GameObject Player;

    int isSlowed = 1;
    int slowTimer = 0;
    int slowTimerLength = 100;

    void Start()
    {
        if (gameObject.tag == "Hostile")
        {
            Player = GameObject.Find("newPlayer");
            playerControlled = false;
            if (Player.GetComponent<ItemSTOPWATCH>() != null)
            {
                moveSpeed /= 1 + (0.25f) * Player.GetComponent<ItemSTOPWATCH>().instances;
            }
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
                        //Debug.Log("Dodge the Roll");
                        dodgeTimer = dodgeTimerLength;
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
                if (gameObject.GetComponent<ItemDODGESPLOSION>() != null)
                {
                    gameObject.GetComponent<ItemDODGESPLOSION>().Splosm();
                }
                if (gameObject.GetComponent<HPDamageDie>().iFrames < 7 * dodgeUp)
                {
                    gameObject.GetComponent<HPDamageDie>().iFrames = 7 * (dodgeUp - 1);
                }
            }
        }
    }

    void FixedUpdate()
    {
        moveDirection *= (1 - 0.5f * isSlowed);

        if (knockBackTimer > 0)
        {
            rb.velocity = maxKnockBack * (knockBackTimer / maxKnockBack) * new Vector2(collisionVector.x, collisionVector.y) + ((maxKnockBack - knockBackTimer) / maxKnockBack) * moveSpeed * new Vector2(moveDirection.x, moveDirection.y);
        }
        else
        {
            rb.velocity = (1 + ((0.7f + 0.3f * dodgeUp) * isDodging * 1.5f)) * new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
        }

        if (slowTimer < 1)
        {
            isSlowed = 0;
            gameObject.GetComponent<Attack>().fireTimerLengthMLT = 1;
        }

        dodgeTimer--;
        knockBackTimer--;
        slowTimer--;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag != gameObject.tag)
        {
            if (playerControlled == false)
            {
                collisionVector = 0.5f * new Vector2(transform.position.x - col.transform.position.x, transform.position.y - col.transform.position.y).normalized;
                knockBackTimer = col.gameObject.GetComponent<DealDamage>().knockBackCoeff * 7f;
                maxKnockBack = knockBackTimer;
            }
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.GetComponent<wapantCircle>() != null)
        {
            if (col.gameObject.tag != gameObject.tag)
            {
                isSlowed = 1;
                slowTimer = slowTimerLength;
                gameObject.GetComponent<Attack>().fireTimerLengthMLT = 2;
            }
        }
    }
}
