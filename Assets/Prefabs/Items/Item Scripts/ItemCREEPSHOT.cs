using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCREEPSHOT : MonoBehaviour
{
    public GameObject creeper;
    GameObject collisionObj;
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
            collisionObj = col.gameObject;
            SpawnTheCreep();
        }
    }

    void RollOnHit(GameObject[] objects)
    {
        collisionObj = objects[0];
        SpawnTheCreep();
    }

    void OnWallHit() // To make it so it can work with lazer!
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
        if (gameObject.GetComponent<meleeGeneral>() != null)
        {
            buoerber.transform.position = transform.position + gameObject.GetComponent<meleeGeneral>().maxDist * (collisionObj.transform.position - transform.position).normalized;
        }
        else
        {
            buoerber.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }
        buoerber.GetComponent<DealDamage>().overwriteDamageCalc = true;
        buoerber.GetComponent<DealDamage>().finalDamageStat = 0.2f * gameObject.GetComponent<DealDamage>().GetDamageAmount() * instances;
        //Debug.Log("creep damage" + buoerber.GetComponent<DealDamage>().damageAmt.ToString());
        buoerber.GetComponent<DealDamage>().owner = gameObject.GetComponent<DealDamage>().owner;
    }

    void Undo()
    {
        Destroy(this);
    }
}
