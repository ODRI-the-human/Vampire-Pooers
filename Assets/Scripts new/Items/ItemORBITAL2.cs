using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemORBITAL2 : MonoBehaviour
{
    GameObject orbSkothos2;
    GameObject myGuy;
    public int instances = 1;

    void Start()
    {
        if (gameObject.tag == "Player" || gameObject.tag == "Hostile")
        {
            orbSkothos2 = GameObject.Find("bigFuckingMasterObject").GetComponent<EntityReferencerGuy>().orbSkothos2;
            SetStats();
        }
    }

    void SetStats()
    {
        myGuy = Instantiate(orbSkothos2);
        myGuy.GetComponent<ItemHolder>().itemsHeld = gameObject.GetComponent<ItemHolder>().itemsHeld;
        myGuy.GetComponent<DealDamage>().damageBase = gameObject.GetComponent<DealDamage>().damageBase;
        myGuy.GetComponent<DealDamage>().damageMult = gameObject.GetComponent<DealDamage>().damageMult;
        myGuy.GetComponent<Attack>().fireTimerLength = gameObject.GetComponent<Attack>().fireTimerLength;
        myGuy.GetComponent<DealDamage>().finalDamageMult *= 0.25f * instances;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "item")
        {
            Destroy(myGuy);
            SetStats();
        }
    }
}
