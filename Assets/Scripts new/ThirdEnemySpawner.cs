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
    public int numberEnemiesSpawned;

    public GameObject enemyBullet;

    public bool bypassWaves;

    public GameObject chaseEnemy;
    public GameObject shootEnemy;
    public GameObject fourDirEnemy;
    public GameObject eightDirEnemy;
    public GameObject spinEnemy;
    public GameObject mole;
    public GameObject homingMineGuy;
    public GameObject telefragGuy;
    public GameObject notMonstro;
    public GameObject grabEnemy;
    public GameObject chargeEnemy;
    public GameObject lazerEnemy;

    bool assignProperBullet;

    public GameObject toSpawn;
    GameObject Player;
    GameObject Camera;

    int typeToAvoidSpawning = -5;

    int spawnTypeMin = 0;
    int spawnTypeMax = 11;

    Vector3 placeToSpawn;

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
        if (spawnNumber - waveNumber != noSpawnsBeforeNewWave && !bypassWaves)
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
        Debug.Log("Spawning a... LIBERAL!");
        numberEnemiesSpawned = Mathf.RoundToInt(Random.Range(minSpawnMultiplier * ((spawnNumber + waveNumber * 2) * spawnScaleRate), maxSpawnMultiplier * ((spawnNumber + waveNumber * 2) * spawnScaleRate))) + 1;
        SpawnType = Random.Range(spawnTypeMin, spawnTypeMax);

        while (typeToAvoidSpawning == SpawnType)
        {
            SpawnType = Random.Range(spawnTypeMin, spawnTypeMax);
        }

        typeToAvoidSpawning = -5;

        switch (SpawnType)
        {
            case 0:
                toSpawn = chaseEnemy;
                assignProperBullet = false;
                SpawnRandomly();
                break;
            case 1:
                toSpawn = shootEnemy;
                assignProperBullet = true;
                SpawnInGroup();
                break;
            case 2:
                toSpawn = fourDirEnemy;
                assignProperBullet = true;
                SpawnInGroup();
                break;
            case 3:
                toSpawn = eightDirEnemy;
                assignProperBullet = true;
                SpawnInGroup();
                break;
            case 4:
                toSpawn = spinEnemy;
                assignProperBullet = false;
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
                spawned.GetComponent<Attack>().currentTarget = Player;
                break;
            case 5:
                int numMoles = 0;

                GameObject[] enemies = GameObject.FindGameObjectsWithTag("Hostile");
                foreach (GameObject friend in enemies)
                {
                    if (friend.GetComponent<moleStatus>() != null)
                    {
                        numMoles++;
                    }
                }

                if (numMoles >= 10)
                {
                    typeToAvoidSpawning = 5;
                    SpawnEnemies();
                }
                else
                {
                    numberEnemiesSpawned = 5;
                    assignProperBullet = false;
                    toSpawn = mole;
                    SpawnInGroup();
                }
                break;
            case 6:
                toSpawn = homingMineGuy;
                assignProperBullet = false;
                SpawnRandomly();
                break;
            case 7:
                toSpawn = telefragGuy;
                assignProperBullet = false;
                SpawnRandomly();
                break;
            case 8:
                toSpawn = notMonstro;
                assignProperBullet = true;
                numberEnemiesSpawned = 2;
                SpawnRandomly();
                break;
            case 9:
                toSpawn = grabEnemy;
                assignProperBullet = false;
                numberEnemiesSpawned = 2;
                SpawnRandomly();
                break;
            case 10:
                toSpawn = chargeEnemy;
                assignProperBullet = false;
                SpawnRandomly();
                break;
            case 11:
                toSpawn = lazerEnemy;
                assignProperBullet = false;
                SpawnRandomly();
                break;
        }
    }

    void GetAFunnyPosition()
    {
        Vector2 camPos = new Vector2(Camera.transform.position.x, Camera.transform.position.y);
        SpawnPosX = camPos.x;
        SpawnPosY = camPos.y;

        float xBound = Camera.GetComponent<cameraMovement>().xBound;
        float yBound = Camera.GetComponent<cameraMovement>().yBound;

        while ((new Vector3(SpawnPosX, SpawnPosY, Player.transform.position.z) - Player.transform.position).magnitude < 10 || Mathf.Abs(SpawnPosY) > Mathf.Abs(yBound) || Mathf.Abs(SpawnPosX) > Mathf.Abs(xBound))
        {
            SpawnPosX = camPos.x + Random.Range(-12.1f, 12.1f);
            SpawnPosY = camPos.y + Random.Range(-8.1f, 8.1f);
        }
    }

    void SpawnInGroup()
    {
        GetAFunnyPosition();

        bool existsMole = false;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Hostile");

        for (int i = 0; i < numberEnemiesSpawned; i++)
        {
            float SpawnPosXVariation = Random.Range(-1f, 1f);
            float SpawnPosYVariation = Random.Range(-1f, 1f);
            GameObject spawned = Instantiate(toSpawn, new Vector3(SpawnPosX + SpawnPosXVariation, SpawnPosY + SpawnPosYVariation, 0), transform.rotation);
            spawned.GetComponent<ItemHolder>().itemsHeld = gameObject.GetComponent<ItemHolder>().itemsHeld;
            if (assignProperBullet)
            {
                spawned.GetComponent<Attack>().Bullet = enemyBullet;
            }
            spawned.GetComponent<Attack>().currentTarget = Player;

            if (toSpawn == mole)
            {
                gameObject.GetComponent<moleGamingV3>().CheckForStopWatch();
                if (!gameObject.GetComponent<moleGamingV3>().doCycle)
                {
                    gameObject.GetComponent<moleGamingV3>().doCycle = true;
                    gameObject.GetComponent<moleGamingV3>().StartCycle();
                }
            }
        }
    }

    void SpawnRandomly()
    {
        for (int i = 0; i < numberEnemiesSpawned; i++)
        {
            GetAFunnyPosition();

            GameObject spawned = Instantiate(toSpawn, new Vector3(SpawnPosX, SpawnPosY, 0), transform.rotation);
            spawned.GetComponent<ItemHolder>().itemsHeld = gameObject.GetComponent<ItemHolder>().itemsHeld;
            if (assignProperBullet)
            {
                spawned.GetComponent<Attack>().Bullet = enemyBullet;
            }
            spawned.GetComponent<Attack>().currentTarget = Player;
        }
    }

    void SpawnItems()
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject newObject = Instantiate(itemPedestal, new Vector3(5 * i - 5, 3, 8) + Camera.transform.position, transform.rotation) as GameObject;
            newObject.transform.localScale = new Vector3(5, 5, 5);
        }
        StartWave();
    }

    void StartWave()
    {
        Player.SendMessage("newWaveEffects");
        waveNumber++;
        spawnNumber = 0;
    }
}
