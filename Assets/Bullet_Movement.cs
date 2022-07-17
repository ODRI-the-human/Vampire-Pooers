using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Movement : MonoBehaviour
{
    Vector2 vectorToPlayer;
    Vector2 playerPos;
    Vector2 enemyPos;
    public GameObject enemyShootAudio;
    int slowsPlayerHas = 0;

    public float moveSpeed = 5f;
    public float destroyDelay = 25; //in seconds

    public Rigidbody2D rb;

    void Start()
    {
        slowsPlayerHas = GameObject.Find("Player").GetComponent<Player_Movement>().stopwatchInstances;

        if (slowsPlayerHas > 0)
        {
            for (int i = 0; i < slowsPlayerHas; i++)
            {
                moveSpeed *= 0.9f;
            }
        }
        Instantiate(enemyShootAudio);
        enemyPos.x = rb.transform.position.x;
        enemyPos.y = rb.transform.position.y;
        playerPos.x = GameObject.Find("Player").transform.position.x;
        playerPos.y = GameObject.Find("Player").transform.position.y;
        vectorToPlayer = (playerPos - enemyPos).normalized;
        rb.velocity = new Vector2(vectorToPlayer.x * moveSpeed, vectorToPlayer.y * moveSpeed);

        Invoke(nameof(DestorySelf), destroyDelay); //will invoke (run the function) in so many seconds
    }

    void DestorySelf() //deeath
    {
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Destroy(gameObject);
    }

}
