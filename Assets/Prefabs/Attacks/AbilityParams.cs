using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "AbilityParams", menuName = "AbilityParams")]
public abstract class AbilityParams : ScriptableObject
{
    public string name;
    public int weight = 100;
    public int maxCharges = 1;
    public int delayTime = 0; // When enemy is using this ability, how long to delay them till their next abiltiy use
    public int coolDownTime = 25; // The cooldown for this specific ability.
    public GameObject[] objectsToUse; // Objects this particular attack spawns and such.
    public AudioClip[] sfx;
    public GameObject[] spawnedAttackObjs;
    public float range = 99999f;

    public float damageMultiplier = 1;
    public float massCoeffMult = 1;
    public float procCoeffMult = 1;
    public float iFrameFracMult = 1;

    public int chargeLength = 0; // For if the ability can be charged up for a funny effect, leave at 0 if the ability is not to be charged.
    public bool isCharged = false; // This gets set/reset every attack, just to easily keep track of whether the last attack was charged or not.
    public bool rechargeAllShotsAfterCooldown = false; // For things like shotgun, where both charges get given back at the same time, have this be true.
    public abstract void ActivateAbility(GameObject dealer, GameObject target, Vector2 direction, bool isPlayerTeam, Material mat, int layer, string tag, bool overrideBulletSpawnMethod);

    // Override bullet spawn method is used by things like splitshot - they only want the base projectile spawned, they don't want the projectile to fuck around
    // with any weirder ways of spawning, so they use that bool to set it to just skip any weird shot direction shenanigans.
    public void UseAttack(GameObject dealer, GameObject target, Vector3 spawnPosition, Vector2 direction, bool isPlayerTeam, int abilityIndex, bool chargeStatus, bool overrideCooldownSetting, bool playSound, bool overrideBulletSpawnMethod)
    {
        isCharged = chargeStatus;
        int LayerPlayerBullet = LayerMask.NameToLayer("PlayerBullets");
        int LayerEnemyBullet = LayerMask.NameToLayer("Enemy Bullets");
        Material mat = null;
        int layer = 0;
        string tag = "";

        if (isPlayerTeam)
        {
            mat = EntityReferencerGuy.Instance.playerBulletMaterial;
            layer = LayerPlayerBullet;
            tag = "PlayerBullet";
        }
        else
        {
            mat = EntityReferencerGuy.Instance.enemyBulletMaterial;
            layer = LayerEnemyBullet;
            tag = "enemyBullet";
        }

        if (playSound && sfx.Length > 0)
        {
            SoundManager.Instance.PlaySound(sfx[Random.Range(0, sfx.Length)]);
        }

        spawnedAttackObjs = new GameObject[objectsToUse.Length];
        for (int i = 0; i < objectsToUse.Length; i++)
        {
            spawnedAttackObjs[i] = Instantiate(objectsToUse[i], spawnPosition, Quaternion.identity);
            spawnedAttackObjs[i].tag = tag;
            spawnedAttackObjs[i].layer = layer;

            if (spawnedAttackObjs[i].GetComponent<ItemHolder>() != null)
            {
                spawnedAttackObjs[i].GetComponent<ItemHolder>().itemsHeld = dealer.GetComponent<ItemHolder>().itemsHeld;

            }
            
            if (spawnedAttackObjs[i].GetComponent<DealDamage>() != null)
            {
                spawnedAttackObjs[i].GetComponent<DealDamage>().finalDamageMult = dealer.GetComponent<DealDamage>().finalDamageMult * dealer.GetComponent<DealDamage>().damageBonus;
                //spawnedAttackObjs[i].GetComponent<DealDamage>().massCoeff *= dealer.GetComponent<DealDamage>().massCoeff;
                spawnedAttackObjs[i].GetComponent<DealDamage>().owner = dealer;
                spawnedAttackObjs[i].GetComponent<DealDamage>().abilityIndex = abilityIndex;
                spawnedAttackObjs[i].GetComponent<DealDamage>().abilityType = this;
                spawnedAttackObjs[i].GetComponent<DealDamage>().finalDamageMult *= damageMultiplier;
                spawnedAttackObjs[i].GetComponent<DealDamage>().procCoeff *= procCoeffMult;
                spawnedAttackObjs[i].GetComponent<DealDamage>().massCoeff *= massCoeffMult;
                spawnedAttackObjs[i].GetComponent<DealDamage>().iFrameFac *= iFrameFracMult;
            }
        }

        if (!overrideCooldownSetting)
        {
            dealer.GetComponent<Attack>().coolDowns[abilityIndex] = Mathf.RoundToInt(coolDownTime * dealer.GetComponent<Attack>().cooldownFacIndiv[abilityIndex] * dealer.GetComponent<Attack>().cooldownFac);
            if (!isPlayerTeam) // For adding extra to the cooldown timer based on stopwatch shenanigans if this is an enemy
            {
                dealer.GetComponent<Attack>().coolDowns[abilityIndex] = Mathf.RoundToInt(dealer.GetComponent<Attack>().coolDowns[abilityIndex] * EntityReferencerGuy.Instance.stopWatchDebuffAmt);
            }
            dealer.GetComponent<Attack>().charges[abilityIndex]--;
            dealer.GetComponent<Attack>().masterCooldown = Mathf.RoundToInt(delayTime * dealer.GetComponent<Attack>().cooldownFacIndiv[abilityIndex] * dealer.GetComponent<Attack>().cooldownFac);
            dealer.GetComponent<Attack>().chargeTimers[abilityIndex] = 0;
        }
        //Debug.Log("direction: " + direction.ToString());
        ActivateAbility(dealer, target, direction, isPlayerTeam, mat, layer, tag, overrideBulletSpawnMethod);
    }

    public abstract bool CheckUsability(GameObject dealer, GameObject target);
}
