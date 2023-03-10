using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemORBITAL1 : MonoBehaviour
{
    GameObject orbSkothos;
    public int instances = 1;

    void Start()
    {
        SpawnGaries();
    }

    void SpawnGaries()
    {
        if (gameObject.tag == "Player" || gameObject.tag == "Hostile")
        {
            for (int i = 0; i < instances; i++)
            {
                orbSkothos = GameObject.Find("bigFuckingMasterObject").GetComponent<EntityReferencerGuy>().orbSkothos;
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
            }
        }
    }

    void itemsAdded()
    {
        GameObject[] orboes = GameObject.FindGameObjectsWithTag("PlayerBullet");
        foreach (GameObject friend in orboes)
        {
            if (friend.GetComponent<OrbitalMovement>() != null)
            {
                Destroy(friend);
            }
        }

        SpawnGaries();
    }

    public void Undo()
    {
        GameObject[] orboes = GameObject.FindGameObjectsWithTag("PlayerBullet");
        foreach (GameObject friend in orboes)
        {
            if (friend.GetComponent<OrbitalMovement>() != null)
            {
                Destroy(friend);
            }
        }

        Destroy(this);
    }
}
