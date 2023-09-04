using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSPLIT : ItemScript
{
    Vector2 ShotVector;
    Rigidbody2D bulletRB;
    GameObject owner;
    GameObject ignoredColObject; // When an attack splits, the split shots have the victim of the split set as this, and the nocollision between the split shots and this is removed.
    float speed;
    public bool canSplit = true;
    public AbilityParams weaponToUse;
    //GameObject Buuleter;

    void Start()
    {
        owner = gameObject.GetComponent<DealDamage>().owner;
        weaponToUse = gameObject.GetComponent<DealDamage>().abilityType;

        if (gameObject.GetComponent<Rigidbody2D>() != null)
        {
            speed = gameObject.GetComponent<Rigidbody2D>().velocity.magnitude;
        }
        else
        {
            speed = 15;
        }

        if (!canSplit)
        {
            Invoke(nameof(EnableCollision), 0.2f);
        }
    }

    void EnableCollision()
    {
        Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), ignoredColObject.GetComponent<Collider2D>(), false);
    }

    // Start is called before the first frame update
    public override void OnHit(GameObject victim, GameObject sussy)
    {
        if (canSplit)
        {
            //AbilityParams weaponToUse = owner.GetComponent<Attack>().abilityTypes[gameObject.GetComponent<DealDamage>().abilityIndex];
            for (int i = 0; i < 3 * instances; i++)
            {
                Vector2 randVector = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f)).normalized;
                weaponToUse.UseAttack(owner, null, victim.transform.position, randVector, owner.GetComponent<Attack>().isPlayerTeam, 0, false, true, false, true);
                GameObject spawnedObj = weaponToUse.spawnedAttackObjs[0];
                spawnedObj.AddComponent<ItemSPLIT>();
                spawnedObj.GetComponent<ItemSPLIT>().canSplit = false;
                spawnedObj.GetComponent<DealDamage>().transform.localScale /= 1.5f;
                spawnedObj.GetComponent<DealDamage>().finalDamageMult /= 5f;
                spawnedObj.GetComponent<DealDamage>().massCoeff /= 10f;
                if (spawnedObj.GetComponent<checkAllLazerPositions>() != null)
                {
                    spawnedObj.GetComponent<checkAllLazerPositions>().ignoredHits.Add(victim);
                }
                else
                {
                    Physics2D.IgnoreCollision(spawnedObj.GetComponent<Collider2D>(), victim.GetComponent<Collider2D>(), true);
                    spawnedObj.GetComponent<ItemSPLIT>().ignoredColObject = victim;
                }
                Debug.Log("splitmsed");
                canSplit = false;
            }
        }
    }

    //void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (!canSplit)
    //    {
    //        Destroy(gameObject);
    //    }
    //    else
    //    {
    //        if (collision.gameObject.tag != "Wall")
    //        {
    //            GameObject[] grobules = new GameObject[] { collision.gameObject, gameObject };
    //            RollOnHit(grobules);
    //        }
    //    }
    //}

    public void Undo()
    {
        Destroy(this);
    }
}
