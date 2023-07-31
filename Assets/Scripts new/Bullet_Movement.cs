using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Movement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
    Vector2 OriginalSpeed;

    public bool doKillBullet = false;

    int timer = 0;
    int lastColTime = 0;
    public bool canCollide = true;
    public int piercesLeft = 0;

    int LayerPlayerBullet;
    int LayerEnemyBullet;
    int defaultLayer;
    string defaultTag;
    Material defaultMaterial;

    float xSpeed;
    float ySpeed;

    void Start()
    {
        LayerPlayerBullet = LayerMask.NameToLayer("PlayerBullets");
        LayerEnemyBullet = LayerMask.NameToLayer("Enemy Bullets");

        defaultLayer = gameObject.layer;
        defaultTag = gameObject.tag;
        defaultMaterial = gameObject.GetComponent<MeshRenderer>().material;
    }

    void EndOfShotRolls()
    {
        gameObject.layer = defaultLayer;
        gameObject.GetComponent<MeshRenderer>().material = defaultMaterial;
        gameObject.tag = defaultTag;
    }

    void FixedUpdate()
    {
        OriginalSpeed = rb.velocity;
        speed = rb.velocity.magnitude;

        //if (timer == 0) // Need to do this here and like this cos the scripts that recieve determineshotrolls aren't available
        //{
        //    gameObject.SendMessage("DetermineShotRolls");
        //}

        timer++;

        if (timer != lastColTime)
        {
            canCollide = true;
        }

        lastColTime++;
    }

    void Update()
    {
        if (rb.velocity.magnitude > 3f)
        {
            transform.rotation = Quaternion.LookRotation(rb.velocity) * Quaternion.Euler(0, 90, 0);
            xSpeed = rb.velocity.normalized.x;
            ySpeed = rb.velocity.normalized.y;
        }
        else
        {
            KillBullet();
        }
    }

    void HitShit(Collider2D col)
    {
        Vector2 enemyPos = new Vector2(transform.position.x, transform.position.y);
        Vector2 bulletPos = new Vector2(col.transform.position.x, col.transform.position.y);

        if (canCollide && col.gameObject.GetComponent<dieOnContactWithBullet>() == null)
        {
            if (piercesLeft > 0 && col.gameObject.tag != "Wall")
            {
                Physics2D.IgnoreCollision(col.gameObject.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>(), true);
                rb.velocity = OriginalSpeed;
                if (gameObject.GetComponent<ItemPIERCING>() != null)
                {
                    gameObject.GetComponent<ItemPIERCING>().IncreaseDamageBonus();
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

                        if (Mathf.Abs(colVector.y) > Mathf.Abs(colVector.x)) // if the bullet hits on a horizontal surface
                        {
                            colVector.x = xSpeed;
                            colVector.y = -ySpeed;
                        }
                        else // on a vertical surface
                        {
                            colVector.y = ySpeed;
                            colVector.x = -xSpeed;
                        }
                    }
                    rb.velocity = speed * colVector.normalized;
                    gameObject.GetComponent<ItemBOUNCY>().bouncesLeft--;
                    //transform.rotation = Quaternion.LookRotation(rb.velocity) * Quaternion.Euler(0, 90, 0);
                    canCollide = false;
                    lastColTime = timer;
                }
                else
                {
                    KillBullet();
                }
            }
        }

        // for contact item.
        if ((col.gameObject.tag == "PlayerBullet" && gameObject.tag == "enemyBullet") || (col.gameObject.tag == "enemyBullet" && gameObject.tag == "PlayerBullet"))
        {
            if (col.gameObject.GetComponent<dieOnContactWithBullet>() != null)
            {
                if (gameObject.tag == "enemyBullet")
                {
                    gameObject.GetComponent<MeshRenderer>().material = EntityReferencerGuy.Instance.playerBulletMaterial;
                    gameObject.layer = LayerPlayerBullet;
                    gameObject.tag = "PlayerBullet";
                }
                else
                {
                    gameObject.GetComponent<MeshRenderer>().material = EntityReferencerGuy.Instance.enemyBulletMaterial;
                    gameObject.layer = LayerEnemyBullet;
                    gameObject.tag = "enemyBullet";
                }

                rb.velocity = speed * (enemyPos - bulletPos).normalized;
                transform.rotation = Quaternion.LookRotation(rb.velocity) * Quaternion.Euler(0, 90, 0);
                gameObject.GetComponent<DealDamage>().finalDamageMult *= 1 + 0.1f * col.gameObject.GetComponent<dieOnContactWithBullet>().instances;
                //gameObject.GetComponent<DealDamage>().owner = col.gameObject.GetComponent<DealDamage>().owner;

                if (col.gameObject.GetComponent<dieOnContactWithBullet>().reduceInstOnHit)
                {
                    col.gameObject.GetComponent<dieOnContactWithBullet>().instances -= 1;
                    if (col.gameObject.GetComponent<dieOnContactWithBullet>().instances == 0)
                    {
                        KillBullet();
                    }
                }
            }
            else
            {
                KillBullet();
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (canCollide)
        {
            HitShit(col.collider);
        }
    }

    public void KillBullet()
    {
        //Destroy(gameObject);
        GameObject owner = gameObject.GetComponent<DealDamage>().owner;
        Destroy(gameObject);
    }

    //void DisableBullet()
    //{
    //    if (gameObject.active == true)
    //    {
    //        GameObject owner = gameObject.GetComponent<DealDamage>().owner;
    //        //owner.GetComponent<Attack>().bulletPool.Release(gameObject);
    //    }
    //}

    void OnTriggerEnter2D(Collider2D col)
    {
        HitShit(col);
    }
}
