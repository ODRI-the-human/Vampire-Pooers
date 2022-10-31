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
        for (int i = 0; i < instances; i++)
        {
            myGuy = Instantiate(orbSkothos2);
            myGuy.GetComponent<DealDamage>().finalDamageMult = 0;
            myGuy.GetComponent<ItemHolder>().itemsHeld = gameObject.GetComponent<ItemHolder>().itemsHeld;
            myGuy.GetComponent<DealDamage>().owner = gameObject;
            myGuy.GetComponent<OrbitalMovement2>().timerDelay = i * (2 * Mathf.PI / 0.03f) / instances;
            myGuy.GetComponent<OrbitalMovement2>().distanceFromPlayer = 1 + 0.08f * instances;

            if (gameObject.GetComponent<weaponType>() != null) // && gameObject.GetComponent<weaponType>().weaponHeld == (int)WEAPONS.DARKARTS
            {
                myGuy.GetComponent<weaponType>().weaponHeld = gameObject.GetComponent<weaponType>().weaponHeld;
                myGuy.GetComponent<Attack>().newAttack = gameObject.GetComponent<Attack>().newAttack;
            }

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

            myGuy.GetComponent<DealDamage>().damageBase = gameObject.GetComponent<DealDamage>().damageBase;
            myGuy.GetComponent<DealDamage>().damageMult = gameObject.GetComponent<DealDamage>().damageMult;
            myGuy.GetComponent<DealDamage>().finalDamageDIV = gameObject.GetComponent<DealDamage>().finalDamageDIV;
            myGuy.GetComponent<Attack>().Bullet = gameObject.GetComponent<Attack>().Bullet;
            myGuy.GetComponent<Attack>().specialFireType = gameObject.GetComponent<Attack>().specialFireType;
            myGuy.GetComponent<Attack>().fireTimerDIV = gameObject.GetComponent<Attack>().fireTimerDIV;
            myGuy.GetComponent<Attack>().noExtraShots = gameObject.GetComponent<Attack>().noExtraShots;
            myGuy.GetComponent<Attack>().shotAngleCoeff = gameObject.GetComponent<Attack>().shotAngleCoeff;
            myGuy.GetComponent<Attack>().shotSpeed = gameObject.GetComponent<Attack>().shotSpeed;
            myGuy.GetComponent<Attack>().fireTimerLength = gameObject.GetComponent<Attack>().fireTimerLength;
            myGuy.GetComponent<DealDamage>().finalDamageMult = 0.25f;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "item")
        {
            GameObject[] orboes = GameObject.FindGameObjectsWithTag("PlayerBullet");
            foreach (GameObject friend in orboes)
            {
                if (friend.GetComponent<OrbitalMovement2>() != null)
                {
                    Destroy(friend);
                }
            }

            Invoke(nameof(SetStats),0.1f);
        }
    }

    public void Undo()
    {
        GameObject[] orboes = GameObject.FindGameObjectsWithTag("PlayerBullet");
        foreach (GameObject friend in orboes)
        {
            if (friend.GetComponent<OrbitalMovement2>() != null)
            {
                Destroy(friend);
            }
        }
    }
}
