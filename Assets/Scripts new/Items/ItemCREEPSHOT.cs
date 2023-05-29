using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCREEPSHOT : MonoBehaviour
{
    public GameObject creeper;
    public int instances = 1;

    void IncreaseInstances(string name)
    {
        if (name == this.GetType().ToString())
        {
            instances++;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Wall" && (gameObject.tag == "PlayerBullet" || gameObject.tag == "enemyBullet"))
        {
            SpawnTheCreep();
        }
    }

    void RollOnHit(GameObject[] objects)
    {
        SpawnTheCreep();
    }

    void SpawnTheCreep()
    {
        if (gameObject.tag == "PlayerBullet")
        {
            creeper = EntityReferencerGuy.Instance.Creep;
        }
        if (gameObject.tag == "enemyBullet")
        {
            creeper = EntityReferencerGuy.Instance.CreepHostile;
        }
        GameObject buoerber = Instantiate(creeper);
        buoerber.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        buoerber.GetComponent<DealDamage>().overwriteDamageCalc = true;
        buoerber.GetComponent<DealDamage>().damageAmt = 0.2f * gameObject.GetComponent<DealDamage>().damageAmt * instances;
        buoerber.GetComponent<DealDamage>().owner = gameObject.GetComponent<DealDamage>().owner;
    }

    void Undo()
    {
        Destroy(this);
    }
}
