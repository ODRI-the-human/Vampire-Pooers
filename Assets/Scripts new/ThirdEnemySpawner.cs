using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ThirdEnemySpawner : MonoBehaviour
{
    public float spawnTimerLength = 200f;
    float spawnTimer = 0;
    int spawnNumber = 1; // records the number of times a group of enemies has been spawned, so more enemies will be spawned after a period of time and (later) more dangerous enemies will spawn.
    int waveNumber = 0;
    public int minSpawnMultiplier = 2;
    public int maxSpawnMultiplier = 4;
    float spawnScaleRate = 0.09f;
    float SpawnPosX;
    float SpawnPosY;
    int SpawnType;
    public TextMeshProUGUI waveText;
    public GameObject itemPedestal;
    int noSpawnsBeforeNewWave = 4; // actually should be one more than the desired number, for some reason.
    int numberEnemiesSpawned;

    public GameObject firstMole;

    public GameObject chaseEnemy;
    public GameObject shootEnemy;
    public GameObject fourDirEnemy;
    public GameObject eightDirEnemy;
    public GameObject spinEnemy;
    public GameObject mole;
    GameObject toSpawn;
    GameObject Player;
    GameObject Camera;

    void Start()
    {
        Player = GameObject.Find("newPlayer");
        Camera = GameObject.Find("Main Camera");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        waveText.text = "Wave: " + (spawnNumber - 1).ToString() + " / Round: " + (waveNumber + 1).ToString();
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
        numberEnemiesSpawned = Mathf.RoundToInt(Random.Range(minSpawnMultiplier * ((spawnNumber + waveNumber * 2) * spawnScaleRate), maxSpawnMultiplier * ((spawnNumber + waveNumber * 2) * spawnScaleRate))) + 1;
        SpawnType = Random.Range(0, 6);
        switch (SpawnType)
        {
            case 0:
                toSpawn = chaseEnemy;
                Debug.Log("Chasey");
                SpawnRandomly();
                break;
            case 1:
                toSpawn = shootEnemy;
                Debug.Log("normie shoot");
                SpawnInGroup();
                break;
            case 2:
                toSpawn = fourDirEnemy;
                Debug.Log("4dir shoot");
                SpawnInGroup();
                break;
            case 3:
                toSpawn = eightDirEnemy;
                Debug.Log("8dir shoot");
                SpawnInGroup();
                break;
            case 4:
                toSpawn = spinEnemy;
                Debug.Log("spiney");

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
                GameObject spawned = Instantiate(toSpawn, new Vector3(SpawnPosX, SpawnPosY, 10.6f) + Camera.transform.position, Quaternion.identity);
                spawned.transform.localScale = SpawnScaleVariation;
                spawned.GetComponent<HPDamageDie>().HP *= 0.8f + 0.2f * numberEnemiesSpawned;
                break;
            case 5:
                toSpawn = mole;
                numberEnemiesSpawned += 2;
                SpawnInGroup();
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

        bool existsMole = false;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Hostile");
        foreach (GameObject friend in enemies)
        {
            if (friend.GetComponent<moleShit>() != null)
            {
                existsMole = true;
                firstMole = friend;
                break;
            }
        }

        if (enemies.Length == 0)
        {
            existsMole = false;
            firstMole = null;
        }

        for (int i = 0; i < numberEnemiesSpawned; i++)
        {
            float SpawnPosXVariation = Random.Range(-1f, 1f);
            float SpawnPosYVariation = Random.Range(-1f, 1f);
            GameObject spawned = Instantiate(toSpawn, new Vector3(SpawnPosX + SpawnPosXVariation, SpawnPosY + SpawnPosYVariation, 10.6f) + Camera.transform.position, transform.rotation);

            if (i == 0 && !existsMole && toSpawn == mole)
            {
                spawned.GetComponent<moleShit>().goesFirst = true;
                firstMole = spawned;
            }

            if (existsMole && toSpawn == mole)
            {
                spawned.GetComponent<moleShit>().timer = firstMole.GetComponent<moleShit>().timer;
            }
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
            GameObject spawned = Instantiate(toSpawn, new Vector3(SpawnPosX, SpawnPosY, 10.6f) + Camera.transform.position, transform.rotation);
        }
    }

    void SpawnItems()
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject newObject = Instantiate(itemPedestal, new Vector3(5 * i - 5, 3, 10.6f) + Camera.transform.position, transform.rotation) as GameObject;
            newObject.transform.localScale = new Vector3(5, 5, 5);
        }
        StartWave();
    }

    void StartWave()
    {
        waveNumber++;
        spawnNumber = 0;
    }
}
