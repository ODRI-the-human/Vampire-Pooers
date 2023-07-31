using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AbilityParams", menuName = "AbilityParams/Bullet8DirConst")]
public class Bullet8DirConst : FireBulletsBase
{
    public override void SpawnBullets(GameObject spawnedBullet, Vector2 targetDir, GameObject owner, GameObject target)
    {
        GameObject bulToSpawn = spawnedBullet;
        Vector2 vectorToTarget = new Vector2(1, 0);
        for (int i = 0; i < 8; i++)
        {
            float currentAngle = (Mathf.PI / 4) * i;
            GameObject newBul = Instantiate(bulToSpawn, owner.transform.position, Quaternion.identity);
            newBul.GetComponent<Rigidbody2D>().velocity = defaultSpeed * new Vector2(vectorToTarget.x * Mathf.Cos(currentAngle) - vectorToTarget.y * Mathf.Sin(currentAngle), vectorToTarget.x * Mathf.Sin(currentAngle) + vectorToTarget.y * Mathf.Cos(currentAngle)).normalized;
        }
        Destroy(spawnedBullet);
    }
}
