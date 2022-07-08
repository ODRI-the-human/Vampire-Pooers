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
    GameObject Player;

    public float moveSpeed = 5f;
    public GameObject Bullet;

    public Rigidbody2D rob;

    public int fireTimerLength = 200;
    int fireTimer = 0;

    public float HP = 100f;

    void FixedUpdate()
    {
        // weapon cooldown
        fireTimer -= 1;

        // getting player and enemy pos, getting the vector, and moving towards player
        enemyPos.x = rob.transform.position.x;
        enemyPos.y = rob.transform.position.y;
        playerPos.x = GameObject.Find("Player").transform.position.x;
        playerPos.y = GameObject.Find("Player").transform.position.y;
        vectorToPlayer = (playerPos - enemyPos).normalized;
        rob.velocity = new Vector2(vectorToPlayer.x * moveSpeed, vectorToPlayer.y * moveSpeed);

        // firing bullets (heavy weapons man)
        if (canFireBullets == 1) // makes it so enemies only fire if it's been set to 1.
        {
            if (fireTimer < 0)
            {
                if ((playerPos - enemyPos).magnitude < 4) // makes it so enemies only shoot if they're close.
                {
                    Instantiate(Bullet, transform.position, transform.rotation);
                    fireTimer = fireTimerLength;
                }
            }
        }

        // >dies
        if (HP <= 0)
        {
            Instantiate(deathAudio);
            int doesSpawnHP = Random.Range(0, 15);
            Debug.Log(doesSpawnHP);
            if (doesSpawnHP > 13)
            {
                Instantiate(HPDrop, transform.position, transform.rotation);
            }
            else
            {
                Instantiate(XP, transform.position, transform.rotation);
            }
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "PlayerBullet")
        {
            HP -= ((col.transform.localScale.x - 0.2f)*5*50);
        }

        if (col.gameObject.tag == "ATGExplosion")
        {
            HP -= 15*FindObjectOfType<Player_Movement>().damageStat;
        }
    }
}
