using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//[CreateAssetMenu(fileName = "AbilityParams", menuName = "AbilityParams/FireBullets")]
public abstract class FireBulletsBase : AbilityParams
{
    public string[] effectsToAdd;
    public float defaultSpeed = 6f;
    public abstract void SpawnBullets(GameObject bulletSpawned, Vector2 targetDir, GameObject owner, GameObject target, bool overrideBulletSpawnMethod);

    public override void ActivateAbility(GameObject dealer, GameObject target, Vector2 direction, bool isPlayerTeam, Material mat, int layer, string tag, bool overrideBulletSpawnMethod)
    {
        // Some of this stuff, like shot speed, the spawn location, and whatever will be overridden in some spawnbullets scripts.
        GameObject spawnedBullet = spawnedAttackObjs[0];
        spawnedBullet.GetComponent<MeshRenderer>().material = mat;
        spawnedBullet.GetComponent<DealDamage>().owner = dealer;
        spawnedBullet.GetComponent<Rigidbody2D>().simulated = true;
        spawnedBullet.GetComponent<ItemHolder>().itemsHeld = dealer.GetComponent<ItemHolder>().itemsHeld;

        spawnedBullet.GetComponent<BulletModifiers>().effectNames = effectsToAdd;

        spawnedBullet.GetComponent<Rigidbody2D>().velocity = defaultSpeed * direction.normalized;
        if (isPlayerTeam)
        {
            spawnedBullet.GetComponent<Rigidbody2D>().velocity *= 1.5f;
        }

        SpawnBullets(spawnedBullet, direction, dealer, target, overrideBulletSpawnMethod);
    }

    public override bool CheckUsability(GameObject dealer, GameObject target)
    {
        return true;
    }
}
