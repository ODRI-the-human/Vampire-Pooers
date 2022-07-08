using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawner : MonoBehaviour
{

    public float spawnTimerLength = 200f;
    float spawnTimer = 0;
    int spawnNumber = 0; // records the number of times a group of enemies has been spawned, so more enemies will be spawned after a period of time and (later) more dangerous enemies will spawn.
    public int minSpawnMultiplier = 2;
    public int maxSpawnMultiplier = 4;
    public float spawnScaleRate = 0.4f;
    float SpawnPosX;
    float SpawnPosY;
    float SpawnType;

    public GameObject Enemy;
    public GameObject funnyEnemy;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnTimer < 0)
        {
            if (GameObject.FindGameObjectsWithTag("Hostile").Length > 300)
            {
                spawnTimer = spawnTimerLength;
                //prevents 16 billion enemies from being onscreen at once
            }
            else
            {
                spawnNumber += 1;
                spawnTimerLength /= 1.025f;
                spawnTimer = spawnTimerLength;
                float numberEnemiesSpawned = Random.Range(minSpawnMultiplier * (spawnNumber * spawnScaleRate), maxSpawnMultiplier * (spawnNumber * spawnScaleRate)); // determines no. of enemies to spawn
                int numberEnemiesSpawnedInt = Mathf.RoundToInt(numberEnemiesSpawned);
                SpawnType = Random.Range(-1f, 1f);
                if (SpawnType > 0f)
                {
                    SpawnPosX = 0;
                    SpawnPosY = 0;
                    while (Mathf.Abs(SpawnPosX) < 10)
                    {
                        SpawnPosX = Random.Range(-12, 12);
                    }
                    while (Mathf.Abs(SpawnPosY) < 6)
                    {
                        SpawnPosY = Random.Range(-8, 8);
                    }
                    for (int i = 0; i < numberEnemiesSpawnedInt; i++)
                    {
                        float SpawnPosXVariation = Random.Range(-1f, 1f);
                        float SpawnPosYVariation = Random.Range(-1f, 1f);
                        Instantiate(Enemy, new Vector3(SpawnPosX + SpawnPosXVariation, SpawnPosY + SpawnPosYVariation, 0), new Quaternion(1, 0, 0, 0));
                    }
                }
                else
                {
                    for (int i = 0; i < numberEnemiesSpawnedInt; i++)
                    {
                        SpawnPosX = 0;
                        SpawnPosY = 0;
                        while (Mathf.Abs(SpawnPosX) < 10)
                        {
                            SpawnPosX = Random.Range(-12, 12);
                        }
                        while (Mathf.Abs(SpawnPosY) < 6)
                        {
                            SpawnPosY = Random.Range(-8, 8);
                        }
                        float SpawnPosXVariation = Random.Range(-1f, 1f);
                        float SpawnPosYVariation = Random.Range(-1f, 1f);
                        Instantiate(funnyEnemy, new Vector3(SpawnPosX + SpawnPosXVariation, SpawnPosY + SpawnPosYVariation, 0), new Quaternion(1, 0, 0, 0));
                    }
                }
            }
        }
    }

    void FixedUpdate()
    {
        spawnTimer -= 1;
    }
}
