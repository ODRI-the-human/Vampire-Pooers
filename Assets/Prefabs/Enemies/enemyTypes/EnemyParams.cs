using UnityEngine;
using System;

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

    // For the actual parameters of the enemy type!!!!!
    public float moveSpeed = 3.5f;
    public bool recieveKnockback = true;
    public int numExtraShots = 0;
    public float maxHP = 150;
    public float damageMult = 1f;
    public float massCoeff = 1f;
    public float fireDelayMult = 1f;
    public Color colorToSet;
    public AbilityParams[] abilities;
    public bool canDealDamage = false;
    public string[] itemsToGive;
    public string[] scriptsToAdd;
    public float[] resistValues = new float[] { 0, 0, 0, 0, 0, 0, 0, 0 };

    public void SetEnemyParams(GameObject enemy)
    {
        enemy.GetComponent<SpriteRenderer>().color = colorToSet;
        enemy.GetComponent<NewPlayerMovement>().baseMoveSpeed = moveSpeed;
        enemy.GetComponent<NewPlayerMovement>().recievesKnockback = recieveKnockback;
        enemy.GetComponent<Attack>().noExtraShots = numExtraShots;
        enemy.GetComponent<Attack>().abilityTypes = abilities;
        enemy.GetComponent<Attack>().cooldownFac = fireDelayMult;
        enemy.GetComponent<giveEnemySpecificItem>().itemNameToAdd = itemsToGive;
        enemy.GetComponent<HPDamageDie>().MaxHP = maxHP;
        enemy.GetComponent<HPDamageDie>().resistVals = resistValues;
        enemy.GetComponent<DealDamage>().finalDamageMult = damageMult;
        enemy.GetComponent<DealDamage>().canDealDamage = canDealDamage;
        enemy.GetComponent<DealDamage>().massCoeff = massCoeff;

        for (int i = 0; i < scriptsToAdd.Length; i++)
        {
            string scriptName = scriptsToAdd[i];
            Type scriptType = Type.GetType(scriptName);
            enemy.AddComponent(scriptType);
        }
    }
}