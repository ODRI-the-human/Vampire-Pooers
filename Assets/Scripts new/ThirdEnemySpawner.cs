using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ThirdEnemySpawner : MonoBehaviour
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
    int numberEnemiesSpawned;

    public GameObject chaseEnemy;
    public GameObject shootEnemy;
    public GameObject fourDirEnemy;
    public GameObject sixDirEnemy;
    public GameObject spinEnemy;
    GameObject toSpawn;

    void Update()
    {
        if (Input.GetButton("Restart"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        waveText.text = spawnNumber.ToString() + "   " + waveNumber.ToString();
        spawnTimer -= 1;
        if (spawnTimer < 0)
        {
            if (GameObject.FindGameObjectsWithTag("item").Length < 1)
            {
                if (spawnNumber - waveNumber != noSpawnsBeforeNewWave)
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
        if (spawnNumber - waveNumber != noSpawnsBeforeNewWave)
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
        numberEnemiesSpawned = Mathf.RoundToInt(Random.Range(minSpawnMultiplier * ((spawnNumber + waveNumber * 2) * spawnScaleRate), maxSpawnMultiplier * ((spawnNumber + waveNumber * 2) * spawnScaleRate)));
        SpawnType = Mathf.RoundToInt(Random.Range(-0.5f, 4.5f));
        switch (SpawnType)
        {
            case 0:
                toSpawn = chaseEnemy;
                SpawnRandomly();
                break;
            case 1:
                toSpawn = shootEnemy;
                SpawnInGroup();
                break;
            case 2:
                toSpawn = fourDirEnemy;
                SpawnInGroup();
                break;
            case 3:
                toSpawn = sixDirEnemy;
                SpawnInGroup();
                break;
            case 4:
                toSpawn = spinEnemy;

                SpawnPosX = 0;
                SpawnPosY = 0;
                while (Mathf.Abs(SpawnPosX) < 10 && Mathf.Abs(SpawnPosY) < 6)
                {
                    SpawnPosX = Random.Range(-12, 12);
                    SpawnPosY = Random.Range(-8, 8);
                }

                Vector2 SpawnScaleVariation = new Vector2(8, 10);
                SpawnScaleVariation.y *= 1 + numberEnemiesSpawned / 8;
                SpawnScaleVariation.x *= 1 + numberEnemiesSpawned / 16;
                GameObject spawned = Instantiate(toSpawn, new Vector3(SpawnPosX, SpawnPosY, 0), Quaternion.identity);
                spawned.transform.localScale = SpawnScaleVariation;
                spawned.GetComponent<Enemy_Movement>().HP *= 0.8f + 0.2f * numberEnemiesSpawned;
                break;
        }
    }

    void SpawnInGroup()
    {
        SpawnPosX = 0;
        SpawnPosY = 0;
        while (Mathf.Abs(SpawnPosX) < 10 && Mathf.Abs(SpawnPosY) < 6)
        {
            SpawnPosX = Random.Range(-12, 12);
            SpawnPosY = Random.Range(-8, 8);
        }

        for (int i = 0; i < numberEnemiesSpawned; i++)
        {
            float SpawnPosXVariation = Random.Range(-1f, 1f);
            float SpawnPosYVariation = Random.Range(-1f, 1f);
            GameObject spawned = Instantiate(toSpawn, new Vector3(SpawnPosX + SpawnPosXVariation, SpawnPosY + SpawnPosYVariation, 0), transform.rotation);
            //spawned.GetComponent<Enemy_Movement>().HP *= 0.8f + 0.2f * waveNumber;
        }
    }

    void SpawnRandomly()
    {
        for (int i = 0; i < numberEnemiesSpawned; i++)
        {
            SpawnPosX = 0;
            SpawnPosY = 0;
            while (Mathf.Abs(SpawnPosX) < 10 && Mathf.Abs(SpawnPosY) < 6)
            {
                SpawnPosX = Random.Range(-12, 12);
                SpawnPosY = Random.Range(-8, 8);
            }
            GameObject spawned = Instantiate(toSpawn, new Vector3(SpawnPosX, SpawnPosY, 0), transform.rotation);
            //spawned.GetComponent<Enemy_Movement>().HP *= 0.8f + 0.2f * waveNumber;
        }
    }

    void SpawnItems()
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject newObject = Instantiate(itemPedestal, new Vector3(5 * i - 5, 3, 0), transform.rotation) as GameObject;
            newObject.transform.localScale = new Vector3(5, 5, 5);
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
