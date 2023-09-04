using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemORBITAL2 : ItemScript
{
    GameObject orbSkothos2;
    GameObject myGuy;
    public GameObject[] spawnedOrbs = new GameObject[0];

    int timey = 0;

    public override void OnPrimaryUse()
    {
        foreach (GameObject orb in spawnedOrbs)
        {
            //Vector3 vec3 = gameObject.GetComponent<Attack>().mouseVector - orb.transform.position;
            //orb.GetComponent<Attack>().vectorToTarget = new Vector2(vec3.x, vec3.y).normalized;
            Vector3 vec3 = Vector3.zero;
            if (gameObject.tag == "Player")
            {
                vec3 = gameObject.GetComponent<Attack>().reticle.transform.position - transform.position;
            }
            else
            {
                vec3 = gameObject.GetComponent<Attack>().currentTarget.transform.position - transform.position;
            }
            orb.GetComponent<Attack>().vectorToTarget = new Vector2(vec3.x, vec3.y).normalized;
            orb.GetComponent<Attack>().UseAttack(gameObject.GetComponent<Attack>().abilityTypes[0], 0, gameObject.GetComponent<Attack>().isPlayerTeam, gameObject.GetComponent<Attack>().lastAttackCharged, true, false);
        }
    }

    public override void AddStack()
    {
        if (gameObject.GetComponent<OrbitalMovement2>() == null)
        {
            orbSkothos2 = EntityReferencerGuy.Instance.orbSkothos2;
            foreach (GameObject item in spawnedOrbs)
            {
                Destroy(item);
            }
            spawnedOrbs = new GameObject[instances];

            for (int i = 0; i < instances; i++)
            {
                myGuy = Instantiate(orbSkothos2);
                myGuy.GetComponent<ItemHolder2>().itemsHeldTransferred = new ItemSOInst[gameObject.GetComponent<ItemHolder2>().itemsHeld.Count];
                for (int j = 0; j < gameObject.GetComponent<ItemHolder2>().itemsHeld.Count; j++)
                {
                    myGuy.GetComponent<ItemHolder2>().itemsHeldTransferred[j] = gameObject.GetComponent<ItemHolder2>().itemsHeld[j];
                }
                myGuy.GetComponent<DealDamage>().owner = gameObject;
                myGuy.GetComponent<Attack>().attackAutomatically = false;
                myGuy.GetComponent<OrbitalMovement2>().timerDelay = i * (2 * Mathf.PI / 0.03f) / instances;
                myGuy.GetComponent<OrbitalMovement2>().distanceFromPlayer = 1 + 0.08f * instances;
                myGuy.GetComponent<Attack>().abilityTypes = gameObject.GetComponent<Attack>().abilityTypes;
                spawnedOrbs[i] = myGuy;
                //Debug.Log("fhdgdsifgds");
            }
            Invoke(nameof(FinaliseStats), 0.1f);
        }
    }

    void FinaliseStats()
    {
        foreach (GameObject orb in spawnedOrbs)
        {
            if (gameObject.tag == "Player")
            {
                orb.tag = "PlayerBullet";
                orb.GetComponent<Attack>().isPlayerTeam = true;
            }
            else
            {
                orb.tag = "enemyBullet";
                orb.GetComponent<Attack>().isPlayerTeam = false;
                int LayerEnemy = LayerMask.NameToLayer("HitPlayerBulletsAndPlayer");
                orb.layer = LayerEnemy;
            }

            orb.GetComponent<DealDamage>().damageBase = gameObject.GetComponent<DealDamage>().damageBase;
            orb.GetComponent<DealDamage>().damageMult = gameObject.GetComponent<DealDamage>().damageMult;
            //orb.GetComponent<Attack>().Bullet = gameObject.GetComponent<Attack>().Bullet;
            orb.GetComponent<Attack>().noExtraShots = gameObject.GetComponent<Attack>().noExtraShots;
            orb.GetComponent<DealDamage>().finalDamageMult = 0.25f;
        }
    }

    public override void RemoveStack()
    {
        AddStack();
    }

    void FixedUpdate()
    {
        timey++;
    }
}
