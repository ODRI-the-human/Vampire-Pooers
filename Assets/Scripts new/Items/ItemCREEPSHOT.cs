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
            GameObject owner = gameObject.GetComponent<DealDamage>().owner;
            owner.GetComponent<ItemCREEPSHOT>().SpawnTheCreep(gameObject);
        }
    }

    void RollOnHit(GameObject[] objects)
    {
        if (gameObject.tag == "Player" || gameObject.tag == "Hostile")
        {
            SpawnTheCreep(objects[1]);
        }
    }

    void SpawnTheCreep(GameObject source)
    {
        if (gameObject.tag == "Player")
        {
            creeper = EntityReferencerGuy.Instance.Creep;
        }
        if (gameObject.tag == "Hostile")
        {
            creeper = EntityReferencerGuy.Instance.CreepHostile;
        }
        GameObject buoerber = Instantiate(creeper);
        buoerber.transform.position = new Vector3(source.transform.position.x, source.transform.position.y, 0);
        buoerber.GetComponent<DealDamage>().overwriteDamageCalc = true;
        buoerber.GetComponent<DealDamage>().damageAmt = 0.1f * source.GetComponent<DealDamage>().damageAmt * instances;
        buoerber.GetComponent<DealDamage>().owner = gameObject.GetComponent<DealDamage>().owner;
    }

    void Undo()
    {
        Destroy(this);
    }
}
