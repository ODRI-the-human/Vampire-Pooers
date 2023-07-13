using UnityEngine;

[CreateAssetMenu(fileName = "EnemyParams", menuName = "ObjectParams/EnemyParams")]
public class EnemyParams : ScriptableObject
{
    public string name;
    public float spawnCost = 20f;
    public int spawnWeight = 100; // The weight of the enemy to spawn (lower is less common spawning!)
    public int minSpawnAmt = 3;
    public int maxSpawnAmt = 8;
    public string[] areasCanSpawnIn; // Stores the levels that the enemy can spawn in, as per the AREAS enum.
    public GameObject enemyPrefab;
}