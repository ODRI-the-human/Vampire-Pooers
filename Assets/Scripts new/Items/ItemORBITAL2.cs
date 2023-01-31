using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemORBITAL2 : MonoBehaviour
{
    GameObject orbSkothos2;
    GameObject myGuy;
    public int instances = 1;
    public List<GameObject> Orbs = new List<GameObject>();

    int timey = 0;

    void Start()
    {
        if (gameObject.tag == "Player" || gameObject.tag == "Hostile")
        {
            orbSkothos2 = GameObject.Find("bigFuckingMasterObject").GetComponent<EntityReferencerGuy>().orbSkothos2;
            Invoke(nameof(SetStats),0.1f);
        }
    }

    void SetStats()
    {
        for (int i = 0; i < instances; i++)
        {
            myGuy = Instantiate(orbSkothos2);
            myGuy.GetComponent<ItemHolder>().itemsHeld = gameObject.GetComponent<ItemHolder>().itemsHeld;
            myGuy.GetComponent<DealDamage>().owner = gameObject;
            myGuy.GetComponent<OrbitalMovement2>().timerDelay = i * (2 * Mathf.PI / 0.03f) / instances;
            myGuy.GetComponent<OrbitalMovement2>().distanceFromPlayer = 1 + 0.08f * instances;
            Orbs.Add(myGuy);
            Debug.Log("fhdgdsifgds");
        }
        Invoke(nameof(FinaliseStats), 0.1f);
    }

    void FinaliseStats()
    {
        foreach (GameObject orb in Orbs)
        {
            orb.GetComponent<ItemHolder>().ApplyAll();
            if (gameObject.GetComponent<weaponType>() != null) // && gameObject.GetComponent<weaponType>().weaponHeld == (int)WEAPONS.DARKARTS
            {
                orb.GetComponent<weaponType>().weaponHeld = gameObject.GetComponent<weaponType>().weaponHeld;
                orb.GetComponent<Attack>().newAttack = gameObject.GetComponent<Attack>().newAttack;
            }

            if (gameObject.tag == "Player")
            {
                orb.tag = "PlayerBullet";
                orb.GetComponent<Attack>().playerControlled = true;
            }
            else
            {
                orb.tag = "enemyBullet";
                orb.GetComponent<Attack>().playerControlled = false;
                int LayerEnemy = LayerMask.NameToLayer("HitPlayerBulletsAndPlayer");
                orb.layer = LayerEnemy;
            }

            orb.GetComponent<DealDamage>().damageBase = gameObject.GetComponent<DealDamage>().damageBase;
            orb.GetComponent<DealDamage>().damageMult = gameObject.GetComponent<DealDamage>().damageMult;
            orb.GetComponent<DealDamage>().finalDamageDIV = gameObject.GetComponent<DealDamage>().finalDamageDIV;
            orb.GetComponent<Attack>().Bullet = gameObject.GetComponent<Attack>().Bullet;
            orb.GetComponent<Attack>().specialFireType = gameObject.GetComponent<Attack>().specialFireType;
            orb.GetComponent<Attack>().fireTimerDIV = gameObject.GetComponent<Attack>().fireTimerDIV;
            orb.GetComponent<Attack>().noExtraShots = gameObject.GetComponent<Attack>().noExtraShots;
            orb.GetComponent<Attack>().shotAngleCoeff = gameObject.GetComponent<Attack>().shotAngleCoeff;
            orb.GetComponent<Attack>().shotSpeed = gameObject.GetComponent<Attack>().shotSpeed;
            orb.GetComponent<Attack>().fireTimerLength = gameObject.GetComponent<Attack>().fireTimerLength;
            orb.GetComponent<DealDamage>().finalDamageMult = 0.25f;
        }
    }

    void FixedUpdate()
    {
        timey++;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "item" && timey > 5)
        {
            Orbs.Clear();
            GameObject[] orboes = GameObject.FindGameObjectsWithTag("PlayerBullet");
            foreach (GameObject friend in orboes)
            {
                if (friend.GetComponent<OrbitalMovement2>() != null)
                {
                    Destroy(friend);
                }
            }

            Invoke(nameof(SetStats), 0.1f);
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
