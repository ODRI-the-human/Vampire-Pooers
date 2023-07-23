using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class Director : MonoBehaviour
{
    float scaleConst = 1; // this increases through the run, to scale up credit gain n shit.
    [SerializeField] float credits = 0;
    [SerializeField] float creditsForNextSpawn = 0; // sets the number of credits required for the next spawn, so FixedUpdate knows when to call the Spawn function.
    [SerializeField] float spawnCost = 0; // sets the number of credits required for the next spawn, so FixedUpdate knows when to call the Spawn function.
    float creditIncreaseRate = 0.2f;
    public int numEnemyThreshold = 0; // this is the number of enemies at which the director will force spawning some enemies.
    public int numEnemies; // Counts num of enemies alive.
    int killCounter = 0;
    int numKillsForNextItem = 5;
    int lastItemKills = 0;
    public GameObject camera;
    public GameObject itemPedestal;
    int xpPerWave = 8;
    int currentXP = 0;

    public int currentLevel; // Stores the level we're currently on.

    [SerializeField] int timeSinceLastSpawn = 0;
    [SerializeField] int timeTillNextSpawn = 50;
    [SerializeField] int spawnTimerRate = 1;
    int timer = 0;
    public EnemyParams[] allEnemyTypes;// stores all enemy types.
    public List<EnemyParams> currentEnemyTypes = new List<EnemyParams>();// stores the enemy types that can be spawned on this level.

    public EliteParams[] allEliteTypes;
    public List<EliteParams> currentEliteTypes = new List<EliteParams>();// stores the enemy types that can be spawned on this level.

    public List<Vector3> goodPositions = new List<Vector3>();

    [SerializeField] EnemyParams enemyToSpawnNext; // Stores the enemy that will be spawned next.
    [SerializeField] int numEnemiesToSpawn; // The no. of enemies that will be spawned next.
    [SerializeField] EliteParams eliteType; // The elite status of any enemies that will be spawned.
    [SerializeField] int numElitesToSpawn;

    public GameObject spawnPosIndicator;

    void Start()
    {
        //Application.targetFrameRate = -1;

        foreach (EnemyParams currentParam in allEnemyTypes) // Checks which enemies can spawn in this area, adds then to currentEnemyTypes.
        {
            for (int i = 0; i < currentParam.areasCanSpawnIn.Length; i++)
            {
                int areaInt = (int)System.Enum.Parse(typeof(AREAS), currentParam.areasCanSpawnIn[i]);
                if (areaInt == currentLevel)
                {
                    currentEnemyTypes.Add(currentParam);
                }
            }
        }

        foreach (EliteParams currentParam in allEliteTypes) // Checks which enemies can spawn in this area, adds then to currentEnemyTypes.
        {
            for (int i = 0; i < currentParam.areasCanSpawnIn.Length; i++)
            {
                int areaInt = (int)System.Enum.Parse(typeof(AREAS), currentParam.areasCanSpawnIn[i]);
                if (areaInt == currentLevel)
                {
                    currentEliteTypes.Add(currentParam);
                }
            }
        }

        camera = GameObject.Find("Main Camera");
        credits = 100;
    }

    void GetNoOfEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Hostile");
        numEnemies = enemies.Length;
    }

    public void OnEnemiesKilled() // Gets called by HPDamageDie when an enemy is killed.
    {
        killCounter++;

        if (killCounter - lastItemKills >= numKillsForNextItem)
        {
            lastItemKills = killCounter;
            numKillsForNextItem = Mathf.FloorToInt(numKillsForNextItem * 1.2f);

            for (int i = 0; i < 3; i++)
            {
                GameObject newObject = Instantiate(itemPedestal, new Vector3(5 * i - 5, 3, 8) + camera.transform.position, transform.rotation);
                newObject.transform.localScale = new Vector3(5, 5, 5);
            }

            foreach (GameObject player in EntityReferencerGuy.Instance.master.GetComponent<playerManagement>().players)
            {
                if (player != null)
                {
                    player.SendMessage("newWaveEffects");
                }
                transform.parent.gameObject.GetComponent<playerManagement>().SetPlayerStates();
            }
        }

        //GetNoOfEnemies();

        if (numEnemyThreshold >= numEnemies)
        {
            spawnTimerRate = 5; // Advances time faster if very few enemies are left.
        }
    }

    void TeleportFarEnemies() // Teleports far-away enemies to near the player, otherwise there can just be some chilling doing nothing.
    {
        GetAvailablePositions();
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Hostile");
        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<LevelUp>().GiveXP(xpPerWave);
            if ((enemy.transform.position - camera.transform.position).magnitude > 25)
            {
                Debug.Log("teleported an enemy");
                enemy.transform.position = goodPositions[UnityEngine.Random.Range(0, goodPositions.Count)] + new Vector3(UnityEngine.Random.Range(-0.1f, 0.1f), UnityEngine.Random.Range(-0.1f, 0.1f), 0);
            }
        }
        currentXP += xpPerWave;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer++;
        timeSinceLastSpawn += spawnTimerRate;
        credits += creditIncreaseRate * scaleConst; // Director gains creditIncreaseRate * scaleConst * 50 credits per sec.
        //creditIncreaseRate += 0.0001f;

        if (timer % 250 == 0) // Every 5 secs
        {
            TeleportFarEnemies();
        }

        if (timeSinceLastSpawn >= timeTillNextSpawn)
        {
            GetSpawnParams();
            Spawn();
        }
    }

    void GetSpawnParams() // To set the enemies, elite types, etc. for spawning.
    {
        creditsForNextSpawn = credits;

        List <EnemyParams> enemiesThatCanSpawn = new List<EnemyParams>();
        int allEnemyWeights = 0;

        foreach (EnemyParams enemy in currentEnemyTypes)
        {
            //Debug.Log("globerl");
            if (enemy.spawnCost * enemy.minSpawnAmt <= creditsForNextSpawn)
            {
                enemiesThatCanSpawn.Add(enemy);
                allEnemyWeights += enemy.spawnWeight;
            }
        }

        int toSpawnRand = UnityEngine.Random.Range(0, allEnemyWeights);
        //Debug.Log("toSpawnRand: " + toSpawnRand.ToString() + " / allEnemyWeights: " + allEnemyWeights.ToString());
        int cumSumWeights = 0;

        for (int i = 0; i < enemiesThatCanSpawn.Count; i++)
        {
            cumSumWeights += enemiesThatCanSpawn[i].spawnWeight;
            if (cumSumWeights > toSpawnRand)
            {
                enemyToSpawnNext = enemiesThatCanSpawn[i];
                break;
            }
        }

        float totalCreditsLeft = creditsForNextSpawn - enemyToSpawnNext.spawnCost * enemyToSpawnNext.minSpawnAmt;
        int maxNoToSpawn = enemyToSpawnNext.minSpawnAmt + 1;

        if (totalCreditsLeft > 0)
        {
            maxNoToSpawn = Mathf.Clamp(Mathf.RoundToInt(enemyToSpawnNext.minSpawnAmt + totalCreditsLeft / enemyToSpawnNext.spawnCost), enemyToSpawnNext.minSpawnAmt, enemyToSpawnNext.maxSpawnAmt);
        }

        numEnemiesToSpawn = UnityEngine.Random.Range(enemyToSpawnNext.minSpawnAmt, maxNoToSpawn + 1);

        creditsForNextSpawn -= enemyToSpawnNext.spawnCost * numEnemiesToSpawn;

        // Determining what type of elite (if any) should spawn.
        List<EliteParams> elitesThatCanSpawn = new List<EliteParams>();
        allEnemyWeights = 0;

        foreach (EliteParams elite in currentEliteTypes)
        {
            if ((elite.spawnCostMult - 1f) * enemyToSpawnNext.spawnCost <= creditsForNextSpawn) // Only adds them if at least one of the elite can spawn.
            {
                elitesThatCanSpawn.Add(elite);
                allEnemyWeights += elite.spawnWeight;
            }
        }

        toSpawnRand = UnityEngine.Random.Range(0, allEnemyWeights);
        cumSumWeights = 0;

        for (int i = 0; i < elitesThatCanSpawn.Count; i++)
        {
            cumSumWeights += elitesThatCanSpawn[i].spawnWeight;
            if (cumSumWeights > toSpawnRand)
            {
                eliteType = elitesThatCanSpawn[i];
                break;
            }
        }

        numElitesToSpawn = 0;
        float creditCostForElite = (eliteType.spawnCostMult - 1) * enemyToSpawnNext.spawnCost;
        for (int i = 0; i < numEnemiesToSpawn; i++)
        {
            if (creditsForNextSpawn - creditCostForElite > 0 && UnityEngine.Random.value >= 0.75f) // Has a 25% chance to spawn an elite.
            {
                numElitesToSpawn++;
                creditsForNextSpawn -= creditCostForElite;
            }
            else
            {
                break;
            }
        }

        creditsForNextSpawn = credits - creditsForNextSpawn;
    }

    void GetAvailablePositions()
    {
        goodPositions.Clear();

        for (int i = 0; i < 8; i++)
        {
            Vector3 vecToCheck = Vector3.zero;
            switch (i)
            {
                case 0:
                    vecToCheck = new Vector3(0, 1, 0);
                    break;
                case 1:
                    vecToCheck = new Vector3(1, 1, 0);
                    break;
                case 2:
                    vecToCheck = new Vector3(1, 0, 0);
                    break;
                case 3:
                    vecToCheck = new Vector3(1, -1, 0);
                    break;
                case 4:
                    vecToCheck = new Vector3(0, -1, 0);
                    break;
                case 5:
                    vecToCheck = new Vector3(-1, -1, 0);
                    break;
                case 6:
                    vecToCheck = new Vector3(-1, 0, 0);
                    break;
                case 7:
                    vecToCheck = new Vector3(-1, 1, 0);
                    break;
            }

            vecToCheck *= 11.2f;
            NavMeshHit hit;
            Vector3 thisPosition = new Vector3(camera.transform.position.x, camera.transform.position.y, 0) + vecToCheck;
            //Debug.Log("pos checked: " + (vecToCheck + new Vector3(camera.transform.position.x, camera.transform.position.y, 0)).ToString());
            if (NavMesh.SamplePosition(thisPosition, out hit, 4.5f, NavMesh.AllAreas))
            {
                goodPositions.Add(hit.position);
            }
        }
    }

    void Spawn()
    {
        GetAvailablePositions();
        timeSinceLastSpawn = 0;
        timeTillNextSpawn = UnityEngine.Random.Range(5 * 50, 11 * 50); // Next enemy spawn is in 5 - 11 secs.


        for (int i = 0; i < numEnemiesToSpawn; i++)
        {
            Vector3 posInit = goodPositions[UnityEngine.Random.Range(0, goodPositions.Count)];
            Vector3 pos = posInit + new Vector3(UnityEngine.Random.Range(-0.1f, 0.1f), UnityEngine.Random.Range(-0.1f, 0.1f), 0);
            posInit = posInit - new Vector3(camera.transform.position.x, camera.transform.position.y, 0); // Essentially turns it back into the 'vecToCheck'.
            GameObject johnSpawned = Instantiate(enemyToSpawnNext.enemyPrefab, pos, Quaternion.identity);
            johnSpawned.GetComponent<LevelUp>().GiveXP(currentLevel);
            GameObject spawnedIndicator = Instantiate(spawnPosIndicator, new Vector3(camera.transform.position.x, camera.transform.position.y, -5) + 0.3f * posInit, Quaternion.identity);
            spawnedIndicator.transform.rotation = Quaternion.LookRotation(posInit, Vector3.forward) * Quaternion.Euler(-90, 0, 0);
            spawnedIndicator.GetComponent<enlargeThenShrink>().camera = camera;
            spawnedIndicator.transform.Find("enemySprite").GetComponent<SpriteRenderer>().color = johnSpawned.GetComponent<SpriteRenderer>().color; // Replace with showing the model!
            //if (i < numElitesToSpawn)
            //{
            //    if (johnSpawned.AddComponent<giveEnemySpecificItem>() == null)
            //    {
            //        johnSpawned.AddComponent<giveEnemySpecificItem>();
            //    }
            //    johnSpawned.GetComponent<giveEnemySpecificItem>().itemNameToAdd.Add("SOY");
            //    johnSpawned.GetComponent<giveEnemySpecificItem>().itemNameToAdd.Add("ATG");
            //    johnSpawned.GetComponent<giveEnemySpecificItem>().itemNameToAdd.Add("DMGADDPT5");
            //    johnSpawned.GetComponent<giveEnemySpecificItem>().itemNameToAdd.Add("HOMING");
            //    johnSpawned.GetComponent<giveEnemySpecificItem>().itemNameToAdd.Add("LUCKIER");
            //    johnSpawned.GetComponent<giveEnemySpecificItem>().itemNameToAdd.Add("HP50");
            //}
        }

        Debug.Log("Spawned " + numEnemiesToSpawn.ToString() + " " + enemyToSpawnNext.name + "s! " + numElitesToSpawn.ToString() + " elites spawned, they are " + eliteType.name + " elites.");
        credits -= creditsForNextSpawn;

        GetNoOfEnemies();
        spawnTimerRate = 1;
        numEnemyThreshold = Mathf.CeilToInt(numEnemies / 2f);
    }
}
