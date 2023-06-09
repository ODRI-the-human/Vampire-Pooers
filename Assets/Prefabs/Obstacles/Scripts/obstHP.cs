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

    public void owMyEntireRockIsInPain(GameObject thingy)
    {
        //Debug.Log("bebeb");

        if (thingy.tag == "PlayerBullet" || thingy.tag == "enemyBullet")
        {
            HP -= thingy.GetComponent<DealDamage>().finalDamageStat / bulletResist;
        }
        else if (thingy.tag == "ATGExplosion")
        {
            HP -= thingy.GetComponent<DealDamage>().finalDamageStat;
        }

        if (HP <= 0)
        {
            SendMessage("doOnDestroy");
            Destroy(gameObject);
        }
    }

    public void doOnDestroy()
    {
        //shut up!
    }
}

