using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemORBITAL1 : ItemScript
{
    GameObject orbSkothos;
    public GameObject[] spawnedOrbs = new GameObject[0];

    //void Start()
    //{
    //    SpawnGaries();
    //}

    public override void AddStack()
    {
        Debug.Log("spawned orbz");
        foreach (GameObject item in spawnedOrbs)
        {
            Destroy(item);
        }
        spawnedOrbs = new GameObject[instances];

        if (gameObject.tag == "Player" || gameObject.tag == "Hostile")
        {
            for (int i = 0; i < instances; i++)
            {
                orbSkothos = EntityReferencerGuy.Instance.orbSkothos;
                GameObject newObject = Instantiate(orbSkothos);
                newObject.GetComponent<DealDamage>().procCoeff = 0;
                newObject.GetComponent<DealDamage>().damageBase = 10;
                newObject.GetComponent<DealDamage>().damageMult = 1f;
                newObject.GetComponent<DealDamage>().massCoeff = 0;
                newObject.GetComponent<DealDamage>().owner = gameObject;
                newObject.GetComponent<OrbitalMovement>().timerDelay = i * (2 * Mathf.PI / 0.0175f) / instances;
                if (gameObject.tag == "Player")
                {
                    newObject.tag = "PlayerBullet";
                }
                else
                {
                    newObject.tag = "enemyBullet";
                    int LayerEnemy = LayerMask.NameToLayer("HitPlayerBulletsAndPlayer");
                    newObject.layer = LayerEnemy;
                }
                spawnedOrbs[i] = newObject;
            }
        }
    }

    public override void RemoveStack()
    {
        AddStack();
    }
}
