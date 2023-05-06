using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
using UnityEngine.InputSystem;

public class NewPlayerMovement : MonoBehaviour
{
    public Vector2 knockBackVector = new Vector2(0, 0);
    public Vector3 desiredVector;

    public Vector3 moveDirection;

    public float baseMoveSpeed;
    [HideInInspector] public float currentMoveSpeed;
    public float speedDiv = 1;
    public float speedMult = 1; // used for dodging and certain enemies' abilities.
    public int slowTimer = 0;
    public int isSlowed = 0;
    public bool moveTowardsPlayer = true;

    public bool recievesKnockback = true;

    [HideInInspector] public int dodgeUp = 1;
    int isDodging = 0;
    int dodgeTimer = -30;
    public int dodgeTimerLength = 20;
    public float dodgeSpeedUp = 2;
    int dodgeMode = 1;
    public int mouseAltMode = 1;

    int LayerPlayer;
    int LayerNone;
    int LayerSB;

    public GameObject dodgeOnlineAudio;
    public GameObject dodgeAudio;
    Rigidbody2D rb;

    public InputActionAsset actions;
    public InputAction moveAction;

    void Start()
    {
        moveAction = actions.FindActionMap("gameplay").FindAction("move");
        actions.FindActionMap("gameplay").FindAction("dodge").performed += OnDodge;

        rb = gameObject.GetComponent<Rigidbody2D>();
        LayerPlayer = LayerMask.NameToLayer("Player");
        LayerNone = LayerMask.NameToLayer("OnlyHitWalls");
        LayerSB = LayerMask.NameToLayer("OnlyHitWallsAndEnemies");
    }

    void OnDodge(InputAction.CallbackContext context) // Applying the dodge when the player, ya know, dodges.
    {
        if (dodgeTimer < -50)
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
                    gameObject.GetComponent<DealDamage>().massCoeff += 7.5f;
                    speedMult = 1.5f * dodgeSpeedUp;
                    gameObject.layer = LayerSB;
                    isDodging = 1;
                    break;
            }
        }
    }

    void Update()
    {
        rb.velocity = new Vector2(0, 0);
        transform.position += Time.deltaTime * new Vector3(knockBackVector.x, knockBackVector.y, 0);

        currentMoveSpeed = baseMoveSpeed * speedMult / speedDiv;

        if (gameObject.tag == "Player")
        {
            if (isDodging == 0)
            {
                moveDirection = moveAction.ReadValue<Vector2>().normalized;//new Vector2(moveX, moveY).normalized;
            }

            desiredVector = moveDirection;
            Vector3 moveDir = currentMoveSpeed * desiredVector * Time.deltaTime;
            transform.position += moveDir;
        }
        else
        {
            if (moveTowardsPlayer)
            {
                desiredVector = gameObject.GetComponent<Attack>().currentTarget.transform.position - transform.position;
            }
            transform.rotation = Quaternion.Euler(0, 0, 0);

            if (!gameObject.GetComponent<NavMeshAgent>().enabled)
            {
                desiredVector = moveDirection;
                Vector3 moveDir = currentMoveSpeed * desiredVector * Time.deltaTime;
                transform.position += moveDir;
            }
        }

        //if (Input.GetButtonDown("ChangeDodgeMode"))
        //{
        //    if (dodgeMode == 1)
        //    {
        //        dodgeMode = 0;
        //    }
        //    else
        //    {
        //        dodgeMode = 1;
        //    }
        //}
    }

    void FixedUpdate()
    {
        // Resetting slow and dodge, and causing all dodge end effects..
        if (dodgeTimer == -50 && dodgeOnlineAudio != null)
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
                    SendMessage("DodgeEndEffects");
                    break;
                case 1:
                    gameObject.layer = LayerPlayer;
                    isDodging = 0;
                    speedMult = 1;
                    gameObject.GetComponent<HPDamageDie>().damageReduction -= 500;
                    gameObject.GetComponent<DealDamage>().massCoeff -= 7.5f;
                    SendMessage("DodgeEndEffects");
                    gameObject.GetComponent<HPDamageDie>().iFrames = 10;
                    break;
            }    
        }

        if (slowTimer == 0)
        {
            isSlowed = 0;
            speedDiv = 1;
        }

        knockBackVector /= 1.15f;
        if (knockBackVector.magnitude < 0.1f)
        {
            knockBackVector *= 0;
        }

        //if (dodgeTimer % 50 == 0)
        //{
        //    SendMessage("DodgeEndEffects");
        //}

        slowTimer--;
        dodgeTimer--;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if ((col.gameObject.tag == "Hostile" || col.gameObject.tag == "enemyBullet" || col.gameObject.tag == "Player" || col.gameObject.tag == "PlayerBullet") && col.gameObject.tag != gameObject.tag && recievesKnockback)
        {
            knockBackVector = 10 * col.gameObject.GetComponent<DealDamage>().massCoeff * new Vector2(transform.position.x - col.gameObject.transform.position.x, transform.position.y - col.gameObject.transform.position.y).normalized;

            if (isDodging == 1 && mouseAltMode == 1)
            {
                col.gameObject.AddComponent<hitIfKBVecHigh>();
                GameObject master = EntityReferencerGuy.Instance.master;
                master.GetComponent<visualPoopoo>().bigHitFreeze(0.1f);
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
}
