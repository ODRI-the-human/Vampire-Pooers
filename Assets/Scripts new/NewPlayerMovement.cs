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
    public int isDodging = 0;
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

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        LayerPlayer = LayerMask.NameToLayer("Player");
        LayerNone = LayerMask.NameToLayer("OnlyHitWalls");
        LayerSB = LayerMask.NameToLayer("OnlyHitWallsAndEnemies");
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (gameObject.GetComponent<PlayerInput>().currentControlScheme == "keyboard")
        {
            desiredVector = context.ReadValue<Vector2>().normalized;//new Vector2(moveX, moveY).normalized;
        }
        else
        {
            desiredVector = context.ReadValue<Vector2>();
        }
    }

    public void OnDodge(InputAction.CallbackContext context) // Applying the dodge when the player, ya know, dodges.
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
                    speedMult *= dodgeSpeedUp;
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
                    speedMult *= 1.5f * dodgeSpeedUp;
                    gameObject.layer = LayerSB;
                    isDodging = 1;
                    break;
            }
        }
    }

    //public void AttackStatus(bool didStart)
    //{
    //    if (didStart)
    //    {
    //        speedMult *= 0.65f;
    //        //Debug.Log("slowed");
    //    }
    //    else
    //    {
    //        speedMult /= 0.65f;
    //        //Debug.Log("speeded");
    //    }
    //}

    void Update()
    {
        rb.velocity = new Vector2(0, 0);
        transform.position += Time.deltaTime * new Vector3(knockBackVector.x, knockBackVector.y, 0);

        //switch (gameObject.GetComponent<Attack>().isFiring)
        //{
        //    case true:
        //        currentMoveSpeed = 0.7f * baseMoveSpeed * speedMult / speedDiv;
        //        break;
        //    case false:
                currentMoveSpeed = baseMoveSpeed * speedMult / speedDiv;
        //        break;
        //}

        if (gameObject.tag == "Player")
        {
            float speed = currentMoveSpeed;
            if (isDodging == 0)
            {
                moveDirection = desiredVector;
            }

            if (gameObject.GetComponent<Attack>().isAttacking)
            {
                speed *= 0.75f;
            }

            Vector3 moveDir = speed * moveDirection * Time.deltaTime;
            transform.position += moveDir;
        }
        else
        {
            if (moveTowardsPlayer && gameObject.GetComponent<Attack>().currentTarget != null)
            {
                desiredVector = gameObject.GetComponent<Attack>().currentTarget.transform.position - transform.position;
            }
            transform.rotation = Quaternion.Euler(0, 0, 0);

            if (!gameObject.GetComponent<NavMeshAgent>().enabled)
            {
                desiredVector = moveDirection;
                Vector3 moveDir = currentMoveSpeed * desiredVector * Time.deltaTime / EntityReferencerGuy.Instance.stopWatchDebuffAmt;
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

    public void DodgeEndEffects()
    {
        // stop
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
                    speedMult /= dodgeSpeedUp;
                    SendMessage("DodgeEndEffects");
                    break;
                case 1:
                    gameObject.layer = LayerPlayer;
                    isDodging = 0;
                    speedMult /= 1.5f * dodgeSpeedUp;
                    gameObject.GetComponent<HPDamageDie>().damageReduction -= 500;
                    gameObject.GetComponent<DealDamage>().massCoeff -= 7.5f;
                    SendMessage("DodgeEndEffects");
                    gameObject.GetComponent<HPDamageDie>().iFrames = 10;
                    break;
            }

            gameObject.GetComponent<ItemHolder2>().OnDodgeEnds();
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
        ApplyKnockBack(col.gameObject);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        ApplyKnockBack(collider.gameObject);
    }

    void ApplyKnockBack(GameObject col)
    {
        if (col.GetComponent<DealDamage>() != null && col.tag != gameObject.tag && recievesKnockback)
        {
            if (gameObject.GetComponent<HPDamageDie>().iFrames <= 0 && col.GetComponent<DealDamage>().massCoeff > 0)
            {
                GameObject referenceObj;

                if (col.gameObject.GetComponent<meleeGeneral>() != null) // if the col object is a melee weapon, the ref obj is the owner! else it's the collision obj itself.
                {
                    referenceObj = col.GetComponent<DealDamage>().owner;
                }
                else
                {
                    referenceObj = col;
                }

                knockBackVector += 10 * col.GetComponent<DealDamage>().massCoeff * new Vector2(transform.position.x - referenceObj.transform.position.x, transform.position.y - referenceObj.transform.position.y).normalized;
                Debug.Log("knockback moment " + col.ToString());
                //Debug.Log("knockbackvector magnitude: " + knockBackVector.magnitude.ToString() + " / mass coeff: " + col.GetComponent<DealDamage>().massCoeff.ToString());
            }
        }
    }
}
