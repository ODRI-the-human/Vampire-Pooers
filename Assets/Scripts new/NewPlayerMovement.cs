using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class NewPlayerMovement : MonoBehaviour
{
    public Vector2 knockBackVector = new Vector2(0, 0);
    public Vector3 desiredVector;

    Vector3 moveDirection;

    public float baseMoveSpeed;
    [HideInInspector] public float currentMoveSpeed;
    float speedDiv = 1;
    float speedMult = 1;
    int slowTimer;
    public int isSlowed = 0;

    [HideInInspector] public int dodgeUp = 1;
    int isDodging = 0;
    int dodgeTimer = -30;
    public int dodgeTimerLength = 20;
    public float dodgeSpeedUp = 2;
    int dodgeMode = 1;
    public int mouseAltMode = 1;

    int LayerPlayer;
    int LayerNone;

    public GameObject dodgeOnlineAudio;
    public GameObject dodgeAudio;
    Rigidbody2D rb;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        LayerPlayer = LayerMask.NameToLayer("Player");
        LayerNone = LayerMask.NameToLayer("OnlyHitWalls");
    }

    void Update()
    {
        rb.velocity = new Vector2(0, 0);
        transform.position += Time.deltaTime * new Vector3(knockBackVector.x, knockBackVector.y, 0);

        currentMoveSpeed = baseMoveSpeed * speedMult / speedDiv;

        if (gameObject.tag == "Player")
        {
            if (Input.GetButtonDown("Dodge") && dodgeTimer < -50)
            {
                switch (mouseAltMode)
                {
                    case 0:
                        if (dodgeMode == 0)
                        {
                            Vector2 mouseVector = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
                            moveDirection = new Vector3(mouseVector.x - transform.position.x, mouseVector.y - transform.position.y, 0).normalized;
                        }

                        Instantiate(dodgeAudio);
                        dodgeTimer = dodgeTimerLength;
                        gameObject.layer = LayerNone;
                        speedMult = dodgeSpeedUp;
                        isDodging = 1;
                        break;
                    case 1:
                        if (dodgeMode == 0)
                        {
                            Vector2 mouseVector = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
                            moveDirection = new Vector3(mouseVector.x - transform.position.x, mouseVector.y - transform.position.y, 0).normalized;
                        }

                        Instantiate(dodgeAudio);
                        dodgeTimer = dodgeTimerLength / 2;
                        gameObject.GetComponent<HPDamageDie>().damageReduction += 500;
                        gameObject.GetComponent<DealDamage>().massCoeff += 3;
                        speedMult = 1.5f * dodgeSpeedUp;
                        isDodging = 1;
                        break;
                }
            }

            if (isDodging == 0)
            {
                float moveX = Input.GetAxisRaw("Horizontal");
                float moveY = Input.GetAxisRaw("Vertical");
                moveDirection = new Vector2(moveX, moveY).normalized;
            }

            desiredVector = moveDirection;
            Vector3 moveDir = currentMoveSpeed * desiredVector * Time.deltaTime;
            Debug.Log(desiredVector.ToString());
            transform.position += moveDir;


        }
        else
        {
            desiredVector = gameObject.GetComponent<Attack>().currentTarget.transform.position - transform.position;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        if (Input.GetButtonDown("ChangeDodgeMode"))
        {
            if (dodgeMode == 1)
            {
                dodgeMode = 0;
            }
            else
            {
                dodgeMode = 1;
            }
        }
    }

    void FixedUpdate()
    {
        slowTimer--;
        dodgeTimer--;

        // Resetting slow and dodge, and causing all dodge end effects..
        if (dodgeTimer == -50)
        {
            Instantiate(dodgeOnlineAudio);
        }

        if (dodgeTimer == 0)
        {
            switch (mouseAltMode)
            {
                case 0:
                    gameObject.layer = LayerPlayer;
                    isDodging = 0;
                    speedMult = 1;
                    SendMessage("dodgeEndEffects");
                    break;
                case 1:
                    isDodging = 0;
                    speedMult = 1;
                    gameObject.GetComponent<HPDamageDie>().damageReduction -= 500;
                    gameObject.GetComponent<DealDamage>().massCoeff -= 3;
                    SendMessage("dodgeEndEffects");
                    break;
            }    
        }

        if (slowTimer < 0)
        {
            isSlowed = 0;
            speedDiv = 1;
        }

        knockBackVector /= 1.15f;
        if (knockBackVector.magnitude < 0.1f)
        {
            knockBackVector *= 0;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if ((col.gameObject.tag == "Hostile" || col.gameObject.tag == "enemyBullet" || col.gameObject.tag == "Player" || col.gameObject.tag == "PlayerBullet") && col.gameObject.tag != gameObject.tag)
        {
            knockBackVector = 10 * col.gameObject.GetComponent<DealDamage>().massCoeff * new Vector2(transform.position.x - col.gameObject.transform.position.x, transform.position.y - col.gameObject.transform.position.y).normalized;

            if (isDodging == 1 && mouseAltMode == 1)
            {
                col.gameObject.AddComponent<hitIfKBVecHigh>();
            }
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.GetComponent<wapantCircle>() != null)
        {
            isSlowed = 1;
            slowTimer = 100;
            speedDiv = 2;
        }
    }

    //private Vector2 moveDirection;
    //float dodgeTimer = -9999;
    //public float dodgeTimerLength = 15;
    //int isDodging = 0;
    //int dodgeMarties;
    //public float moveSpeed = 5;
    //bool playerControlled;

    //Vector2 collisionVector;
    //float knockBackTimer = 0;
    //float maxKnockBack = 0;
    //public int dodgeUp = 1;

    //public GameObject dodgeAudio;
    //public Rigidbody2D rb;
    //GameObject Player;

    //int dodgeMode = 1;

    //public int isSlowed = 0;
    //int slowTimer = -5;
    //int slowTimerLength = 100;

    //int timer = 0;

    //public int dodgeTimerOnline = 30; // the time for the dodge to come back online.

    //public GameObject dodgeOnlineAudio;

    //int LayerPlayer;
    //int LayerNone;
    //public NavMeshAgent agent;

    //void Start()
    //{
    //    LayerPlayer = LayerMask.NameToLayer("Player");
    //    LayerNone = LayerMask.NameToLayer("OnlyHitWalls");

    //    if (gameObject.tag == "Hostile")
    //    {
    //        Player = GameObject.Find("newPlayer");
    //        playerControlled = false;
    //        agent.speed = 1.5f * moveSpeed;
    //        agent.acceleration = 1000;
    //        agent.angularSpeed = 10000;
    //        agent.stoppingDistance = 0;
    //        agent.autoBraking = false;
    //        if (Player.GetComponent<ItemSTOPWATCH>() != null)
    //        {
    //            moveSpeed /= 1 + (0.25f) * Player.GetComponent<ItemSTOPWATCH>().instances;
    //        }
    //    }
    //    else
    //    {
    //        playerControlled = true;
    //    }

    //    isSlowed = 0;
    //    gameObject.GetComponent<Attack>().fireTimerLengthMLT = 1;
    //}

    //void Update()
    //{
    //    if (dodgeTimer == -dodgeTimerOnline)
    //    {
    //        Instantiate(dodgeOnlineAudio);
    //    }


    //    if (Input.GetButtonDown("ChangeDodgeMode"))
    //    {
    //        if (dodgeMode == 1)
    //        {
    //            dodgeMode = 0;
    //        }
    //        else
    //        {
    //            dodgeMode = 1;
    //        }
    //    }

    //    switch (playerControlled)
    //    {
    //        case true:
    //            if (isDodging == 0)
    //            {
    //                float moveX = Input.GetAxisRaw("Horizontal");
    //                float moveY = Input.GetAxisRaw("Vertical");
    //                moveDirection = new Vector2(moveX, moveY).normalized;
    //            }

    //            if (dodgeTimer < -dodgeTimerOnline)
    //            {
    //                if (Input.GetButton("Dodge"))
    //                {
    //                    //Debug.Log("Dodge the Roll");
    //                    dodgeTimer = dodgeTimerLength;
    //                    //iFrames = dodgeTimerLength;
    //                    isDodging = 1;
    //                    Instantiate(dodgeAudio);
    //                    if (dodgeMode == 0)
    //                    {
    //                        moveDirection = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x - gameObject.transform.position.x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y - gameObject.transform.position.y).normalized;
    //                    }
    //                }
    //            }

    //            if (moveDirection.x > 0)
    //            {
    //                gameObject.transform.localScale = new Vector3(1f, 1f, 1);
    //            }
    //            else
    //            {
    //                if (moveDirection.x < 0)
    //                {
    //                    gameObject.transform.localScale = new Vector3(-1f, 1f, 1);
    //                }
    //            }

    //            break;
    //        case false:
    //            moveDirection = Player.transform.position - transform.position;
    //            break;
    //    }

    //    if (isDodging == 1)
    //    {
    //        if (dodgeTimer > 0)
    //        {
    //            //gameObject.GetComponent<Collider2D>().enabled = false;
    //            gameObject.layer = LayerNone;
    //        }
    //        else
    //        {
    //            isDodging = 0;
    //            gameObject.layer = LayerPlayer;
    //            //gameObject.GetComponent<Collider2D>().enabled = true;
    //            if (gameObject.GetComponent<ItemDODGESPLOSION>() != null)
    //            {
    //                gameObject.GetComponent<ItemDODGESPLOSION>().Splosm();
    //            }
    //            if (gameObject.GetComponent<HPDamageDie>().iFrames < 7 * dodgeUp)
    //            {
    //                gameObject.GetComponent<HPDamageDie>().iFrames = 7 * (dodgeUp - 1);
    //            }
    //        }
    //    }

    //    moveDirection *= (1 - 0.5f * isSlowed);

    //    Vector2 velocity;

    //    if (knockBackTimer > 0)
    //    {
    //        velocity = maxKnockBack * (knockBackTimer / maxKnockBack) * new Vector2(collisionVector.x, collisionVector.y) + ((maxKnockBack - knockBackTimer) / maxKnockBack) * moveSpeed * new Vector2(moveDirection.x, moveDirection.y);
    //    }
    //    else
    //    {
    //        velocity = (1 + ((0.7f + 0.3f * dodgeUp) * isDodging * 1.5f)) * new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    //    }

    //    switch (playerControlled)
    //    {
    //        case true:
    //            //rb.velocity = velocity;
    //            Vector3 posso = velocity * Time.deltaTime;
    //            transform.position += posso;
    //            break;
    //        case false:
    //            if (gameObject.GetComponent<spinnerSpin>() == null)
    //            {
    //                transform.rotation = Quaternion.Euler(0, 0, 0);
    //            }
    //            else
    //            {
    //                transform.rotation = Quaternion.Euler(0, 0, timer);
    //            }

    //            rb.velocity = new Vector3(0, 0, 0);
    //            Ray ray = new Ray(transform.position, new Vector3(velocity.x, velocity.y, Player.transform.position.z - transform.position.z));
    //            RaycastHit hit;

    //            if (Physics.Raycast(ray, out hit))
    //            {
    //                agent.SetDestination(hit.point);
    //            }
    //            break;
    //    }

    //    if (slowTimer == 0)
    //    {
    //        isSlowed = 0;
    //        gameObject.GetComponent<Attack>().fireTimerLengthMLT = 1;
    //    }
    //}

    //void FixedUpdate()
    //{
    //    timer++;
    //    dodgeTimer--;
    //    knockBackTimer--;
    //    slowTimer--;
    //}

    //void OnCollisionEnter2D(Collision2D col)
    //{
    //    if (col.gameObject.tag != gameObject.tag)
    //    {
    //        if (playerControlled == false)
    //        {
    //            collisionVector = 0.5f * new Vector2(transform.position.x - col.transform.position.x, transform.position.y - col.transform.position.y).normalized;
    //            knockBackTimer = col.gameObject.GetComponent<DealDamage>().knockBackCoeff * 7f;
    //        }
    //        else
    //        {
    //            collisionVector = 0.2f * new Vector2(transform.position.x - col.transform.position.x, transform.position.y - col.transform.position.y).normalized;
    //            knockBackTimer = col.gameObject.GetComponent<DealDamage>().knockBackCoeff * 30f;
    //        }
    //        maxKnockBack = knockBackTimer;
    //    }
    //}

    //void OnTriggerStay2D(Collider2D col)
    //{
    //    if (col.gameObject.tag != gameObject.tag && col.gameObject.tag == "ATGExplosion")
    //    {
    //        if (playerControlled == false)
    //        {
    //            collisionVector = 0.5f * new Vector2(transform.position.x - col.transform.position.x, transform.position.y - col.transform.position.y).normalized;
    //            knockBackTimer = col.gameObject.GetComponent<DealDamage>().knockBackCoeff * 7f;
    //            maxKnockBack = knockBackTimer;
    //        }
    //    }


    //    if (col.GetComponent<wapantCircle>() != null)
    //    {
    //        if (col.gameObject.tag != gameObject.tag && isSlowed != 1)
    //        {
    //            isSlowed = 1;
    //            slowTimer = slowTimerLength;
    //            gameObject.GetComponent<Attack>().fireTimerLengthMLT = 2;
    //            if (!gameObject.GetComponent<Statuses>().iconOrder.Contains(3))
    //            {
    //                gameObject.GetComponent<Statuses>().iconOrder.Add(3);
    //            }
    //        }
    //    }
    //}
}
