using UnityEngine;

[CreateAssetMenu(fileName = "EnemyParams", menuName = "ObjectParams/EliteParams")]
public class EliteParams : ScriptableObject
{
    public string name; // the type of elite, i.e. fire, ice, whatever.
    public int spawnWeight = 100; // The weight of the elite to spawn (lower is less common spawning!)
    public float spawnCostMult = 2.5f;
    public bool canSpawnNaturally = true; // If the elite can spawn via the director, this should be true. (false for more specific/weird requirements to spawn them)
    public string[] areasCanSpawnIn; // Stores the levels that the elite can spawn in, as per the AREAS enum.
}
