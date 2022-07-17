using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Movement : MonoBehaviour
{

    Vector2 vectorToPlayer;
    Vector2 playerPos;
    Vector2 enemyPos;
    public GameObject deathAudio;
    public GameObject XP;
    public GameObject HPDrop;
    public int canFireBullets = 1;
    public GameObject explosion;
    int isSlowed = 0;
    int slowTimer = 0;
    int slowsPlayerHas = 0;
    int creepTimer = 0;
    Vector2 collisionVector;
    int knockBack = 0;
    float knockBackTimer = 0;
    float maxKnockBack = 0;

    public float moveSpeed = 5f;
    public GameObject Bullet;

    public Rigidbody2D rob;

    public float fireTimerLength = 200f;
    float fireTimer = 0;

    float HP = 100f;

    void Awake()
    {
        slowsPlayerHas = GameObject.Find("Player").GetComponent<Player_Movement>().stopwatchInstances;

        if (slowsPlayerHas > 0)
        {
            for (int i = 0; i < slowsPlayerHas; i++)
            {
                moveSpeed *= 0.9f;
                fireTimerLength /= 0.9f;
            }
        }
    }

    void FixedUpdate()
    {
        // weapon cooldown
        fireTimer -= 1;
        knockBackTimer--;

        if (knockBackTimer < 1)
        {
            knockBack = 0;
        }

        enemyPos.x = rob.transform.position.x;
        enemyPos.y = rob.transform.position.y;
        playerPos.x = GameObject.Find("Player").transform.position.x;
        playerPos.y = GameObject.Find("Player").transform.position.y;
        vectorToPlayer = (playerPos - enemyPos).normalized;

        if (knockBack == 0)
        {
            rob.velocity = (moveSpeed * (2 - isSlowed) / 2) * new Vector2(vectorToPlayer.x, vectorToPlayer.y);
        }
        // getting player and enemy pos, getting the vector, and moving towards player

        if (knockBack == 1)
        {
            rob.velocity = 6*(knockBackTimer/maxKnockBack) * new Vector2(collisionVector.x, collisionVector.y) + ((maxKnockBack - knockBackTimer)/maxKnockBack)*(moveSpeed * (2 - isSlowed) / 2) * new Vector2(vectorToPlayer.x, vectorToPlayer.y);
        }

        slowTimer--;
        if (slowTimer < 1)
        {
            isSlowed = 0;
        }

        // firing bullets (heavy weapons man)
        if (canFireBullets == 1) // makes it so enemies only fire if it's been set to 1.
        {
            if (fireTimer + isSlowed*fireTimerLength < 0)
            {
                if ((playerPos - enemyPos).magnitude < 4) // makes it so enemies only shoot if they're close.
                {
                    Instantiate(Bullet, transform.position, transform.rotation);
                    fireTimer = fireTimerLength;
                }
            }
        }

        creepTimer--;

        // >dies
        if (HP <= 0)
        {
            Instantiate(deathAudio);
            int doesSpawnHP = Random.Range(0, 15);
            //Debug.Log(doesSpawnHP);
            if (doesSpawnHP > 13)
            {
                Instantiate(HPDrop, transform.position, transform.rotation);
            }
            else
            {
                //Instantiate(XP, transform.position, transform.rotation);
            }
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "PlayerBullet")
        {
            HP -= GameObject.Find("Player").GetComponent<Player_Movement>().trueDamageValue;
            collisionVector = new Vector2(transform.position.x - col.transform.position.x, transform.position.y - col.transform.position.y);
            knockBack = 1;
            knockBackTimer = 20f*col.transform.localScale.x;
            maxKnockBack = knockBackTimer;
            //Debug.Log(GameObject.Find("Player").GetComponent<Player_Movement>().HP.ToString());
        }

        if (col.gameObject.tag == "ATGExplosion")
        {
            HP -= GameObject.Find("Player").GetComponent<Player_Movement>().trueDamageValue;
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Slowing")
        {
            isSlowed = 1;
            slowTimer = 200;
        }

        if (col.gameObject.tag == "Creep")
        {
            if (creepTimer < 1)
            {
                HP -= 0.08f * GameObject.Find("Player").GetComponent<Player_Movement>().trueDamageValue;
                creepTimer = 5;
            }
        }
    }
}
