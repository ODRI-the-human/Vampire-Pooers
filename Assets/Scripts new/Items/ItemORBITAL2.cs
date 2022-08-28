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
        myGuy.GetComponent<DealDamage>().owner = gameObject;

        if (gameObject.GetComponent<weaponType>() != null && gameObject.GetComponent<weaponType>().weaponHeld == (int)WEAPONS.DARKARTS)
        {
            myGuy.GetComponent<weaponType>().weaponHeld = gameObject.GetComponent<weaponType>().weaponHeld;
            myGuy.GetComponent<Attack>().newAttack = gameObject.GetComponent<Attack>().newAttack;
        }

        myGuy.GetComponent<ItemHolder>().itemsHeld = gameObject.GetComponent<ItemHolder>().itemsHeld;
        myGuy.GetComponent<DealDamage>().damageBase = gameObject.GetComponent<DealDamage>().damageBase;
        myGuy.GetComponent<DealDamage>().damageMult = gameObject.GetComponent<DealDamage>().damageMult;
        myGuy.GetComponent<Attack>().fireTimerLength = gameObject.GetComponent<Attack>().fireTimerLength;
        myGuy.GetComponent<Attack>().Bullet = gameObject.GetComponent<Attack>().Bullet;
        myGuy.GetComponent<Attack>().specialFireType = gameObject.GetComponent<Attack>().specialFireType;
        myGuy.GetComponent<Attack>().noExtraShots = gameObject.GetComponent<Attack>().noExtraShots;
        myGuy.GetComponent<Attack>().shotAngleCoeff = gameObject.GetComponent<Attack>().shotAngleCoeff;
        myGuy.GetComponent<Attack>().shotSpeed = gameObject.GetComponent<Attack>().shotSpeed;

        myGuy.GetComponent<DealDamage>().finalDamageMult *= 0.25f * instances;

        if (gameObject.tag == "Player")
        {
            myGuy.tag = "PlayerBullet";
            myGuy.GetComponent<Attack>().playerControlled = true;
        }
        else
        {
            myGuy.tag = "enemyBullet";
            myGuy.GetComponent<Attack>().playerControlled = false;
        }
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
