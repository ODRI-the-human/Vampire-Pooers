using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ThirdEnemySpawner : MonoBehaviour
{
    public float spawnTimerLength = 200f;
    public float spawnTimer = 0;
    public int spawnNumber = 1; // records the number of times a group of enemies has been spawned, so more enemies will be spawned after a period of time and (later) more dangerous enemies will spawn.
    public int waveNumber = 0;
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

    int stepUpTo = 0; // Keeps track of what step the spawner is up to, 0 is spawning, 1 is waiting for player to pick items.
    GameObject[] spawnedPedestals;
    int numSpawnedPedestals;

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

    int xpPerWave = 3;
    int currentXP = 0;

    public GameObject toSpawn;
    GameObject[] players;
    GameObject Camera;

    int typeToAvoidSpawning = -5;

    int spawnTypeMin = 0;
    int spawnTypeMax = 12;

    Vector3 placeToSpawn;
    public bool enemiesAreSpawning = true;
    public float totalSpawnsSurvived = 0;

    public List<int> playerBannedItems = new List<int>();
    public int playerBannedWeapon;
    public int playerBannedDodge;

    void Start()
    {
        spawnTimer = 5;

        players = GameObject.FindGameObjectsWithTag("Player");
        Camera = GameObject.Find("Main Camera");

        playerBannedDodge = (int)ITEMLIST.DODGEROLL;
        playerBannedWeapon = (int)ITEMLIST.PISTOL;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (stepUpTo == 1)
        {
            if (GameObject.FindGameObjectsWithTag("item").Length == 0)
            {
                StartWave();
            }
            else
            {
                spawnTimer = 50;
            }
        }

        waveText.text = "Wave: " + (spawnNumber - 1).ToString() + " / Round: " + (waveNumber + 1).ToString();
        spawnTimer--;
        if (spawnTimer < 0)
        {
            enemiesAreSpawning = true;
            if (spawnNumber - waveNumber != noSpawnsBeforeNewWave)
            {
                PickAction();
                spawnNumber += 1;
                currentXP += xpPerWave;
                spawnTimerLength /= 1.025f;
                spawnTimer = spawnTimerLength;
            }
            else
            {
                if (GameObject.FindGameObjectsWithTag("Hostile").Length < 1)
                {
                    PickAction();
                    spawnNumber += 1;
                    currentXP += xpPerWave;
                    spawnTimerLength /= 1.025f;
                    spawnTimer = spawnTimerLength;
                }
            }
        }

        if (Mathf.Round(spawnTimer) % 100 == 0)
        {
            if (GameObject.FindGameObjectsWithTag("Hostile").Length < 1 && GameObject.FindGameObjectsWithTag("item").Length < 1)
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
            totalSpawnsSurvived++;
        }
        else
        {
            SpawnItems();
        }
    }

    void SpawnEnemies()
    {
        stepUpTo = 0;

        Debug.Log("Spawning a... LIBERAL!");
        numberEnemiesSpawned = Mathf.RoundToInt(Random.Range(minSpawnMultiplier * ((spawnNumber + waveNumber * 2) * spawnScaleRate), maxSpawnMultiplier * ((spawnNumber + waveNumber * 2) * spawnScaleRate))) + 1;
        SpawnType = Random.Range(spawnTypeMin, spawnTypeMax);

        typeToAvoidSpawning = 4;

        while (typeToAvoidSpawning == SpawnType)
        {
            SpawnType = Random.Range(spawnTypeMin, spawnTypeMax);
        }

        typeToAvoidSpawning = -5;

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
                toSpawn = eightDirEnemy;
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
                GameObject spawned = Instantiate(toSpawn, new Vector3(SpawnPosX, SpawnPosY, 10.6f) + Camera.transform.position, Quaternion.identity);
                spawned.transform.localScale = SpawnScaleVariation;
                spawned.GetComponent<HPDamageDie>().HP *= 0.8f + 0.2f * numberEnemiesSpawned;
                spawned.GetComponent<Attack>().currentTarget = players[0];
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
                    toSpawn = mole;
                    SpawnInGroup();
                }
                break;
            case 6:
                toSpawn = homingMineGuy;
                SpawnRandomly();
                break;
            case 7:
                toSpawn = telefragGuy;
                SpawnRandomly();
                break;
            case 8:
                toSpawn = notMonstro;
                numberEnemiesSpawned = 2;
                SpawnRandomly();
                break;
            case 9:
                toSpawn = grabEnemy;
                numberEnemiesSpawned = 2;
                SpawnRandomly();
                break;
            case 10:
                toSpawn = chargeEnemy;
                SpawnRandomly();
                break;
            case 11:
                toSpawn = lazerEnemy;
                SpawnRandomly();
                break;
        }


        // For giving enemies XP
        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Hostile");
        foreach (GameObject enemy in allEnemies)
        {
            enemy.GetComponent<LevelUp>().GiveXP(currentXP);
        }

        // For enemy on round end items.
        if (totalSpawnsSurvived % 8 == 0) // If this current spawn is divisible by 8, do the on round ends.
        {
            foreach (GameObject enemy in allEnemies)
            {
                enemy.SendMessage("newWaveEffects");
            }
        }
    }

    void GetAFunnyPosition()
    {
        Vector2 camPos = new Vector2(Camera.transform.position.x, Camera.transform.position.y);
        SpawnPosX = camPos.x;
        SpawnPosY = camPos.y;

        float xBound = Camera.GetComponent<cameraMovement>().xBound;
        float yBound = Camera.GetComponent<cameraMovement>().yBound;

        while ((new Vector3(SpawnPosX, SpawnPosY, Camera.transform.position.z) - Camera.transform.position).magnitude < 10 || Mathf.Abs(SpawnPosY) > Mathf.Abs(yBound) || Mathf.Abs(SpawnPosX) > Mathf.Abs(xBound))
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
            spawned.GetComponent<Attack>().currentTarget = players[0];
            spawned.GetComponent<Attack>().stopwatchDebuffAmount = gameObject.GetComponent<MasterItemManager>().stopWatchDebuffAmt;

            if (toSpawn == mole)
            {
                if (!gameObject.GetComponent<moleGamingV3>().doCycle)
                {
                    gameObject.GetComponent<moleGamingV3>().doCycle = true;
                    gameObject.GetComponent<moleGamingV3>().StartCycle();
                }
            }

            gameObject.GetComponent<doMasterCurses>().ApplyDropItemOnDeath(spawned);
        }
    }

    void SpawnRandomly()
    {
        for (int i = 0; i < numberEnemiesSpawned; i++)
        {
            GetAFunnyPosition();

            GameObject spawned = Instantiate(toSpawn, new Vector3(SpawnPosX, SpawnPosY, 0), transform.rotation);
            spawned.GetComponent<ItemHolder>().itemsHeld = gameObject.GetComponent<ItemHolder>().itemsHeld;
            spawned.GetComponent<Attack>().currentTarget = players[0];
            spawned.GetComponent<Attack>().stopwatchDebuffAmount = gameObject.GetComponent<MasterItemManager>().stopWatchDebuffAmt;
            gameObject.GetComponent<doMasterCurses>().ApplyDropItemOnDeath(spawned);
        }
    }

    void SpawnItems()
    {
        enemiesAreSpawning = false;
        GameObject[] enemyBullets = GameObject.FindGameObjectsWithTag("enemyBullet");
        foreach (GameObject bullet in enemyBullets)
        {
            Destroy(bullet);
        }

        for (int i = 0; i < 3; i++)
        {
            GameObject newObject = Instantiate(itemPedestal, new Vector3(5 * i - 5, 3, 8) + Camera.transform.position, transform.rotation) as GameObject;
            newObject.transform.localScale = new Vector3(5, 5, 5);
            newObject.GetComponent<itemPedestal>().bannedItems = playerBannedItems;
            newObject.GetComponent<itemPedestal>().bannedWeapon = playerBannedWeapon;
            newObject.GetComponent<itemPedestal>().bannedDodge = playerBannedDodge;
        }

        stepUpTo = 1;
    }

    void StartWave()
    {
        foreach (GameObject player in players)
        {
            if (player != null)
            {
                player.SendMessage("newWaveEffects");
            }
        }
        gameObject.GetComponent<playerManagement>().NewRoundStarted();
        waveNumber++;
        stepUpTo = 0;
        spawnNumber = 0;
    }
}
