using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemORBITAL1 : MonoBehaviour
{
    GameObject orbSkothos;

    void Start()
    {
        if (gameObject.tag == "Player" || gameObject.tag == "Hostile")
        {
            orbSkothos = GameObject.Find("bigFuckingMasterObject").GetComponent<EntityReferencerGuy>().orbSkothos;
            GameObject newObject = Instantiate(orbSkothos);
            newObject.GetComponent<DealDamage>().procCoeff = 0;
            newObject.GetComponent<DealDamage>().damageBase = 10;
            newObject.GetComponent<DealDamage>().damageMult = 1f;
            newObject.GetComponent<DealDamage>().knockBackCoeff = 0;
            newObject.GetComponent<DealDamage>().owner = gameObject;
            if (gameObject.tag == "Player")
            {
                newObject.tag = "PlayerBullet";
            }
            else
            {
                newObject.tag = "enemyBullet";
            }
        }
    }
}
