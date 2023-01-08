using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstHP : MonoBehaviour
{
    public float HP;
    public float bulletResist;

    public void OnCollisionEnter2D(Collision2D col)
    {
        owMyEntireRockIsInPain(col.gameObject);
    }
    
    public void OnTriggerEnter2D(Collider2D col)
    {
        owMyEntireRockIsInPain(col.gameObject);
    }

    void owMyEntireRockIsInPain(GameObject thingy)
    {
        if (thingy.tag == "PlayerBullet" || thingy.tag == "enemyBullet")
        {
            HP -= thingy.GetComponent<DealDamage>().damageAmt / bulletResist;
        }
        else if (thingy.tag == "ATGExplosion")
        {
            HP -= thingy.GetComponent<DealDamage>().damageAmt;
        }

        if (HP <= 0)
        {
            Destroy(gameObject);
        }
    }
}

