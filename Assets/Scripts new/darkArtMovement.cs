using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class darkArtMovement : MonoBehaviour
{
    public int timer = 0;
    public int LorR;
    public float accAmount = 0.001f;
    public float rotAngle = 0;
    public float initAngle = 0;
    public GameObject owner;
    bool gaming;

    void Start()
    {
        owner = gameObject.GetComponent<DealDamage>().owner;
        gameObject.AddComponent<ItemCONTACT>();

        if (gameObject.tag == "enemyBullet")
        {
            int LayerEnemy = LayerMask.NameToLayer("Enemy Bullets");
            gameObject.layer = LayerEnemy;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (LorR == 0)
        {
            gaming = false;
        }
        else
        {
            gaming = true;
        }

        if (gameObject.GetComponent<SpriteRenderer>() != null)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = gaming;
        }

        timer++;

        if (timer < 4)
        {
            accAmount *= 13; //13
            rotAngle -= 15; //15
        }

        if (timer == 5)
        {
            gameObject.GetComponent<Collider2D>().enabled = false;
        }

        if (timer > 6)
        {
            accAmount /= 13; // 13
        }

        rotAngle += accAmount;

        if (LorR == 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 3f * rotAngle + initAngle + 155);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, -3f * rotAngle + initAngle + 20);
        }

        if (timer > 10)
        {
            Destroy(gameObject);
        }

        transform.position = owner.transform.position;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.GetComponent<HPDamageDie>() != null) // only heals player if object has an HPDamageDie (so only enemies)
        {
            GameObject owner = gameObject.GetComponent<DealDamage>().owner;
            owner.GetComponent<Healing>().Healo(5);
        }
    }
}
