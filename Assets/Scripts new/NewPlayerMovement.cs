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
    float moveSpeed = 5;

    public GameObject dodgeAudio;
    public Rigidbody2D rb;

    void Update()
    {
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

        if (Input.GetAxisRaw("Horizontal") < -0.001)
        {
            gameObject.transform.localScale = new Vector3(1f, 1f, 1);
        }
        else
        {
            if (Input.GetAxisRaw("Horizontal") > 0.001)
            {
                gameObject.transform.localScale = new Vector3(-1f, 1f, 1);
            }
        }
    }

    void FixedUpdate()
    {
        rb.velocity = (1 + (isDodging * 1.5f)) * new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
        dodgeTimer--;
    }
}
