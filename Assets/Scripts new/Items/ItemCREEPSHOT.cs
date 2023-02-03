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
        creeper = master.GetComponent<EntityReferencerGuy>().CreepHostile;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        GameObject buoerber = Instantiate(creeper, transform.position, Quaternion.Euler(0, 0, 0));
        buoerber.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        buoerber.GetComponent<DealDamage>().overwriteDamageCalc = true;
        buoerber.GetComponent<DealDamage>().damageAmt = 0.1f * gameObject.GetComponent<DealDamage>().damageAmt;
    }
}
