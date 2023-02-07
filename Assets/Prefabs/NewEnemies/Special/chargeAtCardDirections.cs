using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class chargeAtCardDirections : MonoBehaviour
{
    float roundAmount = 1;

    bool isCharging = false;

    int timer = 0;

    // Update is called once per frame
    void Update()
    {
        GameObject target = gameObject.GetComponent<Attack>().currentTarget;

        float distance = (target.transform.position - transform.position).magnitude;

        float targetX = Mathf.Round(target.transform.position.x / roundAmount) * roundAmount;
        float targetY = Mathf.Round(target.transform.position.y / roundAmount) * roundAmount;
        float selfX = Mathf.Round(transform.position.x / roundAmount) * roundAmount;
        float selfY = Mathf.Round(transform.position.y / roundAmount) * roundAmount;

        if (!isCharging && timer > 50 && distance < 9)
        {
            if (targetX == selfX)
            {
                if (targetY < selfY)
                {
                    gameObject.GetComponent<NewPlayerMovement>().moveTowardsPlayer = false;
                    gameObject.GetComponent<NewPlayerMovement>().moveDirection = new Vector2(0, -1);
                    isCharging = true;
                }
                else
                {
                    gameObject.GetComponent<NewPlayerMovement>().moveTowardsPlayer = false;
                    gameObject.GetComponent<NewPlayerMovement>().moveDirection = new Vector2(0, 1);
                    isCharging = true;
                }

                timer = 0;
            }

            if (targetY == selfY)
            {
                if (targetX < selfX)
                {
                    gameObject.GetComponent<NewPlayerMovement>().moveTowardsPlayer = false;
                    gameObject.GetComponent<NewPlayerMovement>().moveDirection = new Vector2(-1, 0);
                    isCharging = true;
                }
                else
                {
                    gameObject.GetComponent<NewPlayerMovement>().moveTowardsPlayer = false;
                    gameObject.GetComponent<NewPlayerMovement>().moveDirection = new Vector2(1, 0);
                    isCharging = true;
                }

                timer = 0;
            }
        }

        if (isCharging)
        {
            gameObject.GetComponent<NewPlayerMovement>().speedMult += 10 * Time.deltaTime;
            gameObject.GetComponent<NewPlayerMovement>().speedMult = Mathf.Clamp(gameObject.GetComponent<NewPlayerMovement>().speedMult, 0, 7);
            gameObject.GetComponent<DealDamage>().massCoeff = gameObject.GetComponent<NewPlayerMovement>().speedMult;
            gameObject.GetComponent<NavMeshAgent>().enabled = false;
        }
    }

    void FixedUpdate()
    {
        if (!isCharging)
        {
            timer++;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        gameObject.GetComponent<NewPlayerMovement>().moveTowardsPlayer = true;
        isCharging = false;
        gameObject.GetComponent<NewPlayerMovement>().speedMult = 1;
        gameObject.GetComponent<NavMeshAgent>().enabled = true;
        gameObject.GetComponent<DealDamage>().massCoeff = 2;

        if (col.gameObject.tag == "Player")
        {
            col.gameObject.AddComponent<hitIfKBVecHigh>();
            col.gameObject.GetComponent<hitIfKBVecHigh>().responsible = gameObject;
        }
    }
}
