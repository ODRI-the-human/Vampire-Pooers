using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class secondEnemySpawner : MonoBehaviour
{
    public float spawnTimerLength = 200f;
    float spawnTimer = 0;
    int spawnNumber = 1; // records the number of times a group of enemies has been spawned, so more enemies will be spawned after a period of time and (later) more dangerous enemies will spawn.
    int waveNumber = 0;
    public int minSpawnMultiplier = 2;
    public int maxSpawnMultiplier = 4;
    float spawnScaleRate = 0.17f;
    float SpawnPosX;
    float SpawnPosY;
    int SpawnType;
    public TextMeshProUGUI waveText;
    public GameObject itemPedestal;
    int noSpawnsBeforeNewWave = 4; // actually should be one more than the desired number, for some reason.

    public GameObject Enemy;
    public GameObject funnyEnemy;
    public GameObject funnyerEnemy;
    public GameObject funniestEnemyEver;
    public GameObject funniestEnemyEverBUTFUNNIER;
    public GameObject funniestEnemyEverBUTFUNNIERANDEVENFUNNIER; // I should stop naming them like this

    // Update is called once per frame
    void FixedUpdate()
    {
        waveText.text = spawnNumber.ToString() + "   " + waveNumber.ToString();
        spawnTimer -= 1;
        if (spawnTimer < 0)
        {
            if (GameObject.FindGameObjectsWithTag("item").Length < 1)
            {
                if (spawnNumber % noSpawnsBeforeNewWave != 0)
                {
                    PickAction();
                    spawnNumber += 1;
                    spawnTimerLength /= 1.025f;
                    spawnTimer = spawnTimerLength;
                }
                else
                {
                    if (GameObject.FindGameObjectsWithTag("Hostile").Length < 1)
                    {
                        PickAction();
                        spawnNumber += 1;
                        spawnTimerLength /= 1.025f;
                        spawnTimer = spawnTimerLength;
                    }
                }
            }
        }
        if (GameObject.FindGameObjectsWithTag("Hostile").Length < 1)
        {
            if (GameObject.FindGameObjectsWithTag("item").Length < 1)
            {
                spawnTimer = 0;
            }
        }
    }

    void PickAction()
    {
        if (spawnNumber % noSpawnsBeforeNewWave != 0)
        {
            SpawnEnemies();
        }
        else
        {
            SpawnItems();
        }
    }

    void SpawnEnemies()
    {
        float numberEnemiesSpawned = Random.Range(minSpawnMultiplier * ((spawnNumber + waveNumber * 3) * spawnScaleRate), maxSpawnMultiplier * ((spawnNumber + waveNumber * 3) * spawnScaleRate)); // determines no. of enemies to spawn
        int numberEnemiesSpawnedInt = Mathf.RoundToInt(numberEnemiesSpawned);
        //SpawnType = Mathf.RoundToInt(Random.Range(-0.5f, 3.5f));
        SpawnType = Mathf.RoundToInt(Random.Range(-0.5f, 5.5f));
        switch (SpawnType)
        {
            case (2):
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
                    Vector2 SpawnScaleVariation = new Vector2(8, 10);
                    SpawnScaleVariation.y *= 1 + numberEnemiesSpawnedInt / 8;
                    SpawnScaleVariation.x *= 1 + numberEnemiesSpawnedInt / 16;
                    GameObject spawned = Instantiate(funnyerEnemy, new Vector3(SpawnPosX + SpawnPosXVariation, SpawnPosY + SpawnPosYVariation, 0), Quaternion.identity);
                    spawned.transform.localScale = SpawnScaleVariation;
                    spawned.GetComponent<Enemy_Movement>().HP *= 0.8f + 0.2f*numberEnemiesSpawnedInt;

                    break;
                }

            case (0):
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

                    break;
                }
            case (1):
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
                    break;
                }
            case (3):
                {
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
                            Instantiate(funniestEnemyEver, new Vector3(SpawnPosX + SpawnPosXVariation, SpawnPosY + SpawnPosYVariation, 0), new Quaternion(1, 0, 0, 0));
                        }

                        break;
                    }
                }
            case (4):
                {
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
                            Instantiate(funniestEnemyEverBUTFUNNIER, new Vector3(SpawnPosX + SpawnPosXVariation, SpawnPosY + SpawnPosYVariation, 0), new Quaternion(1, 0, 0, 0));
                        }

                        break;
                    }
                }
            case (5):
                {
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
                            Instantiate(funniestEnemyEverBUTFUNNIERANDEVENFUNNIER, new Vector3(SpawnPosX + SpawnPosXVariation, SpawnPosY + SpawnPosYVariation, 0), new Quaternion(1, 0, 0, 0));
                        }

                        break;
                    }
                }
        }
    }

    void SpawnItems()
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject newObject = Instantiate(itemPedestal, new Vector3(5 * i - 5, 3, 0), transform.rotation) as GameObject;
            newObject.transform.localScale = new Vector3(5, 5, 5);
            noSpawnsBeforeNewWave++;
        }
        StartWave();
    }

    void StartWave()
    {
        waveNumber++;
        waveText.text = spawnNumber.ToString() + "   " + waveNumber.ToString();
        spawnNumber = 0;
    }
}
