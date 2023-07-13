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
    [SerializeField] float creditCost = 0; // sets the cost of the spawn!
    [SerializeField] float spawnCost = 0; // sets the number of credits required for the next spawn, so FixedUpdate knows when to call the Spawn function.
    float creditIncreaseRate = 0.2f;
    public int numEnemyThreshold = 0; // this is the number of enemies at which the director will force spawning some enemies.
    public int numEnemies; // Counts num of enemies alive.
    int killCounter = 0;
    public GameObject camera;

    public int currentLevel; // Stores the level we're currently on.

    int timeSinceLastEnemyCheck = 0;
    public EnemyParams[] allEnemyTypes;// stores all enemy types.
    public List<EnemyParams> currentEnemyTypes = new List<EnemyParams>();// stores the enemy types that can be spawned on this level.

    public EliteParams[] allEliteTypes;
    public List<EliteParams> currentEliteTypes = new List<EliteParams>();// stores the enemy types that can be spawned on this level.

    public List<Vector3> goodPositions = new List<Vector3>();

    [SerializeField] EnemyParams enemyToSpawnNext; // Stores the enemy that will be spawned next.
    [SerializeField] int numEnemiesToSpawn; // The no. of enemies that will be spawned next.
    [SerializeField] EliteParams eliteType; // The elite status of any enemies that will be spawned.
    [SerializeField] int numElitesToSpawn;

    void Start()
    {
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
        GetSpawnParams(false);
        credits = creditCost - 20f;
    }

    void GetNoOfEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Hostile");
        numEnemies = enemies.Length;
    }

    public void OnEnemiesKilled() // Gets called by HPDamageDie when an enemy is killed.
    {
        killCounter++;
        if (timeSinceLastEnemyCheck >= 50) // Only does the following a max of once per second.
        {
            GetNoOfEnemies();

            if (numEnemyThreshold >= numEnemies)
            {
                GetSpawnParams(true);
            }
        }

        if (killCounter % 5 == 0)
        {
            TeleportFarEnemies();
        }
    }

    void TeleportFarEnemies() // Teleports far-away enemies to near the player, otherwise there can just be some chilling doing nothing.
    {
        GetAvailablePositions();
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Hostile");
        foreach (GameObject enemy in enemies)
        {
            if ((enemy.transform.position - camera.transform.position).magnitude > 25)
            {
                Debug.Log("teleported an enemy");
                enemy.transform.position = goodPositions[UnityEngine.Random.Range(0, goodPositions.Count)] + new Vector3(UnityEngine.Random.Range(-0.1f, 0.1f), UnityEngine.Random.Range(-0.1f, 0.1f), 0);
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timeSinceLastEnemyCheck++;
        credits += creditIncreaseRate * scaleConst; // Director gains creditIncreaseRate * scaleConst * 50 credits per sec.
        scaleConst += 0.001f;
        if (credits >= creditCost && enemyToSpawnNext != null)
        {
            Spawn();
            GetSpawnParams(false);
        }
    }

    void GetSpawnParams(bool spawnNow) // To set the enemies, elite types, etc. for spawning.
    {
        creditsForNextSpawn = credits;
        if (!spawnNow)
        {
            creditsForNextSpawn += creditIncreaseRate * scaleConst * 1000; // Sets the no of credits to the amount it will have in 20 seconds' time.
        }

        creditCost = creditsForNextSpawn;

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
            if (creditsForNextSpawn - creditCostForElite > 0)
            {
                numElitesToSpawn++;
                creditsForNextSpawn -= creditCostForElite;
            }
            else
            {
                break;
            }
        }

        creditCost -= creditsForNextSpawn;
        //else
        //{
        //    eliteType = null;
        //    enemyToSpawnNext = null;
        //}
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

            vecToCheck *= 13.5f;
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

        for (int i = 0; i < numEnemiesToSpawn; i++)
        {
            Vector3 pos = goodPositions[UnityEngine.Random.Range(0, goodPositions.Count)] + new Vector3(UnityEngine.Random.Range(-0.1f, 0.1f), UnityEngine.Random.Range(-0.1f, 0.1f), 0);
            GameObject johnSpawned = Instantiate(enemyToSpawnNext.enemyPrefab, pos, Quaternion.identity);
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
        credits -= creditCost;

        GetNoOfEnemies();
        numEnemyThreshold = Mathf.CeilToInt(numEnemies / 4f);
    }
}
