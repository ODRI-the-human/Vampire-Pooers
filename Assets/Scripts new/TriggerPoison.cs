using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPoison : MonoBehaviour
{
    public GameObject owner;
    public float damageAmt;

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Erm get poisoned lol!!!!!!!!!");
        if ((col.gameObject.tag == "Hostile" && gameObject.tag == "PlayerBullet") || (col.gameObject.tag == "Player" && gameObject.tag == "enemyBullet"))
        {
            col.gameObject.GetComponent<Statuses>().AddStatus((int)STATUSES.POISON, damageAmt, owner);
        }
    }
}
