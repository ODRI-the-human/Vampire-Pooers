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

    void IncreaseInstances(string name)
    {
        if (name == this.GetType().ToString())
        {
            instances++;
        }
    }

    void Start()
    {
        if (gameObject.tag == "Player" || gameObject.tag == "Hostile")
        {
            orbSkothos2 = EntityReferencerGuy.Instance.orbSkothos2;
            Invoke(nameof(SetStats),0.1f);
        }
    }

    public void OnShootEffects()
    {
        foreach (GameObject orb in Orbs)
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
            gameObject.GetComponent<Attack>().vectorToTarget = new Vector2(vec3.x, vec3.y).normalized;
            orb.GetComponent<Attack>().UseAttack(gameObject.GetComponent<Attack>().abilityTypes[0], 0, gameObject.GetComponent<Attack>().isPlayerTeam, gameObject.GetComponent<Attack>().lastAttackCharged, true);
        }
    }

    void SetStats()
    {
        for (int i = 0; i < instances; i++)
        {
            myGuy = Instantiate(orbSkothos2);
            myGuy.GetComponent<ItemHolder>().itemsHeld = gameObject.GetComponent<ItemHolder>().itemsHeld;
            myGuy.GetComponent<DealDamage>().owner = gameObject;
            myGuy.GetComponent<Attack>().attackAutomatically = false;
            myGuy.GetComponent<OrbitalMovement2>().timerDelay = i * (2 * Mathf.PI / 0.03f) / instances;
            myGuy.GetComponent<OrbitalMovement2>().distanceFromPlayer = 1 + 0.08f * instances;
            Orbs.Add(myGuy);
            //Debug.Log("fhdgdsifgds");
        }
        Invoke(nameof(FinaliseStats), 0.1f);
    }

    void FinaliseStats()
    {
        foreach (GameObject orb in Orbs)
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

    void FixedUpdate()
    {
        timey++;
    }

    void itemsAdded(bool isPassive)
    {
        if (timey > 5)
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

        Destroy(this);
    }
}
