using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Movement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
    Vector2 OriginalSpeed;
    public GameObject master;

    public bool doKillBullet = false;
    [System.NonSerialized] public float liveTime = 0.75f;

    int timer = 0;
    int lastColTime = 0;
    public bool canCollide = true;
    public int piercesLeft = 0;

    void Start()
    {
        master = gameObject.GetComponent<DealDamage>().master;
        if (gameObject.GetComponent<DealDamage>().isBulletClone && doKillBullet)
        {
            Invoke(nameof(KillBullet), liveTime);
        }
    }

    void FixedUpdate()
    {
        OriginalSpeed = rb.velocity;
        speed = rb.velocity.magnitude;

        if (timer != lastColTime)
        {
            canCollide = true;
        }

        lastColTime++;
    }

    void Update()
    {
        if (rb.velocity != Vector2.zero)
        {
            transform.rotation = Quaternion.LookRotation(rb.velocity) * Quaternion.Euler(0, 90, 0);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Vector2 enemyPos = new Vector2(transform.position.x, transform.position.y);
        Vector2 bulletPos = new Vector2(col.transform.position.x, col.transform.position.y);

        float xSpeed = rb.velocity.normalized.x;
        float ySpeed = rb.velocity.normalized.y;

        if (canCollide)
        {
            if (piercesLeft > 0 && col.gameObject.tag != "Wall")
            {
                Physics2D.IgnoreCollision(col.gameObject.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>(), true);
                rb.velocity = OriginalSpeed;
                if (gameObject.GetComponent<ItemPIERCING>() != null)
                {
                    gameObject.GetComponent<DealDamage>().finalDamageMult *= 1 + 0.2f * gameObject.GetComponent<ItemPIERCING>().instances;
                }
                piercesLeft--;
            }
            else
            {
                if (gameObject.GetComponent<ItemBOUNCY>() != null && gameObject.GetComponent<ItemBOUNCY>().bouncesLeft > 0)
                {
                    Vector3 colVector = (enemyPos - bulletPos).normalized;

                    if (col.gameObject.tag == "Wall")
                    {
                        Debug.Log("Blimbpet:" + colVector.ToString());

                        if (Mathf.Abs(colVector.y) > Mathf.Abs(colVector.x))
                        {
                            colVector.x = xSpeed;
                        }
                        else
                        {
                            colVector.y = ySpeed;
                        }
                    }
                    rb.velocity = speed * colVector.normalized;
                    gameObject.GetComponent<ItemBOUNCY>().bouncesLeft--;
                    transform.rotation = Quaternion.LookRotation(rb.velocity) * Quaternion.Euler(0, 90, 0);
                    canCollide = false;
                    lastColTime = timer;
                }
                else
                {
                    Destroy(gameObject);
                    if (gameObject.GetComponent<lazerMovement>() != null)
                    {
                        SendMessage("DoOnDestroys");
                    }
                }
            }
        }
    }

    void KillBullet()
    {
        Destroy(gameObject);
    }

    // for contact item.
    void OnTriggerEnter2D(Collider2D col)
    {
        if ((col.gameObject.tag == "PlayerBullet" && gameObject.tag == "enemyBullet") || (col.gameObject.tag == "enemyBullet" && gameObject.tag == "PlayerBullet"))
        {
            if (col.gameObject.GetComponent<dieOnContactWithBullet>() != null)
            {
                int LayerPlayerBullet = LayerMask.NameToLayer("PlayerBullets");
                int LayerEnemyBullet = LayerMask.NameToLayer("Enemy Bullets");
                if (gameObject.tag == "enemyBullet")
                {
                    gameObject.GetComponent<MeshRenderer>().material = master.GetComponent<EntityReferencerGuy>().playerBulletMaterial;
                    gameObject.layer = LayerPlayerBullet;
                    gameObject.tag = "PlayerBullet";
                }
                else
                {
                    gameObject.GetComponent<MeshRenderer>().material = master.GetComponent<EntityReferencerGuy>().enemyBulletMaterial;
                    gameObject.layer = LayerEnemyBullet;
                    gameObject.tag = "enemyBullet";
                }

                Vector2 enemyPos = new Vector2(transform.position.x, transform.position.y);
                Vector2 bulletPos = new Vector2(col.transform.position.x, col.transform.position.y);
                rb.velocity = speed * (enemyPos - bulletPos).normalized;
                transform.rotation = Quaternion.LookRotation(rb.velocity) * Quaternion.Euler(0, 90, 0);
                //gameObject.GetComponent<DealDamage>().owner = col.gameObject.GetComponent<DealDamage>().owner;

                if (col.gameObject.GetComponent<dieOnContactWithBullet>() != null && col.gameObject.GetComponent<dieOnContactWithBullet>().reduceInstOnHit)
                {
                    col.gameObject.GetComponent<dieOnContactWithBullet>().instances -= 1;
                    if (col.gameObject.GetComponent<dieOnContactWithBullet>().instances == 0)
                    {
                        col.gameObject.GetComponent<dieOnContactWithBullet>().CommitDie();
                    }
                }
            }
            else
            {
                Destroy(gameObject);
                if (gameObject.GetComponent<lazerMovement>() != null)
                {
                    SendMessage("DoOnDestroys");
                }
            }
        }
    }
}
