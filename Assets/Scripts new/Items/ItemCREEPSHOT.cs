using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCREEPSHOT : MonoBehaviour
{
    public GameObject creeper;
    public GameObject master;

    void Start()
    {
        master = gameObject.GetComponent<DealDamage>().master;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (gameObject.tag == "PlayerBullet")
        {
            creeper = master.GetComponent<EntityReferencerGuy>().Creep;
        }
        if (gameObject.tag == "enemyBullet")
        {
            creeper = master.GetComponent<EntityReferencerGuy>().CreepHostile;
        }
        GameObject buoerber = Instantiate(creeper, transform.position, Quaternion.Euler(0, 0, 0));
        buoerber.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        buoerber.GetComponent<DealDamage>().overwriteDamageCalc = true;
        buoerber.GetComponent<DealDamage>().damageAmt = 0.1f * gameObject.GetComponent<DealDamage>().damageAmt;
    }
}
