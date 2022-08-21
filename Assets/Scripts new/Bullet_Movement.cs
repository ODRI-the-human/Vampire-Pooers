using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Movement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
    Vector2 OriginalSpeed;

    void Start()
    {
        speed = rb.velocity.magnitude;
    }

    void FixedUpdate()
    {
        OriginalSpeed = rb.velocity;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Vector2 enemyPos = new Vector2(transform.position.x, transform.position.y);
        Vector2 bulletPos = new Vector2(col.transform.position.x, col.transform.position.y);

        if (gameObject.GetComponent<ItemPIERCING>() != null && gameObject.GetComponent<ItemPIERCING>().piercesLeft > 0)
        {
            Physics2D.IgnoreCollision(col.gameObject.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>(), true);
            rb.velocity = OriginalSpeed;
            gameObject.GetComponent<ItemPIERCING>().piercesLeft--;
        }
        else
        {
            if (gameObject.GetComponent<ItemBOUNCY>() != null && gameObject.GetComponent<ItemBOUNCY>().bouncesLeft > 0)
            {
                rb.velocity = speed * (enemyPos - bulletPos).normalized;
                gameObject.GetComponent<ItemBOUNCY>().bouncesLeft--;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "PlayerBullet" && gameObject.tag == "enemyBullet")
        {
            Destroy(gameObject);
        }

        if (col.gameObject.tag == "enemyBullet" && gameObject.tag == "PlayerBullet")
        {
            Destroy(gameObject);
        }
    }
}
