using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AbilityParams", menuName = "AbilityParams/BulletNormal")]
public class BulletNormal : FireBulletsBase
{
    public override void SpawnBullets(GameObject spawnedBullet, Vector2 targetDir, GameObject owner, GameObject target, bool overrideBulletSpawnMethod)
    {       
        //Debug.Log("hey there buddy you working?");
    }
}
