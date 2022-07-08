using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Movement : MonoBehaviour
{
    Vector2 vectorToPlayer;
    Vector2 playerPos;
    Vector2 enemyPos;
    public GameObject enemyShootAudio;
    private int Timer = 0;

    public float moveSpeed = 5f;

    public Rigidbody2D rb;

    void Start()
    {
        Instantiate(enemyShootAudio);
        enemyPos.x = rb.transform.position.x;
        enemyPos.y = rb.transform.position.y;
        playerPos.x = GameObject.Find("Player").transform.position.x;
        playerPos.y = GameObject.Find("Player").transform.position.y;
        vectorToPlayer = (playerPos - enemyPos).normalized;
        rb.velocity = new Vector2(vectorToPlayer.x * moveSpeed, vectorToPlayer.y * moveSpeed);
    }

    void Update()
    {
        Timer += 1;
        if (Timer > 1500)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Destroy(gameObject);
    }

}
