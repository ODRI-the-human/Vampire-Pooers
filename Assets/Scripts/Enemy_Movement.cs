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
    public GameObject explosion;
    int isSlowed = 0;
    int slowTimer = 0;
    int slowsPlayerHas = 0;
    int dodgeSplosionsPlayerHas = 0;
    int ATGInstances = 0;
    int creepTimer = 0;
    Vector2 collisionVector;
    Vector3 currentNearest;
    int knockBack = 0;
    float knockBackTimer = 0;
    float maxKnockBack = 0;
    public int fireType;
    float currentAngle;
    Vector2 newShotVector;
    Rigidbody2D bulletRB;
    public GameObject enemyShootAudio;
    public GameObject enemyLazerAudio;
    public GameObject chargeLazerAudio;
    public GameObject ATGMissile;
    public float shotSpeed;
    Vector2 fireVector;
    int isShootingLazer = 0;
    float ATGProc;
    float lazerTimer = 0;
    int lazerWarningActive = 0;
    public GameObject lazerWarning;
    public int enemyRange;
    public SpriteRenderer sprite;
    Color originalColor;
    float colorChangeTimer = 0;
    GameObject Player;
    int orbital2PlayerHas;
    int splitsPlayerHas;
    List<int> Sploinky = new List<int>();

    public float moveSpeed = 5f;
    public GameObject Bullet;

    public Rigidbody2D rob;

    public float fireTimerLength = 200f;
    float fireTimer = 0;

    public float HP = 100f;

    void Awake()
    {
        Player = GameObject.Find("Player");
        slowsPlayerHas = Player.GetComponent<Player_Movement>().stopwatchInstances;
        dodgeSplosionsPlayerHas = Player.GetComponent<Player_Movement>().dodgeSplosionInstances;
        orbital2PlayerHas = Player.GetComponent<Player_Movement>().orbital2Instances;
        splitsPlayerHas = Player.GetComponent<Player_Movement>().splitInstances;
        sprite = GetComponent<SpriteRenderer>();
        originalColor = sprite.color;

        if (slowsPlayerHas > 0)
        {
            for (int i = 0; i < slowsPlayerHas; i++)
            {
                moveSpeed *= 0.9f;
                fireTimerLength /= 0.9f;
            }
        }

        Sploinky = Player.GetComponent<Player_Movement>().itemsHeld;
        foreach (int item in Sploinky)
        {
            //Debug.Log(item.ToString());
            switch (item)
            {
                case (int)ITEMLIST.ATG:
                    ATGInstances++;
                    break;
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

        // getting player and enemy pos, getting the vector, and moving towards player
        enemyPos.x = rob.transform.position.x;
        enemyPos.y = rob.transform.position.y;
        Transform playerTrans = Player.transform;
        playerPos.x = playerTrans.position.x; 
        playerPos.y = playerTrans.position.y;
        vectorToPlayer = (playerPos - enemyPos).normalized;

        if (knockBack == 0)
        {
            rob.velocity = (moveSpeed * (2 - isSlowed) / 2) * new Vector2(vectorToPlayer.x, vectorToPlayer.y);
        }

        if (knockBack == 1)
        {
            rob.velocity = maxKnockBack * (knockBackTimer / maxKnockBack) * new Vector2(collisionVector.x, collisionVector.y) + ((maxKnockBack - knockBackTimer) / maxKnockBack) * (moveSpeed * (2 - isSlowed) / 2) * new Vector2(vectorToPlayer.x, vectorToPlayer.y);
        }

        slowTimer--;
        if (slowTimer < 1)
        {
            isSlowed = 0;
        }

        // firing bullets (heavy weapons man)
        if (fireTimer + isSlowed*fireTimerLength < 0)
        {
            if ((playerPos - enemyPos).magnitude < enemyRange) // makes it so enemies only shoot if they're close.
            {
                switch (fireType) // Makes enemy shoot in a particular way depending on their fireType.
                {
                    case 0:
                        //fucking nothing
                        break;
                    case 1:
                        GameObject newObject = Instantiate(Bullet, transform.position, transform.rotation) as GameObject;
                        bulletRB = newObject.GetComponent<Rigidbody2D>();
                        bulletRB.velocity = shotSpeed * new Vector2(vectorToPlayer.x, vectorToPlayer.y);
                        Instantiate(enemyShootAudio);
                        break;
                    case 2:
                        for (int i = 0; i < 8; i++)
                        {
                            GameObject newObject2 = Instantiate(Bullet, transform.position, transform.rotation) as GameObject;
                            bulletRB = newObject2.GetComponent<Rigidbody2D>();
                            currentAngle = (Mathf.PI / 4) * i;
                            bulletRB.velocity = shotSpeed * new Vector2(Mathf.Cos(currentAngle) - Mathf.Sin(currentAngle), Mathf.Sin(currentAngle) + Mathf.Cos(currentAngle));
                        }
                        Instantiate(enemyShootAudio);
                        break;
                    case 3:
                        fireVector = vectorToPlayer;
                        Invoke(nameof(FunnyLazer), 0.7f);
                        lazerWarningActive = 1;
                        Instantiate(chargeLazerAudio);
                        break;
                    case 4:
                        for (int i = -1; i < 3; i++)
                        {
                            GameObject newObject3 = Instantiate(Bullet, transform.position, transform.rotation) as GameObject;
                            bulletRB = newObject3.GetComponent<Rigidbody2D>();
                            currentAngle = 0.6f * (0.5f * 3 - i - 1);
                            newShotVector = new Vector2(vectorToPlayer.x * Mathf.Cos(currentAngle) - vectorToPlayer.y * Mathf.Sin(currentAngle), vectorToPlayer.x * Mathf.Sin(currentAngle) + vectorToPlayer.y * Mathf.Cos(currentAngle));
                            bulletRB.velocity = new Vector2(newShotVector.x * shotSpeed, newShotVector.y * shotSpeed); Instantiate(enemyShootAudio);
                        }
                        Instantiate(enemyShootAudio);
                        break;
                }
            }
            fireTimer = fireTimerLength;
        }


        lazerTimer++;

        if (lazerWarningActive == 1)
        {
            //for (int i = 0; i < 40; i++)
            //{
            GameObject LazerMarty = Instantiate(lazerWarning, fireVector * 19f + enemyPos, Quaternion.Euler(0, 0, (180 / (Mathf.PI)) * Mathf.Atan(fireVector.y / fireVector.x))) as GameObject;
            LazerMarty.transform.localScale = new Vector3(60, 1, 1);
            //}
        }

        if (isShootingLazer == 1)
        {
            //for (int i = 0; i < 6; i++)
            //{
            GameObject LazerMoment = Instantiate(Bullet, fireVector * 19f + enemyPos, Quaternion.Euler(0, 0, (180 / (Mathf.PI)) * Mathf.Atan(fireVector.y / fireVector.x))) as GameObject;
            LazerMoment.transform.localScale = new Vector3(60, 1 / lazerTimer, 1);
            //}
        }

        if (lazerTimer > 9)
        {
            isShootingLazer = 0;
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
                Instantiate(XP, transform.position, transform.rotation);
            }
            Destroy(gameObject);
        }

        colorChangeTimer--;

        if (colorChangeTimer == 0)
        {
            sprite.color = originalColor;
        }    
    }

    void FunnyLazer()
    {
        Instantiate(enemyLazerAudio);
        isShootingLazer = 1;
        lazerTimer = 0;
        lazerWarningActive = 0;
    }

    void ATG()
    {
        if (ATGInstances > 0)
        {
            ATGProc = Random.Range(0, 10);
            if (ATGProc > (8 - 0.5 * ATGInstances))
            {
                Instantiate(ATGMissile, Player.transform.position, new Quaternion(1, 0, 0, 0));
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "PlayerBullet")
        {
            HP -= Player.GetComponent<Player_Movement>().trueDamageValue;
            collisionVector = 0.5f*new Vector2(transform.position.x - col.transform.position.x, transform.position.y - col.transform.position.y).normalized;
            knockBack = 1;
            knockBackTimer = 7f;
            maxKnockBack = knockBackTimer;
            sprite.color = Color.red;
            colorChangeTimer = 3;
            ATG();
        }

        if (col.gameObject.tag == "playerBulletSplit")
        {
            HP -= 0.5f * splitsPlayerHas * Player.GetComponent<Player_Movement>().trueDamageValue;
            collisionVector = 0.5f * new Vector2(transform.position.x - col.transform.position.x, transform.position.y - col.transform.position.y).normalized;
            knockBack = 1;
            knockBackTimer = 3f;
            maxKnockBack = knockBackTimer;
            sprite.color = Color.red;
            colorChangeTimer = 3;
            ATG();
        }

        if (col.gameObject.tag == "Orbital Bullet")
        {
            HP -= 0.25f * orbital2PlayerHas * Player.GetComponent<Player_Movement>().trueDamageValue;
            collisionVector = 0.5f * new Vector2(transform.position.x - col.transform.position.x, transform.position.y - col.transform.position.y).normalized;
            knockBack = 1;
            knockBackTimer = 5f;
            maxKnockBack = knockBackTimer;
            sprite.color = Color.red;
            colorChangeTimer = 3;
            ATG();
        }

        if (col.gameObject.tag == "ATGExplosion")
        {
            HP -= Player.GetComponent<Player_Movement>().trueDamageValue;
            collisionVector = 0.75f * new Vector2(transform.position.x - col.transform.position.x, transform.position.y - col.transform.position.y).normalized;
            knockBack = 1;
            knockBackTimer = 8;
            maxKnockBack = knockBackTimer;
            sprite.color = Color.red;
            colorChangeTimer = 3;
        }

        if (col.gameObject.tag == "dodgeExplosion")
        {
            HP -= 20+20*dodgeSplosionsPlayerHas;
            collisionVector = new Vector2(transform.position.x - col.transform.position.x, transform.position.y - col.transform.position.y).normalized;
            knockBack = 1;
            knockBackTimer = 15;
            maxKnockBack = knockBackTimer;
            sprite.color = Color.red;
            colorChangeTimer = 3;
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
                HP -= 0.08f * Player.GetComponent<Player_Movement>().trueDamageValue;
                creepTimer = 5;
                colorChangeTimer = 3;
            }
        }

        if (col.gameObject.tag == "OrbitalContact")
        {
            HP -= 6;
            sprite.color = Color.red;
            colorChangeTimer = 3;
        }
    }
}
