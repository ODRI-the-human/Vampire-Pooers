using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AbilityParams", menuName = "AbilityParams/Shotgun")]
public class Shotgun : FireBulletsBase
{
    public float angleMag = 0.3f;

    public override void SpawnBullets(GameObject spawnedBullet, Vector2 targetDir, GameObject owner, GameObject target, bool overrideBulletSpawnMethod)
    {
        if (!overrideBulletSpawnMethod)
        {
            GameObject bulToSpawn = spawnedBullet;
            Vector2 vectorToTarget = targetDir;
            for (int i = 0; i < 8; i++)
            {
                float currentAngle = Random.Range(-angleMag, angleMag);
                GameObject newBul = Instantiate(bulToSpawn, owner.transform.position, Quaternion.identity);
                newBul.GetComponent<Rigidbody2D>().velocity = spawnedBullet.GetComponent<Rigidbody2D>().velocity.magnitude * Random.Range(0.8f, 1.2f) * new Vector2(vectorToTarget.x * Mathf.Cos(currentAngle) - vectorToTarget.y * Mathf.Sin(currentAngle), vectorToTarget.x * Mathf.Sin(currentAngle) + vectorToTarget.y * Mathf.Cos(currentAngle)).normalized;
            }
            Destroy(spawnedBullet);
        }
    }
}
