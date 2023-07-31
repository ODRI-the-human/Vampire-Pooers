using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AbilityParams", menuName = "AbilityParams/Bullet6DirRandSpd")]
public class Bullet6DirRandSpd : FireBulletsBase
{
    public override void SpawnBullets(GameObject spawnedBullet, Vector2 targetDir, GameObject owner, GameObject target)
    {
        GameObject bulToSpawn = spawnedBullet;
        int RandGuy = Random.Range(0, 2); // either 0 or 1.
        Vector2 vectorToTarget = new Vector2(RandGuy, Mathf.Abs(RandGuy - 1));
        //Destroy(spawnedBullet);
        for (int i = 0; i < 36; i++)
        {
            int rand = Random.Range(0, 6);
            float currentAngle = (Mathf.PI / 3) * rand + Random.Range(-0.1f, 0.1f);
            GameObject newBul = Instantiate(bulToSpawn, owner.transform.position, Quaternion.identity);
            newBul.GetComponent<Rigidbody2D>().velocity = defaultSpeed * Random.Range(0.5f, 1.5f) * new Vector2(vectorToTarget.x * Mathf.Cos(currentAngle) - vectorToTarget.y * Mathf.Sin(currentAngle), vectorToTarget.x * Mathf.Sin(currentAngle) + vectorToTarget.y * Mathf.Cos(currentAngle)).normalized;
        }
        Destroy(spawnedBullet);
    }
}
