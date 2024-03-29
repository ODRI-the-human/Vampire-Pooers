using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "AbilityParams", menuName = "AbilityParams")]
public abstract class AbilityParams : ScriptableObject
{
    public string name;
    public string description;
    public string flavourText;
    public int attackMode = 0; // Effects the mechanics of actually using the attack. 0 = full auto, 1 = not full auto, 2 = chargeable attack, 3 = charge up, then auto once charged.
    public int weight = 100;
    public int maxCharges = 1;
    public int masterCooldownTime = 0; // When enemy is using this ability, how long to delay them till their next abiltiy use
    public float spawnDelay = 0f; // Delay IN SECONDS until this ability actually gets used.
    public float cameraPushAmount = 0f;
    public bool alignCamPushWithDir = true;
    public float dirRandAmt = 0f;
    public bool randomiseDirAtStart = true;
    public int coolDownTime = 25; // The cooldown for this specific ability.
    public int chargeUpTime = 0; // The minimum length of time this ability's button needs to be held to start attacking. For like miniguns n shit. Used when attack mode = 3.
    public GameObject[] objectsToUse; // Objects this particular attack spawns and such.
    public AudioClip[] sfx;
    public string[] attackEffectsToAdd;
    public GameObject[] spawnedAttackObjs;
    public float range = 99999f;

    public float damageMultiplier = 1;
    public float massCoeffMult = 1;
    public float procCoeffMult = 1;
    public float iFrameFracMult = 1;

    public int chargeLength = 0; // For if the ability can be charged up for a funny effect, leave at 0 if the ability is not to be charged.
    public bool isCharged = false; // This gets set/reset every attack, just to easily keep track of whether the last attack was charged or not.
    public bool rechargeAllShotsAfterCooldown = false; // For things like shotgun, where both charges get given back at the same time, have this be true.

    public int dropWeight = 100; // The likelihood of this weapon being selected by a pedestal.
    public Mesh weaponMesh;

    public abstract void ActivateAbility(GameObject dealer, GameObject target, Vector2 direction, bool isPlayerTeam, Material mat, int layer, string tag, bool overrideBulletSpawnMethod);

    public Vector2 RandomiseDirection(Vector2 inVec, float randAmtToUse)
    {
        float currentAngle = Random.Range(-randAmtToUse, randAmtToUse);
        return new Vector2(inVec.x * Mathf.Cos(currentAngle) - inVec.y * Mathf.Sin(currentAngle), inVec.x * Mathf.Sin(currentAngle) + inVec.y * Mathf.Cos(currentAngle));
    }

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
            if (dealer.GetComponent<Attack>().isPlayer)
            {
                if (alignCamPushWithDir)
                {
                    EntityReferencerGuy.Instance.camera.GetComponent<cameraMovement>().CameraShake(0, -direction * cameraPushAmount);
                }
                else
                {
                    EntityReferencerGuy.Instance.camera.GetComponent<cameraMovement>().CameraShake(cameraPushAmount, Vector3.zero);
                }
            }
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

            if (spawnedAttackObjs[i].GetComponent<ItemHolder2>() != null)
            {
                spawnedAttackObjs[i].GetComponent<ItemHolder2>().itemsHeldTransferred = new ItemSOInst[dealer.GetComponent<ItemHolder2>().itemsHeld.Count];
                for (int j = 0; j < dealer.GetComponent<ItemHolder2>().itemsHeld.Count; j++)
                {
                    spawnedAttackObjs[i].GetComponent<ItemHolder2>().itemsHeldTransferred[j] = dealer.GetComponent<ItemHolder2>().itemsHeld[j];
                }
            }
            
            if (spawnedAttackObjs[i].GetComponent<ApplyAttackModifiers>() != null)
            {
                spawnedAttackObjs[i].GetComponent<ApplyAttackModifiers>().effectNames = attackEffectsToAdd;
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

        if (randomiseDirAtStart)
        {
            direction = RandomiseDirection(direction, dirRandAmt);
        }

        //Debug.Log("direction: " + direction.ToString());
        ActivateAbility(dealer, target, direction, isPlayerTeam, mat, layer, tag, overrideBulletSpawnMethod);
    }

    public abstract bool CheckUsability(GameObject dealer, GameObject target);
}
