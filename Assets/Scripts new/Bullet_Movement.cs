using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Movement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
    Vector2 OriginalSpeed;
    public GameObject master;

    void Start()
    {
        master = gameObject.GetComponent<DealDamage>().master;
    }

    void FixedUpdate()
    {
        OriginalSpeed = rb.velocity;
        speed = rb.velocity.magnitude;
    }

    void Update()
    {
        transform.rotation = Quaternion.LookRotation(rb.velocity) * Quaternion.Euler(0, 90, 0);
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
                transform.rotation = Quaternion.LookRotation(rb.velocity) * Quaternion.Euler(0, 90, 0);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    // for contact item.
    void OnTriggerEnter2D(Collider2D col)
    {
        if ((col.gameObject.tag == "PlayerBullet" && gameObject.tag == "enemyBullet") || (col.gameObject.tag == "enemyBullet" && gameObject.tag == "PlayerBullet"))
        {
            if (gameObject.GetComponent<darkArtMovement>() != null)
            {
                Destroy(gameObject);
            }
            else
            {
                int LayerPlayerBullet = LayerMask.NameToLayer("PlayerBullets");
                int LayerEnemyBullet = LayerMask.NameToLayer("EnemyBullets");
                if (gameObject.tag == "enemyBullet")
                {
                    gameObject.GetComponent<MeshRenderer>().material = master.GetComponent<EntityReferencerGuy>().playerBulletMaterial;
                    gameObject.layer = LayerPlayerBullet;
                }
                else
                {
                    gameObject.GetComponent<MeshRenderer>().material = master.GetComponent<EntityReferencerGuy>().enemyBulletMaterial;
                    gameObject.layer = LayerEnemyBullet;
                }

                Vector2 spomble = col.gameObject.GetComponent<Rigidbody2D>().velocity;
                float colObjSpeed = spomble.magnitude;
                Vector2 enemyPos = new Vector2(transform.position.x, transform.position.y);
                Vector2 bulletPos = new Vector2(col.transform.position.x, col.transform.position.y);
                rb.velocity = speed * (enemyPos - bulletPos).normalized;
                transform.rotation = Quaternion.LookRotation(rb.velocity) * Quaternion.Euler(0, 90, 0);

                if (col.gameObject.GetComponent<dieOnContactWithBullet>() != null)
                {
                    col.gameObject.GetComponent<dieOnContactWithBullet>().instances -= 1;
                    if (col.gameObject.GetComponent<dieOnContactWithBullet>().instances == 0)
                    {
                        col.gameObject.GetComponent<dieOnContactWithBullet>().CommitDie();
                    }
                }
            }
        }
    }
}
