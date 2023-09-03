using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCREEPSHOT : ItemScript
{
    public GameObject creeper;

    //void OnCollisionEnter2D(Collision2D col)
    //{
    //    if (col.gameObject.tag == "Wall")
    //    {
    //        collisionObj = col.gameObject;
    //        SpawnTheCreep();
    //    }
    //}

    public override void OnHit(GameObject victim, GameObject responsible)
    {
        SpawnTheCreep(victim, responsible);
    }

    public override void OnWallHit(GameObject victim, GameObject responsible)
    {
        SpawnTheCreep(victim, responsible);
    }

    void SpawnTheCreep(GameObject victim, GameObject responsible)
    {
        if (responsible.GetComponent<ApplyAttackModifiers>() != null)
        {
            if (responsible.gameObject.tag == "PlayerBullet")
            {
                creeper = EntityReferencerGuy.Instance.Creep;
            }
            if (responsible.gameObject.tag == "enemyBullet")
            {
                creeper = EntityReferencerGuy.Instance.CreepHostile;
            }
            GameObject buoerber = Instantiate(creeper);
            if (gameObject.GetComponent<meleeGeneral>() != null)
            {
                buoerber.transform.position = responsible.transform.position + responsible.GetComponent<meleeGeneral>().maxDist * (victim.transform.position - responsible.transform.position).normalized;
            }
            else
            {
                buoerber.transform.position = new Vector3(responsible.transform.position.x, responsible.transform.position.y, 0);
            }
            buoerber.GetComponent<DealDamage>().overwriteDamageCalc = true;
            buoerber.GetComponent<DealDamage>().finalDamageStat = 0.2f * gameObject.GetComponent<DealDamage>().GetDamageAmount() * instances;
            //Debug.Log("creep damage" + buoerber.GetComponent<DealDamage>().damageAmt.ToString());
            buoerber.GetComponent<DealDamage>().owner = gameObject.GetComponent<DealDamage>().owner;
        }
    }
}
