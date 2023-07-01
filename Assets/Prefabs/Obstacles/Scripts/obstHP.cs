using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstHP : MonoBehaviour
{
    public float HP;
    public float bulletResist;

    public void OnCollisionEnter2D(Collision2D col)
    {
        owMyEntireRockIsInPain(col.gameObject, col.gameObject.GetComponent<DealDamage>().GetDamageAmount());
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        owMyEntireRockIsInPain(col.gameObject, col.gameObject.GetComponent<DealDamage>().GetDamageAmount());
    }

    public void owMyEntireRockIsInPain(GameObject thingy, float damageAmt)
    {
        //Debug.Log("bebeb");

        if (thingy.tag == "PlayerBullet" || thingy.tag == "enemyBullet")
        {
            HP -= damageAmt / bulletResist;
        }
        else if (thingy.tag == "ATGExplosion")
        {
            HP -= damageAmt;
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

